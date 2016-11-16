using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{

    /// <summary>
    /// Used to create an instance of the element class
    /// </summary>
    public abstract class ElementBuilder
    {
        protected Element element;

        // Gets Place instance
        public Element Element
        {
            get { return element; }
        }
        public abstract void BuildElementDictonary();
    }
    public class ElementDirector
    {
        // Builder uses a complex series of steps
        public void Construct(ElementBuilder YelpBuilder)
        {
            YelpBuilder.BuildElementDictonary();
        }
    }

}
