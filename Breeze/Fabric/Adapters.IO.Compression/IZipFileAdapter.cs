namespace Dywham.Breeze.Fabric.Adapters.IO.Compression
{
    public interface IZipFileAdapter : IAdapter
    {
        bool TryDecompressToDirectory(string source, out string location);

        string DecompressToDirectory(string source);

        string DecompressToDirectory(string source, string destination);

        string CompressDirectory(string source, string destination);
    }
}