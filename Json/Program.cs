using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

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
            person.TypeList.Add(typeof(string));
            person.TypeList.Add(typeof(int));
            person.TypeList.Add(typeof(List<>));
            person.TypeList.Add(null);
            person.List.Add("4", null);
            person.List.Add("5", typeof(string));
            person.ListX.Add(null);
            person.ListX.Add(typeof(Dictionary<,>));
            buttonSource.My = new My2(buttonSource) { X = "X", Y = "Y", Row = person, Type2 = typeof(int), GridCell = new GridCell() { Text = "Language" } };

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
            // Handle inheritance of ComponentJson and Row classes. Or handle Type object.
            return UtilFramework.IsSubclassOf(typeToConvert, typeof(ComponentJson)) || UtilFramework.IsSubclassOf(typeToConvert, typeof(Row)) || typeToConvert == typeof(Type);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(Converter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType, this);
        }
    }

    public enum PropertyEnum { None = 0, Property = 1, List = 2, Dictionary = 3 }

    /// <summary>
    /// Access property, list or dictionary.
    /// </summary>
    public class Property
    {
        public Property(PropertyInfo propertyInfo, object propertyValue)
        {
            // Property
            PropertyEnum = PropertyEnum.Property;
            PropertyType = propertyInfo.PropertyType;
            PropertyValue = propertyValue;
            PropertyValueList = new List<object>(new object[] { propertyValue });

            // List
            if (propertyValue is IList list)
            {
                PropertyEnum = PropertyEnum.List;
                PropertyType = propertyValue.GetType().GetGenericArguments()[0]; // List type
                PropertyValueList = list;
            }

            // Dictionary
            if (propertyValue is IDictionary dictionary)
            {
                PropertyEnum = PropertyEnum.Dictionary;
                PropertyType = propertyValue.GetType().GetGenericArguments()[1]; // Key type
                PropertyValueList = dictionary.Values;
                PropertyDictionary = dictionary;
            }
        }

        /// <summary>
        /// Gets PropertyEnum (property, list or dictionary)
        /// </summary>
        public PropertyEnum PropertyEnum;

        /// <summary>
        /// Gets PropertyType for property, list and dictionary.
        /// </summary>
        public Type PropertyType;

        /// <summary>
        /// Gets PropertyValueList for property, list and dictionary.
        /// </summary>
        public ICollection PropertyValueList;

        /// <summary>
        /// Gets PropertyValue for property.
        /// </summary>
        public object PropertyValue;

        /// <summary>
        /// Gets PropertyDictionary for dictionary to access key, value pair with DictionaryEntry.
        /// </summary>
        public IDictionary PropertyDictionary;
    }

    public class ComponentJsonReference
    {
        public int? IdReference { get; set; }
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
            // Deserialize Type object
            if (typeToConvert == typeof(Type))
            {
                var typeName = JsonSerializer.Deserialize<string>(ref reader);
                return (T)(object)Type.GetType(typeName);
            }

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
                    var propertyValue = JsonSerializer.Deserialize(valueList[propertyInfo.Name].GetRawText(), propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(result, propertyValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Replace ComponentJson with ComponentJsonReference.
        /// </summary>
        private object ComponentJsonReferenceReplace(PropertyInfo propertyInfo, object propertyValue)
        {
            object result = propertyValue;
            bool isComponentJsonList = UtilFramework.IsSubclassOf(propertyInfo.DeclaringType, typeof(ComponentJson)) && propertyInfo.Name == nameof(ComponentJson.List);
            if (isComponentJsonList == false)
            {
                Property property = new Property(propertyInfo, propertyValue);
                if (UtilFramework.IsSubclassOf(property.PropertyType, typeof(ComponentJson)))
                {
                    switch (property.PropertyEnum)
                    {
                        case PropertyEnum.List:
                            var list = new List<ComponentJsonReference>();
                            foreach (ComponentJson item in property.PropertyValueList)
                            {
                                list.Add(new ComponentJsonReference() { IdReference = item?.Id });
                            }
                            result = list;
                            break;
                        case PropertyEnum.Dictionary:
                            var dictionary = new Dictionary<string, ComponentJsonReference>();
                            foreach (DictionaryEntry item in property.PropertyDictionary)
                            {
                                string key = (string)item.Key;
                                ComponentJson componentJson = (ComponentJson)item.Value;
                                dictionary.Add(key, new ComponentJsonReference { IdReference = componentJson?.Id });
                            }
                            result = dictionary;
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// PropertyType has to be (ComponentJson, Row or Type). Or PropertyType and PropertyValue type need to match. Applies also for list or dictionary.
        /// </summary>
        private void ValidatePropertyAndValueType(PropertyInfo propertyInfo, object propertyValue)
        {
            var property = new Property(propertyInfo, propertyValue);
            
            Type propertyType = property.PropertyType;
            ICollection propertyValueList = property.PropertyValueList;

            // Property type is of type ComponentJson or Row. For example property type object would throw exception.
            if (UtilFramework.IsSubclassOf(propertyType, typeof(ComponentJson)) || UtilFramework.IsSubclassOf(propertyType, typeof(Row)))
            {
                return;
            }
            // Property type is class Type. Property value typeof(int) is class RuntimeType (which derives from class Type)
            if (propertyType == typeof(Type))
            {
                return;
            }

            foreach (var item in propertyValueList)
            {
                if (item != null)
                {
                    // Property type is equal to value. No inheritance.
                    if (!(UtilFramework.TypeUnderlying(propertyType) == item.GetType()))
                    {
                        throw new Exception(string.Format("Combination property type and value type not supported! (PropertyName={0}.{1}; PropertyType={2}; ValueType={3}; Value={4};)", propertyInfo.DeclaringType.Name, propertyInfo.Name, propertyType.Name, item.GetType().Name, item));
                    }
                }
            }
        }

        /// <summary>
        /// Serialize ComponentJson or Row objects.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // Serialize Type object
            if (typeof(T) == typeof(Type))
            {
                if (value is Type type)
                {
                    JsonSerializer.Serialize<string>(writer, type.FullName);
                }
                return;
            }
            
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
                    ValidatePropertyAndValueType(propertyInfo, propertyValue);

                    // TODO Write reference ComponentJson.Id if not Component.List
                    // TODO Distinct serialize for client and for server.
                    // TODO Check ComponentJson reference is in same composition- graph.

                    // Serialize property value
                    writer.WritePropertyName(propertyInfo.Name);
                    JsonSerializer.Serialize(writer, propertyValue, propertyInfo.PropertyType, options);
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

        internal static void Assert(bool isAssert)
        {
            Assert(isAssert, "Assert!");
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