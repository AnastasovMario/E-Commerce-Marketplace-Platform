using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;

namespace E_CommerceMarketplace.Core.Services
{
    public class ItemService : IItemService
	{
		private readonly IRepository repo;
		public ItemService(IRepository _repo)
		{
			repo = _repo;
		}
        //public Order()
        //{
        //    Items = new List<Item>();
        //}

        //[Key]
        //public int Id { get; set; }

        //[Required]
        //public DateTime Date { get; set; }

        //public DateTime? DateCompleted { get; set; }

        //[ForeignKey(nameof(Status))]
        //public int Status_Id { get; set; }

        //public Status Status { get; set; } = null!;

        //[ForeignKey(nameof(Sale))]
        //public int? Sale_Id { get; set; }

        //public Sale? Sale { get; set; } = null!;

        //[ForeignKey(nameof(User))]
        //public string User_Id { get; set; }
        //public ApplicationUser User { get; set; }

        //public List<Item> Items { get; set; }
        public async Task<int> Create(ItemConfirmationModel model, string userId)
		{
            var order = new Order()
            {
                Date = DateTime.UtcNow,
                Status_Id = 2,
                User_Id = userId
            };
            order.Items.Add(new()
            {
                Quantity = model.Quantity,
                Product_Id = model.Product_Id,
                Total = Math.Round(model.Quantity * model.Price),
                Order = order
            });

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            return order.Id;
		}
	}
}
