using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            // Create Button object to serialize
            var buttonSource = new Button(null) { TextHtml = "Hello World!", DateTime = DateTime.Now };
            List<Row> rowList = new List<Row>();
            rowList.Add(new Person() { Name = "Marc" });
            rowList.Add(new Person() { Name = "Andrew" });
            var person = new Person() { Hello = "ldcsdcl", Name = "John", Value2 = 232M, Value1 = 88, Value3 = 9.5, RowList = rowList };
            person.RowList2.Add("j", new Row() { Hello = "Myd2" });
            person.RowList2.Add("k", new Person() { Hello = "My3", Name = "Mc" });
            buttonSource.My = new My2(buttonSource) { X = "X", Y = "Y", Row = person, Type2 = typeof(int) };

            // Serialize with Newtonsoft and inheritance
            var jsonNewtonsoftSource = Newtonsoft.Json.JsonConvert.SerializeObject(buttonSource, new Newtonsoft.Json.JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });

            // Serialize with System.Text.Json
            var options = new JsonSerializerOptions();
            options.Converters.Add(new Factory());
            options.WriteIndented = true;
            var jsonSource = JsonSerializer.Serialize(buttonSource, options);

            // Deserialize with System.Text.Json
            var buttonDest = JsonSerializer.Deserialize<ComponentJson>(jsonSource, options);

            // Serialize System.Text.Json deserialized object with Newtonsoft and inheritance again.
            var jsonNewtonsoftDest = Newtonsoft.Json.JsonConvert.SerializeObject(buttonDest, new Newtonsoft.Json.JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });

            // Compare the two
            if (jsonNewtonsoftSource != jsonNewtonsoftDest)
            {
                throw new Exception("Serialization and deserialization failed!");
            }

            Console.WriteLine(jsonSource);
        }
    }

    public class Factory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            // Handle inheritance of ComponentJson and Row classes.
            return UtilFramework.IsSubclassOf(typeToConvert, typeof(ComponentJson)) || UtilFramework.IsSubclassOf(typeToConvert, typeof(Row));
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(Converter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType, this);
        }
    }

    public class Converter<T> : JsonConverter<T>
    {
        public Converter(Factory factory)
        {
            this.Factory = factory;
        }

        public readonly Factory Factory;

        /// <summary>
        /// Deserialize ComponentJson or Row objects.
        /// </summary>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Deserialize Component or Row object
            var valueList = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader);

            // Read type information
            string typeText = valueList["$Type"].GetString();
            Type type = Type.GetType(typeText); // TODO Cache on factory
            T result = (T)(object)Activator.CreateInstance(type); // TODO No parameterless constructor for ComponentJson

            // Loop through properties
            foreach (var propertyInfo in type.GetProperties())
            {
                if (valueList.ContainsKey(propertyInfo.Name))
                {
                    object propertyValue;

                    // Special property
                    if (propertyInfo.PropertyType == typeof(Type))
                    {
                        string typeName = JsonSerializer.Deserialize<string>(valueList[propertyInfo.Name].GetRawText(), options);
                        propertyValue = Type.GetType(typeName);
                    }
                    else
                    {
                        // Normal property
                        propertyValue = JsonSerializer.Deserialize(valueList[propertyInfo.Name].GetRawText(), propertyInfo.PropertyType, options);
                    }

                    propertyInfo.SetValue(result, propertyValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Serialize ComponentJson or Row objects.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // ComponentJson or Row object start
            writer.WriteStartObject();
            
            // Type information
            writer.WritePropertyName("$Type"); // Note: Type information could be omitted if property type is equal to property value type.
            JsonSerializer.Serialize(writer, value.GetType().FullName);

            // Loop through properties
            foreach (var propertyInfo in value.GetType().GetProperties())
            {
                var propertyValue = propertyInfo.GetValue(value);
                bool isIgnoreNullValue = false;
                if (propertyValue == null)
                {
                    isIgnoreNullValue = true;
                }
                if (propertyValue is ICollection list && list.Count == 0)
                {
                    isIgnoreNullValue = true;
                }
                if (!isIgnoreNullValue)
                {
                    bool isPropertyTypeAssert = false;
                    if (UtilFramework.TypeUnderlying(propertyInfo.PropertyType) == propertyValue.GetType())
                    {
                        // Property type is equal to value. No inheritance.
                        isPropertyTypeAssert = true;
                    }
                    else
                    {
                        // Property type is of type ComponentJson or Row. For example property type object would throw exception.
                        if (UtilFramework.IsSubclassOf(propertyInfo.PropertyType, typeof(ComponentJson)) || UtilFramework.IsSubclassOf(propertyInfo.PropertyType, typeof(Row)))
                        {
                            isPropertyTypeAssert = true;
                        }
                    }

                    // Special property
                    if (propertyInfo.PropertyType == typeof(Type))
                    {
                        isPropertyTypeAssert = true; // Property type is class Type. Property value typeof(int) is class RuntimeType (which derives from class Type)
                        propertyValue = propertyValue.ToString();
                    }

                    UtilFramework.Assert(isPropertyTypeAssert, string.Format("Combination property type and value type not supported! (PropertyName={0}; PropertyType={1}; ValueType={2};)", propertyInfo.Name, propertyInfo.PropertyType.Name, propertyValue.GetType().Name));

                    // TODO Write reference ComponentJson.Id if not Component.List
                    // TODO Distinct serialize for client and for server.
                    // TODO Check ComponentJson reference is in same composition- graph.

                    // Serialize property value
                    writer.WritePropertyName(propertyInfo.Name);
                    JsonSerializer.Serialize(writer, propertyValue, options);
                }
            }

            // ComponentJson or Row object end
            writer.WriteEndObject();
        }
    }

    internal static class UtilFramework
    {
        internal static bool IsSubclassOf(Type type, Type typeBase)
        {
            if (type == null)
            {
                return false;
            }
            return type.IsSubclassOf(typeBase) || type == typeBase;
        }

        internal static void Assert(bool isAssert, string exceptionText)
        {
            if (!isAssert)
            {
                throw new Exception(exceptionText);
            }
        }

        /// <summary>
        /// Returns underlying tpye, if any. For example "type = typeof(int?)" returns "typeof(int)".
        /// </summary>
        internal static Type TypeUnderlying(Type type)
        {
            Type result = type;
            Type typeUnderlying = Nullable.GetUnderlyingType(type);
            if (typeUnderlying != null)
            {
                result = typeUnderlying;
            }
            return result;
        }
    }
}