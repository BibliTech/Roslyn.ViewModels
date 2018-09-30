using System;
using System.Collections.Generic;

namespace BibliId.Web.Models.Entities
{
    public partial class AccountClaim
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Account Account { get; set; }
    }
}
