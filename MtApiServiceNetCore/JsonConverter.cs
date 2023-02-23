using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MtApiServiceNetCore
{
    public class JsonConverter
    {
        public class Int64JsonConverter : JsonConverter<long>
        {
            public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    return long.Parse(reader.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                }
                decimal decimalValue;
                if (reader.TryGetDecimal(out decimalValue))
                {
                    // used most of the time
                    return Convert.ToInt64(decimalValue);
                }
                long longValue;
                if (reader.TryGetInt64(out longValue))
                {
                    return longValue;
                }

                throw new JsonException("Could not read long value since it is in an unexpected format");
            }

            public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) => throw new NotImplementedException();
        }
        public class UInt64JsonConverter : JsonConverter<ulong>
        {
            public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    return ulong.Parse(reader.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture);
                }
                ulong ulongValue;
                if (reader.TryGetUInt64(out ulongValue))
                {
                    // used most of the time
                    return ulongValue;
                }
                decimal decimalValue;
                if (reader.TryGetDecimal(out decimalValue))
                {
                    return Convert.ToUInt64(decimalValue);
                }
                throw new JsonException("Could not read ulong value since it is in an unexpected format");
            }

            public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options) => throw new NotImplementedException();
        }
    }
}
