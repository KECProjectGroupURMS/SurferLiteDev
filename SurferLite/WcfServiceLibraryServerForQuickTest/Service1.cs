using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MHTMLBuilder;

//Added for remote webpage manipulation
using HtmlAgilityPack;

namespace WcfServiceLibraryServerForQuickTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ServiceOnAzure : IService1
    {
        public List<string> GetData(string url)
        {
            if (url == "http://")
            {
                url = "http://www.microsoft.com";
            }
            // Get a page from remote server
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);

            var metaTags = document.DocumentNode.SelectNodes("//meta");

            List<string> output = new List<string>();

            

            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (tag.Attributes["name"] != null && tag.Attributes["content"] != null)
                    {
                        output.Add("Name="+tag.Attributes["name"].Value);
                        output.Add("Content="+tag.Attributes["content"].Value);
                    }
                
                }
            }

            
            
            // return answer
            return output;
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
