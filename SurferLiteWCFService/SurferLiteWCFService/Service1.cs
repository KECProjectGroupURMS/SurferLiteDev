using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//Added for remote webpage manipulation
using HtmlAgilityPack;

namespace SurferLiteWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData()
        {
            // Get a page from remote server
            var webGet = new HtmlWeb();
            var document = webGet.Load("http://www.microsoft.com");
            
            var metaTags = document.DocumentNode.SelectNodes("//meta");
            
            var output = new StringBuilder();

            if (metaTags != null)
            {
               foreach (var tag in metaTags)
               {
                  if (tag.Attributes["name"] != null && tag.Attributes["content"] != null)
                  {
                     output.Append(tag.Attributes["name"].Value);
                     output.Append(tag.Attributes["content"].Value);
                     output.Append("<<<BREAK>>>");
                  }
               }
            }
            
            // return answer
            return string.Format("{0}", output);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
