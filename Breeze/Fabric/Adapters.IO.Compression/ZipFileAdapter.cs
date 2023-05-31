using System.IO;
using System.IO.Compression;

namespace Dywham.Breeze.Fabric.Adapters.IO.Compression
{
    public class ZipFileAdapter : IZipFileAdapter
    {
        private readonly IInputOutputAdapter _inputOutputAdapter;


        public ZipFileAdapter(IInputOutputAdapter inputOutputAdapter)
        {
            _inputOutputAdapter = inputOutputAdapter;
        }


        public bool TryDecompressToDirectory(string source, out string location)
        {
            location = "";

            if (!_inputOutputAdapter.GetFileExist(source)) return false;

            location = DecompressToDirectory(source);

            return true;
        }

        public string DecompressToDirectory(string source)
        {
            return DecompressToDirectory(source, _inputOutputAdapter.CreateDirectoryInTempPath());
        }

        public string DecompressToDirectory(string source, string destination)
        {
            if (!File.Exists(source))
            {
                throw new FileNotFoundException($"Compressed file does not exist: {source}");
            }

            ZipFile.ExtractToDirectory(source, destination);

            return destination;
        }

        public string CompressDirectory(string source, string destination)
        {
            destination = _inputOutputAdapter.PrepareDirectoryDestinationForCopy(source, destination);

            ZipFile.CreateFromDirectory(source, destination);

            return destination;
        }
    }
}