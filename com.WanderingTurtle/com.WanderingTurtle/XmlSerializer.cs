using System;
using System.IO;
using System.Xml.Serialization;

namespace com.WanderingTurtle.BusinessLogic
{
    public static class XmlManager
    {
        /// <summary>
        /// Serializes object into an xml string.
        /// Adapted from code originally by Jim Glasgow
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <remarks>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// Updated Miguel Santana 2015/05/07 Moved to static XmlManager class
        /// </remarks>
        /// <returns>string xml representation of object</returns>
        /// <exception cref="IOException">An I/O error occurs. </exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        /// <exception cref="InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException" /> property. </exception>
        /// <exception cref="ArgumentException"><paramref name="stream" /> does not support reading. </exception>
        /// <exception cref="ArgumentNullException"><paramref name="stream" /> is null. </exception>
        /// <exception cref="ArgumentOutOfRangeException">The position is set to a negative value or a value greater than <see cref="F:System.Int32.MaxValue" />. </exception>
        /// <exception cref="ObjectDisposedException">The stream is closed. </exception>
        public static string ToXml<T>(this object objectToSerialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            MemoryStream memStream = new MemoryStream();

            serializer.Serialize(memStream, objectToSerialize);

            //we need a streamReader to read a memory stream, as it is just a buffer.

            StreamReader reader = new StreamReader(memStream);

            // read stream to a string.
            memStream.Position = 0;
            string text = reader.ReadToEnd();

            reader.Close();
            memStream.Close();

            return text;
        }

        /// <summary>
        /// Saves a serialized xml representation of this object.
        /// Adapted from code originally by Jim Glasgow
        /// </summary>
        /// <remarks>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// Updated Miguel Santana 2015/05/07 Moved to static XmlManager class
        /// </remarks>
        /// <param name="objectToSerialize"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName">string name of the file and path to save the xml</param>
        /// <returns>bool successful</returns>
        /// <exception cref="ApplicationException">Error serializing file</exception>
        public static bool XmlFile<T>(this object objectToSerialize, string fileName)
        {
            try
            {
                //first, new up an xml serializer of the current type.
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                //to write to a file, build a streamWriter
                StreamWriter writer = new StreamWriter(fileName);

                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }

                serializer.Serialize(writer, objectToSerialize);

                writer.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error serializing file", ex);
            }
        }
    }
}