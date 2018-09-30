using System;
using System.Collections.Generic;

namespace BibliId.Web.Models.Entities
{
    public partial class Account
    {
        public Account()
        {
            AccountClaim = new HashSet<AccountClaim>();
            AccountEmail = new HashSet<AccountEmail>();
            TokenRevocation = new HashSet<TokenRevocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanLogin { get; set; }
        public string LockoutReason { get; set; }

        public ICollection<AccountClaim> AccountClaim { get; set; }
        public ICollection<AccountEmail> AccountEmail { get; set; }
        public ICollection<TokenRevocation> TokenRevocation { get; set; }
    }
}
