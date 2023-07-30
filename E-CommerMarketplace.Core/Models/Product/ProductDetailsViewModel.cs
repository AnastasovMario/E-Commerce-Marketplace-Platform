namespace E_CommerceMarketplace.Core.Models.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string? Description { get; init; }

        public string ImageUrl { get; init; } = null!;
    }
}
