using System.Text.Json.Serialization;
using System.Text.Json;

namespace Infrastructure.Services;


public class TimeSpanConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (TimeSpan.TryParse(reader.GetString(), out TimeSpan result))
        {
            return result;
        }

        return TimeSpan.Zero; // or throw an exception if parsing fails
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
