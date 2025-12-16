using System.Text;

namespace Ezzygate.Infrastructure.Cryptography;

public class Blob
{
    private byte[] _data;
    private Encoding _textEncoding;

    public Blob()
    {
        _data = [];
        _textEncoding = Encoding.Default;
    }

    public Blob(byte[] data) : this()
    {
        _data = data;
    }

    public Blob(string text) : this()
    {
        Text = text;
    }

    public byte this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    public int Size
    {
        get => _data.Length;
        set => _data = new byte[value];
    }

    public byte[] Bytes
    {
        get => _data;
        set => _data = value;
    }

    public Encoding TextEncoding
    {
        get => _textEncoding;
        set => _textEncoding = value;
    }

    public string TextEncodingName
    {
        get => _textEncoding.EncodingName;
        set => _textEncoding = Encoding.GetEncoding(value);
    }

    public string Text
    {
        get => _textEncoding.GetString(_data);
        set => _data = _textEncoding.GetBytes(value);
    }

    public string Ascii
    {
        get => Encoding.ASCII.GetString(_data);
        set => _data = Encoding.ASCII.GetBytes(value);
    }

    public string Unicode
    {
        get => Encoding.Unicode.GetString(_data);
        set => _data = Encoding.Unicode.GetBytes(value);
    }

    public string Utf8
    {
        get => Encoding.UTF8.GetString(_data);
        set => _data = Encoding.UTF8.GetBytes(value);
    }
    
    public string Windows1255 
    { 
        get => Encoding.GetEncoding("windows-1255").GetString(_data); 
        set => _data = Encoding.GetEncoding("windows-1255").GetBytes(value); 
    }

    public string UrlEncode
    {
        get => Uri.EscapeDataString(Utf8);
        set => Utf8 = Uri.UnescapeDataString(value);
    }

    public string Base64
    {
        get => Convert.ToBase64String(_data);
        set => _data = Convert.FromBase64String(value);
    }

    public string Hex
    {
        get => ToHexString(_data);
        set => _data = FromHexString(value);
    }

    public void XorWith(Blob blob)
    {
        Xor(_data, blob.Bytes);
    }

    public static void Xor(byte[] dest, byte[] src)
    {
        ArgumentNullException.ThrowIfNull(dest);
        ArgumentNullException.ThrowIfNull(src);

        for (var i = 0; i < dest.Length; i++)
            dest[i] ^= src[i % src.Length];
    }

    public static string ToHexString(byte[] value, string separator = "")
    {
        var result = new StringBuilder(value.Length * 2);
        for (var i = 0; i < value.Length; i++)
        {
            if (i > 0 && !string.IsNullOrEmpty(separator))
                result.Append(separator);
            result.Append(value[i].ToString("X2"));
        }
        return result.ToString();
    }

    public static byte[] FromHexString(string value, string? separator = "")
    {
        if (string.IsNullOrEmpty(value)) return [];
            
        var step = 2 + (separator?.Length ?? 0);
        var result = new byte[value.Length / step];
            
        for (var i = 0; i < value.Length; i += step)
            result[i / step] = Convert.ToByte(value.Substring(i, 2), 16);
            
        return result;
    }
}