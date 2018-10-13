using System;
using System.Collections.Generic;

namespace BibliId.Web.Models.Entities
{
    public partial class AccountEmail
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public bool Confirmed { get; set; }
        public int LoginServiceId { get; set; }
        public string ExternalAccountId { get; set; }
        public string ExternalAccountInfo { get; set; }

        public Account Account { get; set; }
        public LoginService LoginService { get; set; }
    }
}
