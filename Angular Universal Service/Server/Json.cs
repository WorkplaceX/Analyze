namespace Server
{
    using Application;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Json
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

        public static string Serialize(object obj)
        {
            TypeInfoAdd(obj, null);
            SerializePrepare(obj);
            string result = JsonConvert.SerializeObject(obj);
            // TODO Debug
            {
                string debugSource = JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                object debugObj = Deserialize(result);
                string debugDest = JsonConvert.SerializeObject(debugObj, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                Util.Assert(debugSource == debugDest);
                SerializePrepare(debugObj);
                result = JsonConvert.SerializeObject(debugObj);
            }
            //
            return result;
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

        private static object DeserializeToken(JToken jToken, Type fieldType, string namespaceName, Assembly assembly)
        {
            object result = null;
            JObject jObject = jToken as JObject;
            if (jObject != null)
            {
                string objectTypeString = (string)((JValue)jObject.Property("Type")?.Value)?.Value;
                Type objectType;
                if (objectTypeString != null)
                {
                    objectType = Type.GetType(namespaceName + "." + objectTypeString + ", " + assembly.FullName);
                }
                else
                {
                    objectType = fieldType;
                }
                result = Activator.CreateInstance(objectType); // TODO FormatterServices.GetUninitializedObject; RuntimeHelpers.GetUninitializedObject
                foreach (var item in jObject.Properties())
                {
                    FieldInfo fieldInfo = objectType.GetTypeInfo().GetField(item.Name);
                    TypeGroup typeGroup;
                    Type valueType;
                    Json.TypeInfo(fieldInfo.FieldType, out typeGroup, out valueType);
                    if (typeGroup == TypeGroup.Dictionary)
                    {
                        var list = (IDictionary)Activator.CreateInstance(fieldInfo.FieldType);
                        foreach (var keyValue in ((JObject)item.Value))
                        {
                            object value = DeserializeToken(keyValue.Value, valueType, namespaceName, assembly);
                            var valueSet = DeserializeObjectConvert(value, valueType);
                            list.Add(keyValue.Key, valueSet);
                        }
                        fieldInfo.SetValue(result, list);
                    }
                    else
                    {
                        object value = DeserializeToken(item.Value, valueType, namespaceName, assembly);
                        if (item.Value is JArray)
                        {
                            var list = (IList)Activator.CreateInstance(fieldInfo.FieldType);
                            foreach (var listItem in (IList)value)
                            {
                                var listItemSet = DeserializeObjectConvert(listItem, valueType);
                                list.Add(listItemSet);
                            }
                            fieldInfo.SetValue(result, list);
                        }
                        else
                        {
                            object valueSet = DeserializeObjectConvert(value, fieldInfo.FieldType);
                            fieldInfo.SetValue(result, valueSet);
                        }
                    }
                }
            }
            else
            {
                JValue jValue = jToken as JValue;
                if (jValue != null)
                {
                    object value = jValue.Value;
                    result = value;
                }
                else
                {
                    JArray jArray = jToken as JArray;
                    if (jArray != null)
                    {
                        List<object> resultList = new List<object>();
                        foreach (var listItem in jArray)
                        {
                            object value = DeserializeToken(listItem, fieldType, namespaceName, assembly);
                            resultList.Add(value);
                        }
                        result = resultList.ToArray();
                    }
                    else
                    {
                        throw new Exception("Type unknown!");
                    }
                }
            }
            return result;
        }

        public static object Deserialize(string value)
        {
            JObject jObject = (JObject)JsonConvert.DeserializeObject(value);
            return DeserializeToken(jObject, null, "Application", typeof(Data).GetTypeInfo().Assembly);
        }
    }
}
