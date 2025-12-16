namespace Ezzygate.Infrastructure.FileSystem;

public interface IFileSystemProvider
{
    bool FileExists(string path);
    bool DirectoryExists(string path);
    void CreateDirectory(string path);
    byte[] ReadAllBytes(string path);
    void WriteAllBytes(string path, byte[] data);
    string ReadAllText(string path, System.Text.Encoding encoding);
    void WriteAllText(string path, string content, System.Text.Encoding? encoding = null);
    void DeleteFile(string path);
    string[] GetFiles(string path, string searchPattern);
    string GetFileName(string path);
    string GetFileNameWithoutExtension(string path);
    string GetDirectoryName(string path);
    string ChangeExtension(string path, string extension);
    string Combine(params string[] paths);
    FileStream CreateFile(string path, FileMode mode, FileAccess access);
}