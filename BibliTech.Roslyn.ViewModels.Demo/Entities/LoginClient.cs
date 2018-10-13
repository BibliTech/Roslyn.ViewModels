using System;
using System.Collections.Generic;

namespace BibliId.Web.Models.Entities
{
    public partial class LoginClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public string RedirectUri { get; set; }
    }
}
