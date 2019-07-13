using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace abcBank.Models
{
    public class Customer
    {
        [Key]
        public int CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PhoneNumer { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Account> AccOwned { get; set; }
        //public ICollection<Translog> TransLogs { get; set; }
    }
}
