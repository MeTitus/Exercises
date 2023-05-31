using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Fabric.Adapters.IO
{
    public class InputOutputAdapter : IInputOutputAdapter
    {
        private readonly IUniqueIdentifierGeneratorAdapter _uniqueIdentifierGeneratorAdapter;


        public InputOutputAdapter(IUniqueIdentifierGeneratorAdapter uniqueIdentifierGeneratorAdapter)
        {
            _uniqueIdentifierGeneratorAdapter = uniqueIdentifierGeneratorAdapter;
        }


        public string Copy(string sourceFileName, bool overwriteIfNewer = true)
        {
            return Copy(sourceFileName, Path.GetTempPath(), overwriteIfNewer);
        }

        public string Copy(string sourceFileName, string destDirectory, bool overwriteIfNewer = true)
        {
            if (!new FileInfo(sourceFileName).Exists)
            {
                throw new FileNotFoundException("File not found", sourceFileName);
            }

            var file = new FileInfo(sourceFileName);
            var destFile = new FileInfo(Path.Combine(destDirectory, file.Name));

            file.CopyTo(destFile.FullName, overwriteIfNewer);

            return destFile.FullName;
        }

        public void CreateFile(string filename)
        {
            using (File.Create(filename)) { }
        }

        public bool GetDirectoryExist(string path)
        {
            return Directory.Exists(path);
        }

        public string PrepareDirectoryDestinationForCopy(string source, string destination)
        {
            //Relative paths are not supported 
            if (!Path.IsPathFullyQualified(destination))
            {
                destination = Path.Combine(Directory.GetCurrentDirectory(), destination);
            }

            var destinationDirectoryOnly = Path.GetDirectoryName(destination) ?? "";

            //Try to create it
            if (!Directory.Exists(destinationDirectoryOnly))
            {
                Directory.CreateDirectory(destinationDirectoryOnly);
            }
            else if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            return destination;
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string CreateDirectoryInTempPath()
        {
            return Directory.CreateDirectory(Path.Combine(Path.GetTempPath(),
                _uniqueIdentifierGeneratorAdapter.Generate().ToString())).FullName;
        }

        public string CreateDirectoryInTempPath(string name)
        {
            var path = Path.Combine(Path.GetTempPath(), name);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            return Directory.CreateDirectory(path).FullName;
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public bool GetFileExist(string file)
        {
            return new FileInfo(file).Exists;
        }

        public Task<string[]> ReadAllLinesAsync(string path, CancellationToken token = default)
        {
            return !File.Exists(path)
                ? Task.FromResult(new string[] { })
                : File.ReadAllLinesAsync(path, token);
        }

        public Task AppendAllLinesAsync(string path, string text, CancellationToken token = default)
        {
            return File.AppendAllLinesAsync(path, new List<string> { text }, token);
        }

        public void DeleteDirectory(string path)
        {
            Directory.Delete(path);
        }

        public void DeleteFile(string file)
        {
            File.Delete(file);
        }
    }
}