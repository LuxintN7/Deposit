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
        public static ICardsService CardsService { get; set; }
        public static IDepositWaysOfAccumulationService DepositWaysOfAccumulationService { get; set; }
        public static IDepositTermsService DepositTermsService { get; set; }
        public static ICurrenciesService CurrenciesService { get; set; }
        public static IAddCardHandler AddCardHandler { get; set; }
        public static INewDepositHandler NewDepositHandler { get; set; }
        public static ICloseDepositHandler CloseDepositHandler { get; set; }

        public DepositController(
            ICardsService cardsService, 
            IDepositWaysOfAccumulationService depositWaysOfAccumulationService,
            IDepositTermsService depositTermsService,
            ICurrenciesService currenciesService,
            IAddCardHandler addCardHandler,
            INewDepositHandler newDepositHandler,
            ICloseDepositHandler closeDepositHandler)
        {
            CardsService = cardsService;
            DepositWaysOfAccumulationService = depositWaysOfAccumulationService;
            DepositTermsService = depositTermsService;
            CurrenciesService = currenciesService;
            AddCardHandler = addCardHandler;
            NewDepositHandler = newDepositHandler;
            CloseDepositHandler = closeDepositHandler;
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
              
                var card = AddCardHandler.GetCardByRequisites(model.Id, model.ExpirationMonth, model.ExpirationYear, model.SecretCode);

                if (card == null)
                {
                    return PartialView("_Message", new MessageViewModel("A card with such requisites does not exist.", false));
                }

                AddCardHandler.SetCardOwnerId(card,User.Identity.GetUserId());

                return RedirectToAction("GeneralInfoContent");
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
            var cards = CardsService.CreateUserCardsByCurrencyList(userId, termsId);
            var waysOfAccumulation = DepositWaysOfAccumulationService.GetList();
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
                var card = NewDepositHandler.GetCardById(model.CardId);

                if (CardHasEnoughFunds(card.Balance, model.Amount))
                {
                    var newDeposit = NewDepositHandler
                        .CreateNewDeposit(model.Amount, model.WayOfAccumulationId, userId,termsId, card.Id);

                    NewDepositHandler.DecreaseCardBalanceByDepositAmount(newDeposit.InitialAmount, card.Id);

                    string cardHistoryDescription = String.Format("Opening deposit #{0} of {1} ({2}).",
                        newDeposit.Id, newDeposit.Balance, NewDepositHandler.GetCurrencyByDepositTermsId(newDeposit.TermId).Abbreviation);
                        
                    NewDepositHandler.AddCardHistoryRecord(card.Id, cardHistoryDescription);
                }
                else
                {
                    return PartialView("_Message", new MessageViewModel("Not enough money.", false));
                }

                return PartialView("_Message", new MessageViewModel("Deposit has been opened successfully.", true));
            }
            

            var cards = CardsService.CreateUserCardsByCurrencyList(userId, termsId);
            var waysOfAccumulation = DepositWaysOfAccumulationService.GetList();
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
            var deposit = CloseDepositHandler.GetDepositById(depositId);

            CloseDepositHandler.IncreaseCardBalanceByDepositBalance(deposit.Balance, cardId);

            CloseDepositHandler.SetDepositState("Closed", depositId);

            var cardHistoryDescription = String.Format("Closing deposit #{0}. Income: {1} ({2}).",
                deposit.Id, deposit.Balance, CloseDepositHandler.GetCurrencyByDepositTermsId(deposit.TermId).Abbreviation);
                
            CloseDepositHandler.AddCardHistoryRecord(cardId, cardHistoryDescription);

            return PartialView("_Message", new MessageViewModel("Deposit has been closed successfully.", true));
        }
    }
}