using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.WebApi.WebSocketRelatedStuff
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
                    reader.TokenType));
            }

            var ticks = (long)reader.Value; // milliseconds
            var seconds = ticks / 1000;

            var date = new DateTime(1970, 1, 1);
            return date.AddSeconds(seconds);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime dateTime)
            {
                var epoc = new DateTime(1970, 1, 1);
                var delta = dateTime - epoc;
                if (delta.TotalMilliseconds < 0)
                {
                    throw new ArgumentOutOfRangeException("Unix epoch starts January 1st, 1970");
                }
                ticks = (long)delta.TotalMilliseconds;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(ticks);
        }
    }
}
