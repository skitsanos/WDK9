using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections;
using System.Xml;

namespace XMLSerialization
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlSerializationUtility
    {
        /// <summary>
        /// 
        /// </summary>
        private static string strERROR = "ERROR";
        /// <summary>
        /// Сериализирует объект в xml
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToXmlString(object obj)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                XmlSerializer xmlSrz = SerializerCache.GetSerializer(obj.GetType());
                xmlSrz.Serialize(ms, obj, new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName("", "") }));
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;
                XmlReader xmlRd = XmlReader.Create(ms);
                xmlRd.MoveToContent();
                string strObjXmlString = xmlRd.ReadOuterXml();
                ms.Flush();
                ms.Close();
                xmlRd.Close();
                return (strObjXmlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ПОПА:" + ex.ToString());
                return (strERROR);
            }
        }
        /// <summary>
        /// Сериализирует объект в xml
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Namespace"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string ObjectToXmlString(object obj, string Name, string Namespace)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                XmlSerializer xmlSrz = SerializerCache.GetSerializer(obj.GetType());
                xmlSrz.Serialize(ms, obj, new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(Name, Namespace) }));
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;
                XmlReader xmlRd = XmlReader.Create(ms);
                xmlRd.MoveToContent();
                string strObjXmlString = xmlRd.ReadOuterXml();
                ms.Flush();
                ms.Close();
                xmlRd.Close();
                return (strObjXmlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ПОПА:" + ex.ToString());
                return (strERROR);
            }
        }
        /// <summary>
        /// Сериализирует объект в xml
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Namespace"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string ObjectToXmlString(object obj, string DefaultNamespace, XmlSerializerNamespaces namespaces)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                XmlSerializer xmlSrz = SerializerCache.GetSerializer(obj.GetType(), DefaultNamespace);                
                xmlSrz.Serialize(ms, obj, namespaces);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;                               

                XmlReader xmlRd = XmlReader.Create(ms);
                xmlRd.MoveToContent();
                string strObjXmlString = xmlRd.ReadOuterXml();
                ms.Flush();
                ms.Close();
                xmlRd.Close();
                return (strObjXmlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ПОПА:" + ex.ToString());
                return (strERROR);
            }
        }
        /// <summary>
        /// Сериализирует объект в xml
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Namespace"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string ObjectToXmlString(object obj, XmlSerializerNamespaces namespaces)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                XmlSerializer xmlSrz = SerializerCache.GetSerializer(obj.GetType());
                xmlSrz.Serialize(ms, obj, namespaces);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;

                XmlReader xmlRd = XmlReader.Create(ms);
                xmlRd.MoveToContent();
                string strObjXmlString = xmlRd.ReadOuterXml();
                ms.Flush();
                ms.Close();
                xmlRd.Close();
                return (strObjXmlString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ПОПА:" + ex.ToString());
                return (strERROR);
            }
        }
        /// <summary>
        /// Десериализирует xml в объект
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="XmlStirng"></param>
        /// <returns></returns>
        public static T XmlStringToObject<T>(string XmlStirng)
        {
            try
            {
                if (XmlStirng == null)
                    return (default(T));
                if (XmlStirng == string.Empty)
                    return ((T)Activator.CreateInstance(typeof(T)));

                StringReader reader = new StringReader(XmlStirng);
                XmlSerializer xmlSrz = new XmlSerializer(typeof(T));
                return ((T)xmlSrz.Deserialize(reader));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ПОПА:" + ex.ToString());
                return (default(T));
            }
        }
    }


    /// <summary>
    /// Кэш для используемых сериалайзеров 
    /// </summary>
    internal class SerializerCache
    {
        private static Hashtable hash = new Hashtable();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XmlSerializer GetSerializer(Type type)
        {
            XmlSerializer res = null;
            lock (hash)
            {
                res = hash[type.FullName] as XmlSerializer;
                if (res == null)
                {
                    res = new XmlSerializer(type);
                    hash[type.FullName] = res;
                }
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XmlSerializer GetSerializer(Type type, string DefaultNamespace)
        {
            XmlSerializer res = null;
            lock (hash)
            {
                res = hash[type.FullName] as XmlSerializer;
                if (res == null)
                {
                    res = new XmlSerializer(type, DefaultNamespace);
                    hash[type.FullName] = res;
                }
            }
            return res;
        }
    }  
}
