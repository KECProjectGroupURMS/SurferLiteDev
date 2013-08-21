using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceSurferlite
{
    public class ClientContactDepartment
    {
        private string clientInfo;
        
        public string clientInformation
        {
            get
            {
                return clientInfo;
            }
            set
            {
                clientInfo = value;
            }
        }

        public InternetContactDepartment InternetContactDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        public UserDataStoreDepartment UserDataStoreDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            { 
            }
        }
    }
}