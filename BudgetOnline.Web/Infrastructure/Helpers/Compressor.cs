using BudgetOnline.Common.Contracts;

namespace BudgetOnline.Web.Infrastructure.Helpers
{
    public class Compressor : ICompressor
    {
        //private ICSharpCode.SharpZipLib.Zip.Compression.Deflater compressor;

        public byte[] Compress(byte[] data)
        {
            return data;
        }

        public string Compress(string data)
        {
            return data;
        }

        public byte[] Decompress(byte[] data)
        {
            return data;
        }

        public string Decompress(string data)
        {
            return data;
        }
    }
}