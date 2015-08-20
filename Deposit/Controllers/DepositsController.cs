using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Deposit.Models;
using DomainLogic;
using DomainLogic.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using DepositDatabase;

namespace Deposit.Controllers
{
    public class DepositController : Controller
    {
        public static UnityContainer DiContainer { get; private set; }

        static DepositController()
        {
            DiContainer = new UnityContainer();
        }

        public DepositController()
        {
            DiContainer.RegisterType<IAddCardHandler, AddCardHandler>(new InjectionConstructor());
            DiContainer.RegisterType<INewDepositHandler, NewDepositHandler>(new InjectionConstructor());
            DiContainer.RegisterType<IDepositTermsService, DepositTermsData>(new InjectionConstructor());
            DiContainer.RegisterType<ICurrenciesService, CurrenciesData>(new InjectionConstructor());
        }

        [Authorize] 
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GeneralInfo()
        {
            return PartialView("_GeneralInfo");
        }


        [Authorize]
        public ActionResult GeneralInfoContent()
        {
            return PartialView("_GeneralInfoContent");
        }

        [Authorize]
        public ActionResult DepositList()
        {
            return PartialView("_DepositList");
        }

        [Authorize]
        public ActionResult OpenNewDeposit()
        {
            return PartialView("_OpenNewDeposit");
        }


#region GeneralInfo actions
        public ActionResult AddCard()
        {
            return PartialView("AddCard");
        }

        [HttpPost]
        public ActionResult AddCard(Cards model)
        {
            if (ModelState.IsValid)
            {
                ViewData["modelIsValid"] = true;

                using (var addCardHandler = DiContainer.Resolve<IAddCardHandler>())
                {
                    var card = addCardHandler.GetCardByRequisites(model.Id, model.ExpirationMonth, model.ExpirationYear, model.SecretCode);

                    if (card == null)
                    {
                        return PartialView("_Message", new MessageViewModel("A card with such requisites does not exist.", false));
                    }

                    addCardHandler.SetCardOwnerId(card,User.Identity.GetUserId());

                    return RedirectToAction("GeneralInfoContent");
                }
            }

            ViewData["modelIsValid"] = false;
            return PartialView("AddCard");
        }

        #endregion

        #region OpenNewDeposit actions
        public ActionResult NewDeposit()
        {
            var termsId = Convert.ToByte(Request["termsId"]);
            ViewData["termsId"] = termsId;

            var userId = User.Identity.GetUserId();
            var cards = CardsData.CreateUserCardsByCurrencyList(userId, termsId).ToDomainLogic();
            var waysOfAccumulation = DepositWaysOfAccumulationData.CreateWayList().ToDomainLogic();
            var model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }

        [HttpPost]
        public ActionResult NewDeposit(NewDepositViewModel model)
        {
            var termsId = Convert.ToByte(Request["termsId"]);
            ViewData["termsId"] = termsId;
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                using (var newDepositHandler = DiContainer.Resolve<INewDepositHandler>())
                {
                    var card = newDepositHandler.GetCardById(model.CardId);

                    if (CardHasEnoughFunds(card.Balance, model.Amount))
                    {
                        var newDeposit = newDepositHandler
                            .CreateNewDeposit(model.Amount,model.WayOfAccumulationId, userId,termsId, card.Id);

                        newDepositHandler.DecreaseCardBalanceByDepositAmount(newDeposit.InitialAmount,card.Id);

                        string cardHistoryDescription = String.Format("Opening deposit #{0} of {1} ({2}).",
                            newDeposit.Id, newDeposit.Balance, newDepositHandler.GetCurrencyByDepositTermsId(newDeposit.TermId).Abbreviation);
                        
                        newDepositHandler.AddCardHistoryToDbContext(card.Id,cardHistoryDescription);
                    }
                    else
                    {
                        return PartialView("_Message", new MessageViewModel("Not enough money.", false));
                    }

                    return PartialView("_Message", new MessageViewModel("Deposit has been opened successfully.", true));
                }
            }

            var cards = CardsData.CreateUserCardsByCurrencyList(userId, termsId).ToDomainLogic();
            var waysOfAccumulation = DepositWaysOfAccumulationData.CreateWayList().ToDomainLogic();
            model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }

        private bool CardHasEnoughFunds(Decimal cardBalance, decimal depositAmount)
        {
            return Convert.ToDecimal(depositAmount) <= cardBalance;
        }
        #endregion

        //public ActionResult CloseDeposit(int depositId, string cardId)
        //{
        //    using (var db = new DepositEntities())
        //    {
        //        var card = CardsData.GetCardById(cardId, db);
        //        var deposit = DepositsData.GetDepositById(depositId, db);

        //        card.Balance += deposit.Balance;
        //        deposit.DepositStates = DepositStatesData.GetStateByName("Closed", db);

        //        var cardHistoryDescription = String.Format("Closing deposit #{0}. Income: {1} ({2}).",
        //            deposit.Id, deposit.Balance, deposit.DepositTerms.Currencies.Abbreviation);
        //        CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription, db);

        //        db.SaveChanges();
        //    }

        //    return PartialView("_Message", new MessageViewModel("Deposit has been closed successfully.", true));
        //}
    }
}