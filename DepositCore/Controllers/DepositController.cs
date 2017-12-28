using System;
using System.Threading.Tasks;
using DepositCore.Models;
using DepositDatabaseCore.Model;
using DomainLogic;
using DomainLogic.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Card = DomainLogic.Model.Card;
using DepositState = DomainLogic.Model.DepositState;


namespace DepositCore.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class DepositController : Controller
    {
        public static IConfiguration Configuration { get; private set; }
        public static UserManager<DepositDatabaseCore.Model.AspNetUser> UserManager { get; private set; }
        public static ICardsService CardsService { get; private set; }
        public static IDepositWaysOfAccumulationService DepositWaysOfAccumulationService { get; private set; }
        public static IDepositTermsService DepositTermsService { get; private set; }
        public static ICurrenciesService CurrenciesService { get; private set; }
        public static IAddCardHandler AddCardHandler { get; private set; }
        public static INewDepositHandler NewDepositHandler { get; private set; }
        public static ICloseDepositHandler CloseDepositHandler { get; private set; }
        
        public DepositController(
            IConfiguration configuration,
            UserManager<DepositDatabaseCore.Model.AspNetUser> userManager, 
            ICardsService cardsService,
            IDepositWaysOfAccumulationService depositWaysOfAccumulationService,
            IDepositTermsService depositTermsService,
            ICurrenciesService currenciesService,
            IAddCardHandler addCardHandler,
            INewDepositHandler newDepositHandler,
            ICloseDepositHandler closeDepositHandler)
        {
            Configuration = configuration;
            UserManager = userManager;
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
        public ActionResult AddCard(Card serializedCard)
        {
            if (ModelState.IsValid)
            {
                ViewData["modelIsValid"] = true;
              
                var card = AddCardHandler.GetCardByRequisites(serializedCard.Id, serializedCard.ExpirationMonth, serializedCard.ExpirationYear, serializedCard.SecretCode);

                if (card == null)
                {
                    return PartialView("_Message", new MessageViewModel("A card with such requisites does not exist.", false));
                }

                AddCardHandler.SetCardOwnerId(card, UserManager.GetUserId(User));

                return RedirectToAction("GeneralInfoContent");
            }
            

            ViewData["modelIsValid"] = false;
            return PartialView("AddCard");
        }

#endregion

#region OpenNewDeposit actions

        public ActionResult NewDeposit(int termsId)
        {
            //var termsId = Convert.ToInt32(Request.Query["termsId"]);
            ViewData["termsId"] = termsId;

            var userId = UserManager.GetUserId(User);
            var cards = CardsService.CreateUserCardsByCurrencyList(userId, termsId);
            var waysOfAccumulation = DepositWaysOfAccumulationService.GetList();
            var model = new NewDepositViewModel(cards, waysOfAccumulation);

            return PartialView("NewDeposit", model);
        }

        [HttpPost]
        public ActionResult NewDeposit(NewDepositViewModel model)
        {
            var termsId = Convert.ToInt32(Request.Form["termsId"]);
            ViewData["termsId"] = termsId;
            var userId = UserManager.GetUserId(User);

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

            CloseDepositHandler.SetDepositState(DepositState.ClosedDepositStateName, depositId);

            var cardHistoryDescription = String.Format("Closing deposit #{0}. Income: {1} ({2}).",
                deposit.Id, deposit.Balance, CloseDepositHandler.GetCurrencyByDepositTermsId(deposit.TermId).Abbreviation);
                
            CloseDepositHandler.AddCardHistoryRecord(cardId, cardHistoryDescription);

            return PartialView("_Message", new MessageViewModel("Deposit has been closed successfully.", true));
        }
    }
}