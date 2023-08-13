using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
    internal class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            //builder.HasData(CreateStatuses());
        }

        public List<Status> CreateStatuses()
        {
            List<Status> statuses = new List<Status>()
            {
                new Status()
                {
                    Id = 1,
                    Description = "Unavailable"
                },
                new Status()
                {
                    Id = 2,
                    Description = "Stocked"
                },
            };

            return statuses;
        }
    }
}
