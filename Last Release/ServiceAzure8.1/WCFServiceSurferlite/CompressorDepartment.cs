using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// For stream
using System.IO;

// For compression
using System.IO.Compression;

namespace WCFServiceSurferlite
{
    public class CompressorDepartment
    {
        Stream compressed;
        public Stream CompressedStream
        {
            get { return compressed; }
            set { compressed = value; }
        }

        public void CompressBytes(byte[] ByteArray)
        {
            //destination stream
            MemoryStream compressedStream = new MemoryStream();
            
            //make new compressor "zip"
            GZipStream zip = new GZipStream(compressedStream, CompressionLevel.Optimal, true);
            zip.Write(ByteArray, 0, ByteArray.Length);
            zip.Close();

            compressedStream.Position = 0;
            CompressedStream = compressedStream;
        }
    }
}