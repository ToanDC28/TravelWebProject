using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DAO
{
    public class TransactionDAO
    {
        private static TransactionDAO instance = null!;
        private readonly TravelWebContext _context = null;

        private TransactionDAO()
        {
            _context = new TravelWebContext();
        }

        public static TransactionDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransactionDAO();
                }
                return instance;
            }
        }

        public List<TransactionInfo> GetListTransaction()
        {
            try {
                return _context.TransactionInfos.ToList();
            } catch (Exception e) {
                Console.WriteLine(e);
                return new List<TransactionInfo>();
            }
        }

        public void AddTransaction(TransactionInfo transactionInfo)
        {
            try {
                _context.TransactionInfos.Add(transactionInfo);
                _context.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}