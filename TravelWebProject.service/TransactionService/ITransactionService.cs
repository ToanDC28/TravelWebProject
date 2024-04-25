using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.service.TransactionService
{
    public interface ITransactionService
    {
        public List<TransactionInfo> GetListTransaction();
        public void AddTransaction(TransactionInfo transactionInfo);
    }
}