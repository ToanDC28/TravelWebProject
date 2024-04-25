using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using TravelWebProject.repo.TransactionRepo;

namespace TravelWebProject.service.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionService(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }
        public List<TransactionInfo> GetListTransaction()
        {
            return _transactionRepo.GetListTransaction();
        }

        public void AddTransaction(TransactionInfo transactionInfo)
        {
            _transactionRepo.AddTransaction(transactionInfo);
        }
    }
}