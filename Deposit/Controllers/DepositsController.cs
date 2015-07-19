using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Deposit.Models;
using DepositDatabase.Model;
using Microsoft.AspNet.Identity;

namespace Deposit.Controllers
{
    public class DepositsController : Controller
    {
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

                using(var db = new DepositEntities())
                {
                    var card = from cards in db.Cards
                        where cards.Id == model.Id
                              && cards.ExpirationMonth == model.ExpirationMonth
                              && cards.ExpirationYear == model.ExpirationYear
                              && cards.SecretCode == model.SecretCode
                        select cards;

                    if (card.ToList().Count != 1) return PartialView("_NoSuchCard");

                    card.First().UserOwnerId = User.Identity.GetUserId();
                    
                    db.SaveChanges();

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
            var id = Convert.ToByte(Request["termsId"]);
            ViewData["termsId"] = id;

            var waysOfAccumulation = CreateWaysOfAccumulation();
            var cards = CreateUserCards(id);
            var model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }

        [HttpPost]
        public ActionResult NewDeposit(NewDepositViewModel model)
        {
            var termsId = Convert.ToByte(Request["termsId"]);
            ViewData["termsId"] = termsId;

            if (ModelState.IsValid)
            {
                using (var db = new DepositEntities())
                {
                    var card = db.Cards.First(c => c.Id == model.SelectedCardId);

                    if (Convert.ToDecimal(model.Sum) <= card.Balance)
                    {
                        var depositTerms = db.DepositTerms.First(dt => dt.Id == termsId);

                        var newDeposit = new Deposits()
                        {
                            Id = db.Deposits.Count(),
                            UserOwnerId = User.Identity.GetUserId(),
                            InitialAmount = Convert.ToDecimal(model.Sum),
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                            Balance = Convert.ToDecimal(model.Sum),
                            DepositWaysOfAccumulation = db.DepositWaysOfAccumulation.First(w => w.Id == model.SelectedWayOfAccumulationId),
                            Cards = card,
                            DepositTerms = depositTerms,
                            DepositStates = db.DepositStates.First(ds => ds.Id == 0)
                        };

                        card.Balance -= newDeposit.InitialAmount;

                        db.Deposits.Add(newDeposit);
                        
                        var newCardHistoryRecord = new CardHistory()
                        {
                            Id = db.CardHistory.Count(),
                            DateTime = DateTime.Now,
                            Desription = String.Format("Opening deposit #{0} of {1} ({2}).", newDeposit.Id, newDeposit.Balance,newDeposit.DepositTerms.Currencies.Abbreviation),
                            Cards = card
                        };

                        db.CardHistory.Add(newCardHistoryRecord); 
                        db.SaveChanges();
                    }
                    else
                    {
                        return PartialView("_NotEnoughMoney");
                    }
                }

                return PartialView("_DepositOpenedSuccessfully");
            }

            var waysOfAccumulation = CreateWaysOfAccumulation();
            var cards = CreateUserCards(termsId);
            model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }
        #endregion

        public ActionResult CloseDeposit(int depositId, string cardId)
        {
            using (var db = new DepositEntities())
            {
                var card = db.Cards.First(c => c.Id.Equals(cardId));
                var deposit = db.Deposits.First(d => d.Id == depositId);

                card.Balance += deposit.Balance;
                deposit.DepositStates = db.DepositStates.First(s => s.Name.Equals("Закритий"));

                db.CardHistory.Add(new CardHistory()
                {
                    Id = db.CardHistory.Count(),
                    Cards = card,
                    DateTime = DateTime.Now,
                    Desription = String.Format("Closing deposit #{0}. Income: {1} ({2}).", 
                    deposit.Id, deposit.Balance, deposit.DepositTerms.Currencies.Abbreviation)
                });

                db.SaveChanges();
            }

            return PartialView("_DepositClosedSuccessfully");
        }
        
        #region Refactored methods
        private List<Cards> CreateUserCards(byte id)
        {
            List<Cards> cards;

            using (var db = new DepositEntities())
            {
                string userId = User.Identity.GetUserId();
                byte currencyId = db.DepositTerms.Where(dt => dt.Id == id).ToList().First().Currencies.Id;

                cards = (from c in db.Cards
                    where c.AspNetUsers.Id == userId && c.Currencies.Id == currencyId
                    select c).ToList();
            }

            return cards;
        }

        private static List<DepositWaysOfAccumulation> CreateWaysOfAccumulation()
        {
            List<DepositWaysOfAccumulation> waysOfAccumulations = null;

            using (var db = new DepositEntities())
            {
                waysOfAccumulations = db.DepositWaysOfAccumulation.ToList();
            }

            return waysOfAccumulations;
        }
        #endregion
    }
}