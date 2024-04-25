using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class TransactionInfo
    {
        public int InforId { get; set; }
        public string? ArrangementId { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? BookingDate { get; set; }
        public string? ValueDate { get; set; }
        public string? Amount { get; set; }
        public string? Currency { get; set; }
        public string? CreditDebitIndicator { get; set; }
        public string? RunningBalance { get; set; }
        public string? OfsAcctNo { get; set; }
        public string? OfsAcctName { get; set; }
        public string? CreditorBankNameVn { get; set; }
        public string? CreditorBankNameEn { get; set; }
        public int? BookingId { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
