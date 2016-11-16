using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraper
{
    public interface IDocument
    {
        IList<HtmlDocument> Pages
        {
            get;
            set;
        }
        void StartPage(string urls);
        void CreatePages(string[] urls);
    }

    public class Document : IDocument
    {
        private readonly string baseUrl = "https://www.yelp.com";
        private IList<HtmlDocument> pages;
        private Document document;
        public IList<HtmlDocument> Pages
        {
            get { return pages; }
            set { pages = value; }
        }
        public Document()
        {
            pages = new List<HtmlDocument>();
        }

        /// <summary>
        /// Grabs first page of url
        /// </summary>
        /// <param name="url"></param>
        public void StartPage(string url)
        {
            var html = new HtmlDocument();
            html.LoadHtml(new WebClient { Encoding = Encoding.UTF8 }.DownloadString(url));
            Pages.Add(html);
        }

        /// <summary>
        /// From an array of urls
        /// creates a list of htmldocument
        /// object to later be iterated for specific nodes
        /// </summary>
        /// <param name="urls"></param>
        public void CreatePages(string[] urls)
        {
            //int index = 0;
            foreach (var url in urls)
            {
                //if (index ==1)
                //{
                //    break;
                //}
                var html = new HtmlDocument();
                html.LoadHtml(new WebClient { Encoding = Encoding.UTF8 }.DownloadString(baseUrl+url));
                Pages.Add(html);
                //index++;
            }
        }
    }
}
