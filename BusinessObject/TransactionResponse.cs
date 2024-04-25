using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace BusinessObject
{
    public class TransactionResponse
    {
        public string totalRows { get; set; }
        public string maxAcentrysmo { get; set; }
        public List<TransactionInfo> transactionInfos;
    }
}