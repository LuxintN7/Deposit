using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Deposit.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Deposit.Controllers
{
    public class DepositsController : Controller
    {
        // GET: Deposits
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
        public ActionResult DepositList()
        {
            return PartialView("_DepositList");
        }

        [Authorize]
        public ActionResult MakeNewDeposit()
        {
            return PartialView("_MakeNewDeposit");
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
                using(var db = new DepositEntities())
                {
                    var card = from cards in db.Cards
                        where cards.Id == model.Id
                              && cards.ExpirationMonth == model.ExpirationMonth
                              && cards.ExpirationYear == model.ExpirationYear
                              && cards.SecretCode == model.SecretCode
                        select cards;

                    if (card.ToArray().Length != 1) return View(-1);

                    db.CardOwnership.Add(new CardOwnership() { UserId = User.Identity.GetUserId(), CardId = model.Id});
                    db.SaveChanges();

                    return RedirectToAction("GeneralInfo");
                }
            }
            return PartialView("AddCard");
        }
        #endregion

        #region MakeNewDeposit actions

        
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
                    var depositTerms = (from dt in db.DepositTerms
                        where dt.Id == termsId
                        select dt).ToList().First();

                    var card = db.Cards.Where(c => c.Id == model.SelectedCardId).ToList().First();


                    if (Convert.ToDecimal(model.Sum) <= card.Balance)
                    {
                        int depositCount = db.Deposits.Count();

                        //db.Deposits.Add(new Deposits()
                        //{
                        //    DepositOwnership =
                        //        new DepositOwnership()
                        //        {
                        //            UserId = User.Identity.GetUserId(),
                        //            //DepositId = depositCount
                        //        },
                        //    Id = depositCount,
                        //    InitialAmount = Convert.ToDecimal(model.Sum),
                        //    StartDate = DateTime.Now,
                        //    EndDate = DateTime.Now.AddMonths(depositTerms.Months),
                        //    Balance = Convert.ToDecimal(model.Sum),
                        //    DepositTerms = depositTerms,

                        //    DepositStates = db.DepositStates.Where(d => d.Id == 0).ToList().First()
                        //});

                        DepositStates depStates = db.DepositStates.Where(d => d.Id == 0).ToList().First();
                        


                        Deposits deposit = new Deposits();
                        deposit.Id = depositCount;
                        deposit.InitialAmount = Convert.ToDecimal(model.Sum);
                        deposit.StartDate = DateTime.UtcNow;
                        deposit.EndDate = DateTime.Now.AddMonths(depositTerms.Months);
                        deposit.Balance = Convert.ToDecimal(model.Sum);

                        deposit.DepositTerms = depositTerms;
                        deposit.DepositStates = depStates;
                        //deposit.DepositOwnership = db.DepositOwnership.FirstOrDefault(d => d.DepositId.Equals(depositCount));

                        //depOwn.Deposits = deposit;

                        DepositOwnership depOwn = new DepositOwnership();
                        depOwn.UserId = User.Identity.GetUserId();
                        depOwn.Deposits = db.Deposits.FirstOrDefault(d => d.Id.Equals(depositCount));

                        db.Deposits.Add(deposit);

                        db.SaveChanges();
                        //db.Dispose();
                    }
                    else
                    {
                        return PartialView("_NotEnoughMoney");
                    }
                }

                return PartialView("_DepositSuccessfullyMade");
            }

            var waysOfAccumulation = CreateWaysOfAccumulation();
            var cards = CreateUserCards(termsId);
            model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }

        #endregion
        
        // Refactored methods
        #region refactored methods
        private List<Cards> CreateUserCards(byte id)
        {
            List<Cards> cards;

            using (var db = new DepositEntities())
            {
                string userId = User.Identity.GetUserId();
                byte currencyId = db.DepositTerms.Where(dt => dt.Id == id).ToList().First().Currencies.Id;

                cards = (from c in db.Cards
                    join co in db.CardOwnership on c.Id equals co.CardId
                    where co.UserId == userId && c.Currencies.Id == currencyId
                    select c).ToList();
            }
            return cards;
        }

        private static List<WayOfAccumulation> CreateWaysOfAccumulation()
        {
            var waysOfAccumulation = new List<WayOfAccumulation>()
            {
                new WayOfAccumulation() {Id = 0, Name = "Капіталізація"},
                new WayOfAccumulation() {Id = 1, Name = "Перераховувати на картку"}
            };
            return waysOfAccumulation;
        }
        #endregion
    }
}