using System.Threading;
using System.Threading.Tasks;

namespace Dywham.Breeze.Fabric.Adapters.IO
{
    public interface IInputOutputAdapter : IAdapter
    {
        string Copy(string sourceFileName, bool overwriteIfNewer = true);

        string Copy(string sourceFileName, string destDirectory, bool overwriteIfNewer = true);

        public void CreateFile(string filename);

        void CreateDirectory(string path);

        string CreateDirectoryInTempPath();

        string CreateDirectoryInTempPath(string name);

        string Combine(params string[] paths);

        bool GetFileExist(string file);

        bool GetDirectoryExist(string path);

        string PrepareDirectoryDestinationForCopy(string source, string destination);

        string GetFileName(string path);

        Task<string[]> ReadAllLinesAsync(string path, CancellationToken token = default);

        Task AppendAllLinesAsync(string path, string text, CancellationToken token = default);

        void DeleteDirectory(string path);

        void DeleteFile(string file);
    }
}