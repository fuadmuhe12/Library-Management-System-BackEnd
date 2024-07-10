using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Library_Management_System_BackEnd.Helper.Json
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = JToken.Load(reader).ToString();
            return DateOnly.Parse(value);
        }

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(Format));
        }
    }
}