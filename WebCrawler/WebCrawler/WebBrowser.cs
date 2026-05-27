using WebCrawler.Interfaces;

namespace WebCrawler
{
    /// <inheritdoc/>
    public class WebBrowser : IWebBrowser
    {
        /// <inheritdoc/>
        public string GetHtml(string url)
        {
            if (File.Exists(url))
            {
                return File.ReadAllText(url);
            }

            return null!;
        }

        /// <inheritdoc/>
        public string ResolvePath(string currentPage, string href)
        {
            if (IsExternalUrl(href))
            {
                return href;
            }
            else
            {

                string currentDirectory = Path.GetDirectoryName(currentPage)!;
                string combinedPath = Path.Combine(currentDirectory, href);
                return Path.GetFullPath(combinedPath);
            }
        }

        private static bool IsExternalUrl(string url)
        {
            return (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith("www.", StringComparison.OrdinalIgnoreCase));
        }
    }
}
