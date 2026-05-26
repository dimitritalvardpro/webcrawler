namespace WebCrawler.Interfaces
{
    /// <summary>
    /// Web Browser Interface
    /// </summary>
    public interface IWebBrowser
    {
        /// <summary>
        /// Returns the HTML content of the given HTML page
        /// </summary>
        /// <param name="url">URL of the HTML page to visit</param>
        /// <returns>The HTML content, or null if the page could not be visited</returns>
        string GetHtml(string url);
    }
}
