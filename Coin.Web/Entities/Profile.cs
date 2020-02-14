namespace Coin.Web.Entities
{
    public class Profile
    {
        public long Id { get; set; }
        public string Slug { get; set; } 
        public string IdentityId { get; set; }   
        public ApplicationUser Identity { get; set; }  // navigation property
    }
}