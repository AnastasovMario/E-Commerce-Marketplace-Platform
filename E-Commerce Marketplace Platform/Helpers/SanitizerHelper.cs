using E_CommerceMarketplace.Core.Constants;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Marketplace_Platform.Helpers
{
    public class SanitizerHelper
    {
        private readonly HtmlSanitizer _sanitizer;
        private readonly ILogger _logger;

        public SanitizerHelper(ILogger<SanitizerHelper> logger)
        {
            _sanitizer = new HtmlSanitizer();
            _logger = logger;
        }

        public string Sanitize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            var sanitzedString = _sanitizer.Sanitize(input);

            if (string.IsNullOrEmpty(sanitzedString))
            {
                _logger.LogError($"Attemtped to pass malicious script to the database.");
                throw new ArgumentException("Attempted to symbols that are not permitted.");
            }

            return sanitzedString;
        }
    }
}
