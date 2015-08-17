using System;
using DomainLogic.Model;

namespace DomainLogic
{
    public interface IAddCardHandler : IDisposable
    {
        Cards GetCardByRequisites(string id, string expirationMonth, string expirationYear, string secretCode);
    }
}
