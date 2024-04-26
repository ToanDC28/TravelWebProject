using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DAO;

namespace TravelWebProject.repo.TransactionRepo
{
    public class TransactionRepo : ITransactionRepo
    {
        public List<TransactionInfo> GetListTransaction() => TransactionDAO.Instance.GetListTransaction();
        public void AddTransaction(TransactionInfo transactionInfo) => TransactionDAO.Instance.AddTransaction(transactionInfo);
    }
}