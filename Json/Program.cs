using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var button = new Button(null) { TextHtml = null };
            var options = new JsonSerializerOptions { Converters = { new Factory() } };
            options.IgnoreNullValues = true;
            options.WriteIndented = true;


            var d = System.Text.Json.JsonSerializer.Serialize(button, button.GetType(), options);
        }
    }

    public class Factory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert == typeof(string))
            {
                return true;
            }
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(string))
            {
                // var keyType = typeToConvert.GenericTypeArguments[0];
                var converterType = typeof(Converter<>).MakeGenericType(typeof(string));

                return (JsonConverter)Activator.CreateInstance(converterType);

            }
            return null;
        }
    }

    public class Converter<T> : JsonConverter<T>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return base.CanConvert(typeToConvert);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
        }
    }
}
