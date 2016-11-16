using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    /// <summary>
    /// Indexer class used to manually name my keys
    /// to map the elements I wanted on the html document
    /// </summary>
    public class Element
    {
        private readonly Dictionary<string, HtmlElement> _attributes = new Dictionary<string, HtmlElement>();
        public Element() { }
        // Indexer
        public HtmlElement this[string key]
        {
            get { return _attributes[key]; }
            set { _attributes[key] = value; }
        }

        public void Show()
        {
            foreach (var val in _attributes)
            {
                Console.WriteLine("Key : {0} Value : {1}", val.Key, val.Value);
            }
       
        }
    }
    public class HtmlElement
    {
        public string Name { get; set; }
        public string xPaths { get; set; }
    }

   
}
