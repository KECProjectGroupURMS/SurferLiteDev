﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;

namespace WCFServiceSurferlite
{
    public static class LogDepartment
    {
        // Location of the Log on the server
        static string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Log.txt");

        // Add the Logtext to the Log file
        public static void Log(string logText)
        {
      
            logText = DateTime.UtcNow+": "+logText;
            StreamWriter w;

            if (filePath != null)
            {
                w = File.AppendText(filePath);
                w.WriteLine(logText);
                w.Flush();
                w.Close();
            }
        }

        // Deletes the Log file
        public static void Clear()
        {
            File.Delete(filePath);
        }
    }
}