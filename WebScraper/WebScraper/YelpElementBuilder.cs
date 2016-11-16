using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    /// <summary>
    /// Builds a dictionary with keys named after the elements I will be selecting 
    /// using html agility
    /// </summary>
    public class YelpElementBuilder : ElementBuilder
    {
        public YelpElementBuilder()
        {
            element = new Element();
        }
        public override void BuildElementDictonary()
        {
            element["Main"] = new HtmlElement {Name = "Main",xPaths =  "//ul[@class='ylist ylist-bordered search-results']" };
            element["Name"] = new HtmlElement{ Name = "Name", xPaths = "//h3[@class='search-result-title']/span/a/span"};
            element["ImageAnchor"] = new HtmlElement{ Name = "ImageAnchor", xPaths = "//div[@class='photo-box pb-90s']/a"};
            element["ImageLink"] = new HtmlElement { Name = "ImageLink", xPaths = "//div[@class='photo-box pb-90s']/a/img" };
            element["Category"] = new HtmlElement{Name = "Category", xPaths = "//span[@class='category-str-list']"};
            element["Address"] = new HtmlElement{ Name = "Address",xPaths = "//address"};
            element["Phone"] = new HtmlElement{ Name = "Phone", xPaths = "//span[@class='biz-phone']" };
        }
    }
}
