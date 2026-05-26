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
    }
}
