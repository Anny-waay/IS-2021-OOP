using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackupsExtra.Services
{
    public class FileInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(FileSystemInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var jObject = JObject.Load(reader);
            var fullPath = (jObject["FullPath"] ?? throw new InvalidOperationException()).Value<string>();
            return Activator.CreateInstance(objectType, fullPath);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var info = value as FileSystemInfo;
            var obj = info == null
                ? null
                : new
                {
                    FullPath = info.FullName,
                };
            var token = JToken.FromObject(obj);
            token.WriteTo(writer);
        }
    }
}