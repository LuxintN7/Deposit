using System;
using System.Web.Mvc;
using Deposit.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using DomainLogic;
using DomainLogic.Model;
using DomainLogic.Handlers;
using DepositDatabase;
using DepositDatabase.Handlers;


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
            DiContainer.RegisterType<ICardsService, CardsService>(new InjectionConstructor());
            DiContainer.RegisterType<IDepositWaysOfAccumulationService, DepositWaysOfAccumulationService>(new InjectionConstructor());
            DiContainer.RegisterType<IDepositTermsService, DepositTermsService>(new InjectionConstructor());
            DiContainer.RegisterType<ICurrenciesService, CurrenciesService>(new InjectionConstructor());
            DiContainer.RegisterType<IAddCardHandler, AddCardHandler>(new InjectionConstructor());
            DiContainer.RegisterType<INewDepositHandler, NewDepositHandler>(new InjectionConstructor());
            DiContainer.RegisterType<ICloseDepositHandler, CloseDepositHandler>(new InjectionConstructor());
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
            var cards = DiContainer.Resolve<ICardsService>().CreateUserCardsByCurrencyList(userId, termsId);
            var waysOfAccumulation = DiContainer.Resolve<IDepositWaysOfAccumulationService>().GetList();
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
                        
                        newDepositHandler.AddCardHistoryRecord(card.Id,cardHistoryDescription);
                    }
                    else
                    {
                        return PartialView("_Message", new MessageViewModel("Not enough money.", false));
                    }

                    return PartialView("_Message", new MessageViewModel("Deposit has been opened successfully.", true));
                }
            }

            var cards = DiContainer.Resolve<ICardsService>().CreateUserCardsByCurrencyList(userId, termsId);
            var waysOfAccumulation = DiContainer.Resolve<IDepositWaysOfAccumulationService>().GetList();
            model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }

        private bool CardHasEnoughFunds(decimal cardBalance, decimal depositAmount)
        {
            return Convert.ToDecimal(depositAmount) <= cardBalance;
        }

#endregion

        public ActionResult CloseDeposit(int depositId, string cardId)
        {
            using (var closeDepositHandler = DiContainer.Resolve<ICloseDepositHandler>())
            {
                var deposit = closeDepositHandler.GetDepositById(depositId);

                closeDepositHandler.IncreaseCardBalanceByDepositBalance(deposit.Balance, cardId);

                closeDepositHandler.SetDepositState("Closed", depositId);

                var cardHistoryDescription = String.Format("Closing deposit #{0}. Income: {1} ({2}).",
                    deposit.Id, deposit.Balance, closeDepositHandler.GetCurrencyByDepositTermsId(deposit.TermId).Abbreviation);
                
                closeDepositHandler.AddCardHistoryRecord(cardId, cardHistoryDescription);
            }

            return PartialView("_Message", new MessageViewModel("Deposit has been closed successfully.", true));
        }
    }
}