using Ganss.Xss;

namespace E_Commerce_Marketplace_Platform.Helpers
{
    public class SanitizerHelper
    {
        private readonly HtmlSanitizer _sanitizer;

        public SanitizerHelper()
        {
            _sanitizer= new HtmlSanitizer();
        }

        public string Sanitize(string input)
        {
            var sanitzedString = _sanitizer.Sanitize(input);
            return sanitzedString;
        }
    }
}
