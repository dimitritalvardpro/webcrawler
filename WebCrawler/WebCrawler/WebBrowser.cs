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
            string currentDirectory = Path.GetDirectoryName(currentPage)!;

            string combinedPath = Path.Combine(currentDirectory, href);

            return Path.GetFullPath(combinedPath);
        }
    }
}
