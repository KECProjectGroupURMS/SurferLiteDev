using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client81
{
    // This class gets and queue request from users and also give data in response to each request.
    class CustomerDepartment
    {
        internal CallerDepartment CallerDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        internal CompressionDepartment CompressionDepartment
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// This method is async method and is used to display page through SurferLite Service
        /// </summary>
        /// <param name="URLString">The URL in string which you want to display in WebViewBrowse</param>
        private async void NavigateThroughSurferLite(string URLString)
        {
            try
            {
                Uri URL = new Uri(URLString);
                //ProgressRingLoad.IsActive = true;

                //Although Service Contract says GetHtml(URL) returns stream. It automatically is byte[].
                //XXXXXX: Stream pullStream = await client.GetHtmlAsync(URL);

                //byte[] pullStream = await client.GetHtmlAsync(URL);

                // Update total data
                //dataSize += pullStream.Length;

                // Displaying size of byte[] got from service
                //TextBlockSize.Text = pullStream.Length.ToString() + " B";

                // Decompressing pullStream to get actual decompressed data
                //string result = CompressionDepartment.DecompressBytes(pullStream);

                // To view in Html Source
                //TextBlockSource.Text = result;

                // result is ready and is displayed in webview
                //WebViewBrowse.NavigateToString(result);
            }

            catch (Exception e)
            {
                // There is some error. This needs modification so Exception is exact
                //****WebViewBrowse.NavigateToString(e.ToString());
            }

            //*** ProgressRingLoad.IsActive = false;

        }

    }
}
