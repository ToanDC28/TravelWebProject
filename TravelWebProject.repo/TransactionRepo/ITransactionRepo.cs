using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace TravelWebProject.repo.TransactionRepo
{
    public interface ITransactionRepo
    {
        public List<TransactionInfo> GetListTransaction();
        public void AddTransaction(TransactionInfo transactionInfo);
    }
}