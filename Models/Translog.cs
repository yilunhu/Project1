using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace abcBank.Models
{
    public enum TransType
    {
        Transfer,
        Deposit,
        Withdraw,
        PayLoan
    }
    public class Translog
    {
        [Key]
        public int TransLogId { get; set; }
        //public Customer custId { get; set; }
        //public Account AccId { get; set; }
        public TransType TransType { get; set; }
        public int? RecieverId { get; set; }
        public decimal TransAmount { get; set; }
        public string Comment { get; set; }

        //public Customer CustID { get; set; }
        public virtual Account Account { get; set; }
        public virtual int AccountID { get; set; }

    }
}
