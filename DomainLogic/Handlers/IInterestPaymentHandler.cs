using System;
using DomainLogic.Model;

namespace DomainLogic.Handlers
{
    public interface IInterestPaymentHandler
    {
        Deposits GetActiveDeposits();
        void ExtendDeposit(int depositId);
        void AddInterest(int depositId);
        void SetLastInterestPaymentDate(int depositId, DateTime date);
        void SetDepositState(int depositId, string stateName);
    }
}