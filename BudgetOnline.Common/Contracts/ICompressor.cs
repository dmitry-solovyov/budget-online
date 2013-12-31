namespace BudgetOnline.Common.Contracts
{
    public interface ICompressor
    {
        byte[] Compress(byte[] data);
        string Compress(string data);

        byte[] Decompress(byte[] data);
        string Decompress(string data);
    }

    public enum CompressionLevels
    {
        Low,
        Normal,
        Hight
    }
}
