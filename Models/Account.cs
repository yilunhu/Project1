using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace abcBank.Models
{
    public enum Acctype {
        Checking,
        Business,
        TermDeposit,
        Loan
    }
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public Acctype AccountType { get; set; }
        public decimal Balance { get; set; }
        public string InterestRate;
        public decimal overdraft { get; set; }
        //public Customer CustID { get; set; }
        public virtual  int CustomerID { get; set; }

        public virtual Customer Customer  { get; set; }

        public DateTime LastAccess { get; set; }
        public virtual ICollection<Translog> Transaction { get; set; }
    }
}
