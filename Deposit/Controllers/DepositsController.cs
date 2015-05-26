using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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

                    if (card.ToList().Count != 1) return PartialView("_NoSuchCard");

                    card.First().UserOwnerId = User.Identity.GetUserId();
                    
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
                    var card = db.Cards.First(c => c.Id == model.SelectedCardId);


                    if (Convert.ToDecimal(model.Sum) <= card.Balance)
                    {
                        var newDeposit = new Deposits()
                        {
                            Id = db.Deposits.Count(),
                            UserOwnerId = User.Identity.GetUserId(),
                            InitialAmount = Convert.ToDecimal(model.Sum),
                            StartDate = DateTime.Now,
                            Balance = Convert.ToDecimal(model.Sum),

                            DepositTerms = db.DepositTerms.First(dt => dt.Id == termsId),
                            DepositStates = db.DepositStates.First(ds => ds.Id == 0)
                        };

                        db.Deposits.Add(newDeposit);
                        db.SaveChanges();
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
                    where c.AspNetUsers.Id == userId && c.Currencies.Id == currencyId
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