using E_CommerceMarketplace.Core.Contracts;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace E_CommerceMarketplace.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IProductModel product)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(product.Name.Replace(" ", "-"));
            sb.Append("-");
            sb.Append(GetImageUrl(product.ImageUrl));

            return sb.ToString();
        }

        private static string GetImageUrl(string imageUrl)
        {
            string result = string
                .Join("-", imageUrl.Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Take(3));

            return Regex.Replace(imageUrl, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
