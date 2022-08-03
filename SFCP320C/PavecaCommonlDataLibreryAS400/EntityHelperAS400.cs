using System;
using System.Data;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Xml;

namespace PavecaCommonlDataLibreryAS400
{
    [Serializable]
    public class EntityHelperAS400
    {
        public static T Mapper<T>(IDataReader reader, T entity)
        {
            int i;
            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    try
                    {
                        i = reader.GetOrdinal(propertyInfo.Name);
                        if (DBNull.Value != reader[i])
                        {
                            propertyInfo.SetValue(entity, reader[i], null);
                        }
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                    }
                }
            }
            return entity;
        }

        public static string Serialize(object objectInstance, Type objectType)
        {
            XmlRootAttribute rootAttribute = new XmlRootAttribute();
            rootAttribute.Namespace = objectType.ToString();
            rootAttribute.ElementName = objectType.ToString().Substring(objectType.ToString().LastIndexOf(".") + 1);

            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add("PAVECA", objectType.ToString());

            XmlSerializer MySerializer = new XmlSerializer(objectType, rootAttribute);

            MemoryStream me = new MemoryStream();
            XmlTextWriter tw = new XmlTextWriter(me, Encoding.UTF8);

            MySerializer.Serialize(tw, objectInstance, xmlns);

            Encoding e8 = new UTF8Encoding();
            string xml = e8.GetString(me.ToArray(), 1, me.ToArray().Length - 1);

            xml = xml.Substring(xml.IndexOf("<")).Trim();
            return xml;
        }

        public static object Deserialize(string serializedInstance, Type typeInstance)
        {
            serializedInstance = serializedInstance.Substring(serializedInstance.IndexOf("<")).Trim();
            XmlRootAttribute rootAttribute = new XmlRootAttribute();
            rootAttribute.Namespace = typeInstance.ToString();
            rootAttribute.ElementName = typeInstance.ToString().Substring(typeInstance.ToString().LastIndexOf(".") + 1);

            XmlSerializer serializer = new XmlSerializer(typeInstance, rootAttribute);
            TextReader reader = new StringReader(serializedInstance);
            object instance = serializer.Deserialize(reader);

            return instance;
        }
    }
}
