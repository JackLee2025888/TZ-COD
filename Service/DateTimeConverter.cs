using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Biminno.JZBIM.Service
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.TryParse(reader.GetString(), out var dateTime) ? dateTime : default(DateTime);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class DateTimeNullableConvert : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.TryParse(reader.GetString(), out var dateTime) ? dateTime : default(DateTime?);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public class DoubleConverter : JsonConverter<double>
    {

        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                return double.TryParse(reader.GetString(), out var value) ? value : double.NaN;
            else if (reader.TokenType == JsonTokenType.Number)
                return reader.GetDouble();
            else
                return double.NaN;
        }

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (double.IsNaN(value))
                writer.WriteStringValue(string.Empty);
            writer.WriteStringValue(value.ToString());
        }
    }


}
