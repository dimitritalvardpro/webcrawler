namespace WebCrawler.Interfaces
{
    /// <summary>
    /// Provides methods for crawling web pages and extracting data from them.
    /// </summary>
    public interface IWebCrawler
    {
        /// <summary>
        /// Retrieves all email addresses found in the specified page and its child pages, up to the given crawling depth.
        /// </summary>
        /// <param name="browser">Web browser instance used to navigate and retrieve page content</param>
        /// <param name="url">URL of the HTML page to crawl</param>
        /// <param name="maximumDepth">Maximum depth level to explore child HTML pages</param>
        /// <returns>A list of email addresses found during the crawl</returns>
        List<string> GetEmailsInPageAndChildPages(IWebBrowser browser, string url, int maximumDepth);
    }
}
