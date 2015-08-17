using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Deposit.Models;
//using DepositDatabase;
//using DepositDatabase.Model;
using DomainLogic;
using DomainLogic.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace Deposit.Controllers
{
    public class DepositController : Controller
    {
        private UnityContainer container;

        public DepositController()
        {
            container = new UnityContainer();

            container.RegisterType<IAddCardHandler, AddCardHandler>();
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

                using (var db = new AddCardHandler)
                {
                    var card = GetCardByRequisites(model.Id, model.ExpirationMonth, model.ExpirationYear, model.SecretCode);

                    if (card == null)
                    {
                        return PartialView("_Message", new MessageViewModel("A card with such requisites does not exist.", false));
                    }

                    card.UserOwnerId = User.Identity.GetUserId();

                    db.SaveChanges();

                    return RedirectToAction("GeneralInfoContent");
                }
            }

            ViewData["modelIsValid"] = false;
            return PartialView("AddCard");
        }

        #endregion
        
//#region OpenNewDeposit actions
//        public ActionResult NewDeposit()
//        {
//            var termsId = Convert.ToByte(Request["termsId"]);
//            ViewData["termsId"] = termsId;
           
//            var userId = User.Identity.GetUserId();
//            var cards = CardsData.CreateUserCardsByCurrencyList(userId, termsId); 
//            var waysOfAccumulation = DepositWaysOfAccumulationData.CreateWayList();
//            var model = new NewDepositViewModel(cards, waysOfAccumulation);

//            return PartialView("NewDeposit", model);
//        }

//        [HttpPost]
//        public ActionResult NewDeposit(NewDepositViewModel model)
//        {
//            var termsId = Convert.ToByte(Request["termsId"]);
//            ViewData["termsId"] = termsId;
//            var userId = User.Identity.GetUserId();

//            if (ModelState.IsValid)
//            {
//                using (var db = new DepositEntities())
//                {
//                    var card = CardsData.GetCardById(model.CardId, db);

//                    if (CardHasEnoughFunds(card.Balance, model.Amount))
//                    {
//                        var newDeposit = DepositsData.CreateDeposit(model, userId, termsId, card.Id, db);
//                        DepositsData.AddNewDepositToDbContext(newDeposit, db);

//                        card.Balance -= newDeposit.InitialAmount;

//                        string cardHistoryDescription = String.Format("Opening deposit #{0} of {1} ({2}).",
//                            newDeposit.Id, newDeposit.Balance, newDeposit.DepositTerms.Currencies.Abbreviation);
//                        CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription, db);

//                        db.SaveChanges();
//                    }
//                    else
//                    {
//                        return PartialView("_Message", new MessageViewModel("Not enough money.", false));
//                    }

//                    return PartialView("_Message", new MessageViewModel("Deposit has been opened successfully.", true));
//                }
//            }

//            var cards = CardsData.CreateUserCardsByCurrencyList(userId, termsId);
//            var waysOfAccumulation = DepositWaysOfAccumulationData.CreateWayList();
//            model = new NewDepositViewModel(cards, waysOfAccumulation);

//            return PartialView("NewDeposit", model);
//        }

//        private bool CardHasEnoughFunds(Decimal cardBalance, decimal depositAmount)
//        {
//            return Convert.ToDecimal(depositAmount) <= cardBalance;
//        }
//#endregion

//        public ActionResult CloseDeposit(int depositId, string cardId)
//        {
//            using (var db = new DepositEntities())
//            {
//                var card = CardsData.GetCardById(cardId, db);
//                var deposit = DepositsData.GetDepositById(depositId, db);

//                card.Balance += deposit.Balance;
//                deposit.DepositStates = DepositStatesData.GetStateByName("Closed", db);
                
//                var cardHistoryDescription = String.Format("Closing deposit #{0}. Income: {1} ({2}).",
//                    deposit.Id, deposit.Balance, deposit.DepositTerms.Currencies.Abbreviation);
//                CardHistoryData.AddRecordToDbContext(card, cardHistoryDescription, db);

//                db.SaveChanges();
//            }

//            return PartialView("_Message", new MessageViewModel("Deposit has been closed successfully.", true));
//        }
    }
}