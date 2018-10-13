using System;
using System.Collections.Generic;

namespace BibliId.Web.Models.Entities
{
    public partial class TokenRevocation
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string TokenHash { get; set; }
        public string Reason { get; set; }
        public DateTime NaturalExpirationTime { get; set; }
        public DateTime RevokeTime { get; set; }

        public Account Account { get; set; }
    }
}
