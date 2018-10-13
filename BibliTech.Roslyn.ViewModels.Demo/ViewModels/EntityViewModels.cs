namespace ViewModels
{

    public partial class AccountEntityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanLogin { get; set; }
        public string LockoutReason { get; set; }
    }



    public partial class AccountClaimEntityViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }



    public partial class AccountEmailEntityViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; }
        public bool Confirmed { get; set; }
        public int LoginServiceId { get; set; }
        public string ExternalAccountId { get; set; }
        public string ExternalAccountInfo { get; set; }
    }




    public partial class LoginClientEntityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public string RedirectUri { get; set; }
    }



    public partial class LoginServiceEntityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }



    public partial class TokenRevocationEntityViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string TokenHash { get; set; }
        public string Reason { get; set; }
    }




}
