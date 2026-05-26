using HtmlAgilityPack;
using WebCrawler.Interfaces;

namespace WebCrawler
{
    public class WebCrawler : IWebCrawler
    {
        private IWebBrowser? _browser;

        private static List<string> CrawledPages = [];
        private static List<string> CrawledEmails = [];

        /// <summary>
        /// Crawls a page and its child pages recursively
        /// until the specified depth is reached.
        /// Only distinct emails are returned.
        /// </summary>
        /// <param name="browser">Web browser</param>
        /// <param name="url">>URL of the HTML page to visit</param>
        /// <param name="maximumDepth">Maximum depth to crawl</param>
        /// <returns>List of emails found in the HTML pages and subpages</returns>
        public List<string> GetEmailsInPageAndChildPages(IWebBrowser browser, string url, int maximumDepth)
        {
            _browser = browser;

            Crawl(url, 0, maximumDepth);

            return CrawledEmails;
        }

        /// <summary>
        /// Process the crawling on the given page
        /// </summary>
        /// <param name="currentPageUrl">Current page URL to crawl</param>
        /// <param name="currentDepth">Current depth of the crawling</param>
        /// <param name="maximumDepth">Maximum depth for the crawling</param>
        private void Crawl(string currentPageUrl, int currentDepth, int maximumDepth)
        {
            if (_browser is null)
            {
                return;
            }

            if (maximumDepth >= 0 && currentDepth > maximumDepth)
            {
                return;
            }

            bool keepCrawling = currentDepth < maximumDepth;

            if (!CrawledPages.Contains(currentPageUrl))
            {
                CrawledPages.Add(currentPageUrl);

                string pageContent = _browser.GetHtml(currentPageUrl);

                if (string.IsNullOrWhiteSpace(pageContent))
                {
                    return;
                }

                var childPages = ReadPageHrefAndReturnsChildPages(pageContent, keepCrawling);

                if (keepCrawling)
                {
                    foreach (string childPage in childPages)
                    {
                        string childPageUrl = _browser.ResolvePath(currentPageUrl, childPage);
                        Crawl(childPageUrl, currentDepth + 1, maximumDepth);
                    }
                }
            }
        }

        /// <summary>
        /// Reads the current page to retrieve the child pages and emails :
        /// Parses all anchor tags from the HTML content.
        /// Mailto links are stored as emails.
        /// HTML links are returned only when crawling is enabled.
        /// </summary>
        /// <param name="htmlContent"></param>
        private static List<string> ReadPageHrefAndReturnsChildPages(string htmlContent, bool keepCrawling)
        {
            List<string> childPages = [];

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//a[@href]");

            foreach (var link in links)
            {
                var strLink = link.GetAttributeValue("href", string.Empty);

                if (!string.IsNullOrWhiteSpace(strLink))
                {
                    if (strLink.EndsWith(".html") && keepCrawling)
                    {
                        childPages.Add(strLink);
                    }
                    else if (strLink.StartsWith("mailto:"))
                    {
                        ProcessMailtoHref(strLink);
                    }
                }
            }

            return childPages;
        }

        /// <summary>
        /// Process a href link and add the email in the crawled list
        /// </summary>
        /// <param name="link">Href link to process</param>
        private static void ProcessMailtoHref(string link)
        {
            string email = link["mailto:".Length..];
            if (!CrawledEmails.Contains(email))
            {
                CrawledEmails.Add(email);
            }
        }
    }
}
