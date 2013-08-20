using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceSurferlite
{
    public class DataAnalyzeModifyFilterDepartment
    {
        private string changedByte;
        internal string modifiedByte
        {
            get
            {
                return changedByte;
            }
            set
            {
                changedByte = value;
            }
        }

        public CompressorDepartment CompressorDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void RemoveImage() { }
        public void RemoveHrefs() { }
        public void ChangeHtml() { }
        
    }
}