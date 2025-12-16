namespace Ezzygate.Infrastructure.FileSystem;

public class FileSystemProvider : IFileSystemProvider
{
    public bool FileExists(string path) => File.Exists(path);

    public bool DirectoryExists(string path) => Directory.Exists(path);

    public void CreateDirectory(string path) => Directory.CreateDirectory(path);

    public byte[] ReadAllBytes(string path) => File.ReadAllBytes(path);

    public void WriteAllBytes(string path, byte[] data) => File.WriteAllBytes(path, data);

    public string ReadAllText(string path, System.Text.Encoding encoding)
        => File.ReadAllText(path, encoding);

    public void WriteAllText(string path, string content, System.Text.Encoding? encoding = null)
    {
        if (encoding == null)
            File.WriteAllText(path, content);
        else
            File.WriteAllText(path, content, encoding);
    }

    public void DeleteFile(string path) => File.Delete(path);

    public string[] GetFiles(string path, string searchPattern)
        => Directory.GetFiles(path, searchPattern);

    public string GetFileName(string path) => Path.GetFileName(path);

    public string GetFileNameWithoutExtension(string path)
        => Path.GetFileNameWithoutExtension(path);

    public string GetDirectoryName(string path)
        => Path.GetDirectoryName(path) ?? string.Empty;

    public string ChangeExtension(string path, string extension)
        => Path.ChangeExtension(path, extension) ?? path;

    public string Combine(params string[] paths) => Path.Combine(paths);

    public FileStream CreateFile(string path, FileMode mode, FileAccess access)
        => new FileStream(path, mode, access);
}