namespace Coin.Web.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
        string Slug { get; set; }
    }
}