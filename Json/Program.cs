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
            var person = new Person() { Hello = "ldcsdcl", Name = "John", Value2 = 232M, Value1 = 88, Value3 = 9.5 };
            buttonSource.My = new My2(buttonSource) { X = "X", Y = "Y", Row = person, Type2 = typeof(int), GridCell = new GridCell() { Text = "Language" }, RowList = rowList };
            ((My2)buttonSource.My).RowList2.Add("j", new Row() { Hello = "Myd2" });
            ((My2)buttonSource.My).RowList2.Add("k", new Person() { Hello = "My3", Name = "Mc" });
            ((My2)buttonSource.My).TypeList.Add(typeof(string));
            ((My2)buttonSource.My).TypeList.Add(typeof(int));
            ((My2)buttonSource.My).TypeList.Add(typeof(List<>));
            ((My2)buttonSource.My).TypeList.Add(null);
            buttonSource.Row = new Person { Value1 = 23 };
            buttonSource.Person = new Person { Value1 = 23 };
            ((My2)buttonSource.My).ListX.Add("4", null);
            ((My2)buttonSource.My).ListX.Add("5", typeof(string));
            ((My2)buttonSource.My).ListX2.Add(null);
            ((My2)buttonSource.My).ListX2.Add(typeof(Dictionary<,>));

            int performanceCount = 1; // 5000;

            string jsonNewtonsoftSource = null;
            for (int i = 0; i < performanceCount; i++)
            {
                // Serialize with Newtonsoft and inheritance
                jsonNewtonsoftSource = Newtonsoft.Json.JsonConvert.SerializeObject(buttonSource, new Newtonsoft.Json.JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });

                // Deserialize with Newtonsoft and inheritance
                Newtonsoft.Json.JsonConvert.DeserializeObject(jsonNewtonsoftSource, new Newtonsoft.Json.JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });
            }

            // Serialize with System.Text.Json (Init)
            var options = new JsonSerializerOptions();
            options.Converters.Add(new Factory());
            options.WriteIndented = true;

            // Serialize with System.Text.Json
            string jsonSource = null;
            ComponentJson buttonDest = null;
            for (int i = 0; i < performanceCount; i++)
            {
                jsonSource = JsonSerializer.Serialize(buttonSource, options);

                // Deserialize with System.Text.Json
                buttonDest = JsonSerializer.Deserialize<ComponentJson>(jsonSource, options);
                var factory = options.Converters.OfType<Factory>().Single();
                foreach (var item in factory.ComponentJsonReferenceList)
                {
                    PropertyInfo propertyInfo = item.PropertyInfo;
                    ComponentJson componentJson = item.ComponentJson;
                    ComponentJson componentJsonReference = factory.ComponentJsonList[item.Id];
                    propertyInfo.SetValue(componentJson, componentJsonReference);
                }
            }

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

        public List<ComponentJsonReference> ComponentJsonReferenceList = new List<ComponentJsonReference>();

        public Dictionary<int, ComponentJson> ComponentJsonList = new Dictionary<int, ComponentJson>();
    }

    public class ComponentJsonReference
    {
        public PropertyInfo PropertyInfo;

        public ComponentJson ComponentJson;

        public int Id;
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

            var interfaceList = propertyInfo.PropertyType.GetInterfaces();

            // List
            if (interfaceList.Contains(typeof(IList)))
            {
                PropertyEnum = PropertyEnum.List;
                PropertyType = propertyInfo.PropertyType.GetGenericArguments()[0]; // List type
                PropertyValueList = (IList)propertyValue;
            }

            // Dictionary
            if (interfaceList.Contains(typeof(IDictionary)))
            {
                PropertyEnum = PropertyEnum.Dictionary;
                PropertyType = propertyInfo.PropertyType.GetGenericArguments()[1]; // Key type
                PropertyDictionary = (IDictionary)propertyValue;
                PropertyValueList = PropertyDictionary?.Values;
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

    public enum ContainerEnum { None = 0, Type = 1, ComponentReference, Row }

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

            // Deserialize ComponentJson or Row object
            var valueList = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader);

            // Deserialize Row
            if (UtilFramework.IsSubclassOf(typeToConvert, typeof(Row)))
            {
                var typeRowName = valueList["$typeRow"].GetString();
                string rowJson = valueList["$row"].GetRawText();
                Type typeRow = Type.GetType(typeRowName);
                var resultRow = JsonSerializer.Deserialize(rowJson, typeRow); // Native deserialization for data row.
                return (T)(object)resultRow;
            }

            // Read type information
            string typeText = valueList["$type"].GetString();
            Type type = Type.GetType(typeText); // TODO Cache on factory
            var result = (ComponentJson)Activator.CreateInstance(type); // TODO No parameterless constructor for ComponentJson

            // Loop through properties
            foreach (var propertyInfo in type.GetProperties())
            {
                if (valueList.ContainsKey(propertyInfo.Name))
                {
                    if (IsComponentJsonReference(propertyInfo))
                    {
                        // Deserialize ComponentJsonReference
                        var componentJsonReferenceValueList = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(valueList[propertyInfo.Name].GetRawText());
                        UtilFramework.Assert(componentJsonReferenceValueList["$type"].GetString() == "$componentJsonReference");
                        int id = componentJsonReferenceValueList["$id"].GetInt32();
                        Factory.ComponentJsonReferenceList.Add(new ComponentJsonReference { PropertyInfo = propertyInfo, ComponentJson = result, Id = id });
                        continue;
                    }

                    // Deserialize ComponentJson
                    var propertyValue = JsonSerializer.Deserialize(valueList[propertyInfo.Name].GetRawText(), propertyInfo.PropertyType, options);
                    propertyInfo.SetValue(result, propertyValue);
                }
            }

            // Add ComponentJson for ComponentJsonReference resolve
            Factory.ComponentJsonList.Add(result.Id, result);

            return (T)(object)result;
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

        private bool IsComponentJsonReference(PropertyInfo propertyInfo)
        {
            bool result = false;
            Property property = new Property(propertyInfo, null);
            bool isComponentJsonList = propertyInfo.DeclaringType == typeof(ComponentJson) && propertyInfo.Name == nameof(ComponentJson.List);
            bool isComponentJson = UtilFramework.IsSubclassOf(property.PropertyType, typeof(ComponentJson));
            if (!isComponentJsonList && isComponentJson) // Is it a component reference?
            {
                if (property.PropertyEnum == PropertyEnum.List || property.PropertyEnum == PropertyEnum.Dictionary)
                {
                    throw new Exception("ComponentJson reference supported only for property! Not for list and dictionary!");
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Serialize ComponentJson or Row objects.
        /// </summary>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // Serialize Type object
            if (typeof(T) == typeof(Type))
            {
                JsonSerializer.Serialize(writer, (value as Type).FullName);
                return;
            }

            // Serialize data row object
            if (UtilFramework.IsSubclassOf(typeof(T), typeof(Row)))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("$typeRow");
                JsonSerializer.Serialize(writer, value.GetType().FullName);
                writer.WritePropertyName("$row");
                JsonSerializer.Serialize(writer, value, value.GetType()); // Native serialization of data row
                writer.WriteEndObject();
                return;
            }

            // ComponentJson or Row object start
            writer.WriteStartObject();
            
            // Type information
            writer.WritePropertyName("$type"); // Note: Type information could be omitted if property type is equal to property value type.
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
                    writer.WritePropertyName(propertyInfo.Name);
                    if (IsComponentJsonReference(propertyInfo))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("$type");
                        JsonSerializer.Serialize(writer, "$componentJsonReference");
                        writer.WritePropertyName("$id");
                        var id = ((ComponentJson)propertyValue).Id;
                        JsonSerializer.Serialize<int>(writer, id); 
                        writer.WriteEndObject();
                    }
                    else
                    {
                        // TODO Distinct serialize for client and for server.
                        // TODO Check ComponentJson reference is in same composition- graph.

                        // Serialize property value
                        JsonSerializer.Serialize(writer, propertyValue, propertyInfo.PropertyType, options);
                    }
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