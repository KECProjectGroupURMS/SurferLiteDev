using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceSurferlite
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceSurferLite" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceSurferLite.svc or ServiceSurferLite.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSurferLite : IServiceSurferlite
    {
        public string GetData(int value)
        {
            // TEST: DELETE LATER
            LogDepartment.Log("Done creation process");

            return string.Format("You entered: {0}", value);
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
