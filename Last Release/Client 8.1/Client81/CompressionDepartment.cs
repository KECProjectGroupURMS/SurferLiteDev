using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//for streams
using System.IO;

//for compression
using System.IO.Compression;

namespace Client81
{
    //This class handles decompression of data form receiver and give it to customer
    class CompressionDepartment
    {
        string dataResult;

        public string DecompressBytes(byte[] compressedByte)
        {
            // for holding decompressed byte
            string result;

            //Prepare for decompress
            MemoryStream ms = new MemoryStream(compressedByte);
            GZipStream compressor = new GZipStream(ms, CompressionMode.Decompress);

            //Reset variable to collect uncompressed result
            compressedByte = new byte[9999999];

            //Decompress and rByte is size of decompressed stream
            int rByte = compressor.Read(compressedByte, 0, compressedByte.Length);

            //CompressedByte is now decompressed
            MemoryStream decompressedStream = new MemoryStream(compressedByte);

            //converting to string to return
            StreamWriter writer = new StreamWriter(decompressedStream);
            writer.Write(compressedByte);
            writer.Flush();

            decompressedStream.Position = 0;
            StreamReader reader = new StreamReader(decompressedStream);
            result = reader.ReadToEnd();

            //XXXXXXXXX: TextBlockSize.Text = result.Length.ToString()+" B"; //This gives size of decompressedStream which is 9999999
            //******TextBlockAnsSize.Text = rByte.ToString() + " B";

            // Cleanup if they are to be used again
            compressor.Dispose();

            return result;

            ////Uncomment these if you want to use String instead of string (Some modification have to be done may be)

            ////Transform byte[] unzip data to string
            //System.Text.StringBuilder sB = new System.Text.StringBuilder(rByte);

            ////Read the number of bytes GZipStream red and do not a for each bytes in
            ////resultByteArray;

            //for (int i = 0; i < rByte; i++)
            //{
            //    sB.Append((char)compressedByte[i]);
            //}

            //compressor.Dispose();
            //ms.Dispose();
            //TextBlockAnsSize.Text = sB.Length.ToString()+" B";

            //return sB.ToString();

        }

    }
}
