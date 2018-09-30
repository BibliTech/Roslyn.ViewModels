using System;
using System.Collections.Generic;

namespace BibliId.Web.Models.Entities
{
    public partial class LoginService
    {
        public LoginService()
        {
            AccountEmail = new HashSet<AccountEmail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<AccountEmail> AccountEmail { get; set; }
    }
}
