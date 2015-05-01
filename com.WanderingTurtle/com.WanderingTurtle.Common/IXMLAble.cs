using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    public interface IXMLAble
    {
        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// Converts to an object to a string xml representation of itself.
        /// 
        /// Adapted from code orignally by Jim Glasgow
        /// </summary>
        /// <returns>string xml</returns>
        string ToXML();

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// Writes a xml representation of an object to a file.
        /// 
        /// Adapted from code originally by Jim Glasgow
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool XMLFile(string filename);
    }
}
