using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Extensions;

public static class JsonExtension
{
    private static readonly JsonSerializerOptions? JsonSettings = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DateOnlyConverter() }
    };

    public static string Serialize(this object obj)
    {
        return JsonSerializer.Serialize(obj, JsonSettings);
    }

    public static T? Deserialize<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, JsonSettings);
    }
}

public class DateOnlyConverter : JsonConverter<DateTime>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}