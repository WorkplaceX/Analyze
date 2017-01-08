namespace Server.Json
{
    using Application;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Util
    {
        private enum TypeGroup { None, Value, Object, List, Dictionary }

        private static void TypeInfo(Type fieldType, out TypeGroup typeGroup, out Type valueType)
        {
            if (fieldType.GetTypeInfo().IsValueType)
            {
                typeGroup = TypeGroup.Value;
                valueType = fieldType;
                return;
            }
            if (fieldType.GetTypeInfo().IsGenericType && fieldType.GetTypeInfo().GetGenericTypeDefinition() == typeof(List<>))
            {
                typeGroup = TypeGroup.List;
                valueType = fieldType.GetTypeInfo().GetGenericArguments().First();
                return;
            }
            if (fieldType.GetTypeInfo().IsGenericType && fieldType.GetTypeInfo().GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                typeGroup = TypeGroup.Dictionary;
                valueType = fieldType.GetTypeInfo().GetGenericArguments()[1];
                return;
            }
            if (fieldType == typeof(string))
            {
                typeGroup = TypeGroup.Value;
                valueType = fieldType;
                return;
            }
            if (fieldType.GetTypeInfo().GetConstructors().Count() > 0)
            {
                typeGroup = TypeGroup.Object;
                valueType = fieldType;
                return;
            }
            valueType = null;
            typeGroup = TypeGroup.None;
        }

        /// <summary>
        /// Add type information to object if derived from valueType.
        /// </summary>
        private static void TypeInfoAdd(object obj, Type valueType)
        {
            if (obj != null)
            {
                if (valueType != null && Nullable.GetUnderlyingType(valueType) != null)
                {
                    valueType = Nullable.GetUnderlyingType(valueType);
                }
                FieldInfo fieldInfo = obj.GetType().GetFields().Where(item => item.Name == "Type").FirstOrDefault();
                if (obj.GetType() != valueType)
                {
                    if (fieldInfo == null)
                    {
                        throw new Exception(string.Format("Derived class does not contain field 'Type'! (Base={0}; Derived={1})", valueType?.Name, obj.GetType().Name));
                    }
                    fieldInfo.SetValue(obj, obj.GetType().Name);
                }
                else
                {
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(obj, null);
                    }
                }
            }
        }

        private static void SerializePrepare(object obj)
        {
            if (obj != null)
            {
                if (obj is IList)
                {
                    foreach (var item in (IList)obj)
                    {
                        SerializePrepare(item);
                    }
                }
                else
                {
                    if (obj is IDictionary)
                    {
                        foreach (DictionaryEntry item in (IDictionary)obj)
                        {
                            SerializePrepare(item.Value);
                        }
                    }
                    else
                    {
                        foreach (var field in obj.GetType().GetFields())
                        {
                            TypeGroup typeGroup;
                            Type valueType;
                            TypeInfo(field.FieldType, out typeGroup, out valueType);
                            object value = field.GetValue(obj);
                            switch (typeGroup)
                            {
                                case TypeGroup.Value:
                                    TypeInfoAdd(value, valueType);
                                    break;
                                case TypeGroup.Object:
                                    TypeInfoAdd(value, valueType);
                                    SerializePrepare(value);
                                    break;
                                case TypeGroup.List:
                                    foreach (var item in (IList)value)
                                    {
                                        TypeInfoAdd(item, valueType);
                                        SerializePrepare(item);
                                    }
                                    if (((IList)value).Count == 0)
                                    {
                                        // field.SetValue(obj, null); // TODO clone object first.
                                    }
                                    break;
                                case TypeGroup.Dictionary:
                                    foreach (DictionaryEntry item in (IDictionary)value)
                                    {
                                        TypeInfoAdd(item.Value, valueType);
                                        SerializePrepare(item.Value);
                                    }
                                    if (((IDictionary)value).Count == 0)
                                    {
                                        // field.SetValue(obj, null); // TODO clone object first.
                                    }
                                    break;
                                default:
                                    throw new Exception("Type unknown!");
                            }
                        }
                    }
                }
            }
        }

        private static string Serialize(object obj, Type rootType)
        {
            SerializePrepare(obj);
            string result = JsonConvert.SerializeObject(obj);
            // TODO Debug
            {
                string debugSource = JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                object debugObj = Deserialize(result, rootType);
                string debugDest = JsonConvert.SerializeObject(debugObj, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                Util.Assert(debugSource == debugDest);
                SerializePrepare(debugObj);
                result = JsonConvert.SerializeObject(debugObj);
            }
            //
            return result;
        }

        public static string Serialize(object obj)
        {
            return Serialize(obj, obj.GetType());
        }

        private static object DeserializeObjectConvert(object value, Type type)
        {
            if (value == null)
            {
                return value;
            }
            if (type == typeof(object))
            {
                return value;
            }
            if (type == typeof(Guid))
            {
                return Guid.Parse((string)value);
            }
            if (value.GetType().GetTypeInfo().IsSubclassOf(type))
            {
                return value;
            }
            if (Nullable.GetUnderlyingType(type) != null)
            {
                type = Nullable.GetUnderlyingType(type);
            }
            return Convert.ChangeType(value, type);
        }

        private static Type DeserializeTokenObjectType(JObject jObject, Type fieldType, Type rootType)
        {
            Type result = null;
            if (jObject != null)
            {
                result = fieldType;
                if (jObject.Property("Type") != null)
                {
                    JValue jValue = jObject.Property("Type").Value as JValue;
                    string objectTypeString = jValue.Value as string;
                    if (objectTypeString != null)
                    {
                        result = Type.GetType(rootType.Namespace + "." + objectTypeString + ", " + rootType.GetTypeInfo().Assembly.FullName);
                    }
                }
            }
            return result;
        }

        private static object DeserializeToken(JToken jToken, Type fieldType, Type rootType)
        {
            object result = null;
            TypeGroup typeGroup;
            Type valueType;
            Util.TypeInfo(fieldType, out typeGroup, out valueType);
            switch (typeGroup)
            {
                case TypeGroup.Value:
                    {
                        JValue jValue = jToken as JValue;
                        if (jValue != null)
                        {
                            object value = jValue.Value;
                            result = DeserializeObjectConvert(value, valueType);
                        }
                    }
                    break;
                case TypeGroup.Object:
                    {
                        JObject jObject = jToken as JObject;
                        Type objectType = DeserializeTokenObjectType(jObject, fieldType, rootType);
                        if (objectType != null)
                        {
                            result = Activator.CreateInstance(objectType);
                            foreach (var fieldInfo in result.GetType().GetTypeInfo().GetFields())
                            {
                                if (jObject != null)
                                {
                                    JToken jTokenChild = jObject.Property(fieldInfo.Name).Value;
                                    Type fieldTypeChild = fieldInfo.FieldType;
                                    object valueChild = DeserializeToken(jTokenChild, fieldTypeChild, rootType);
                                    fieldInfo.SetValue(result, valueChild);
                                }
                            }
                        }
                    }
                    break;
                case TypeGroup.List:
                    {
                        var list = (IList)Activator.CreateInstance(fieldType);
                        JArray jArray = jToken as JArray;
                        if (jArray != null)
                        {
                            foreach (var jTokenChild in jArray)
                            {
                                Type fieldTypeChild = valueType;
                                object valueChild = DeserializeToken(jTokenChild, fieldTypeChild, rootType);
                                list.Add(valueChild);
                            }
                        }
                        result = list;
                    }
                    break;
                case TypeGroup.Dictionary:
                    {
                        var list = (IDictionary)Activator.CreateInstance(fieldType);
                        JObject jObject = jToken as JObject;
                        if (jObject != null)
                        {
                            foreach (var jKeyValue in jObject)
                            {
                                Type fieldTypeChild = valueType;
                                JToken jTokenChild = jKeyValue.Value;
                                object valueChild = DeserializeToken(jTokenChild, fieldTypeChild, rootType);
                                list.Add(jKeyValue.Key, valueChild);
                            }
                        }
                        result = list;
                    }
                    break;
                default:
                    throw new Exception("Type unknown!");
            }
            return result;
        }

        private static object Deserialize(string json, Type rootType)
        {
            JObject jObject = (JObject)JsonConvert.DeserializeObject(json);
            return DeserializeToken(jObject, rootType, rootType);
        }

        public static T Deserialize<T>(string json)
        {
            return (T)Deserialize(json, typeof(T));
        }

        public static void Assert(bool isAssert, string exceptionText)
        {
            if (!isAssert)
            {
                throw new Exception(exceptionText);
            }
        }

        public static void Assert(bool isAssert)
        {
            Assert(isAssert, "Assert!");
        }
    }
}
