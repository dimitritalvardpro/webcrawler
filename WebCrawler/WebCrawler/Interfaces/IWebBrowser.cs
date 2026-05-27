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

        /// <summary>
        /// Resolve the path of the child page with the parent path
        /// if the href is an external URL then returns it
        /// </summary>
        /// <param name="currentPage">URL of the current page</param>
        /// <param name="href">Href provided for the child page</param>
        /// <returns>The resolved path</returns>
        string ResolvePath(string currentPage, string href);
    }
}
