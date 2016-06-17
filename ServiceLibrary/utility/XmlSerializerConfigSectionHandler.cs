using System;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace ServiceLibrary.utility
{
    /*
The Last Configuration Section Handler I'll Ever Need  https://sites.google.com/site/craigandera/craigs-stuff/clr-workings/the-last-configuration-section-handler-i-ll-ever-need
     
If you've ever seen the web.config file that ASP.NET uses, and thought, 
"Wow, I'm so glad they configure things that way, in this nice, easy-to-use XML file.
Wish I could do that!" then you should check out the System.Configuration namespace. 
There's an interface called IConfigurationSectionHandler that lets you write your own
parsers for your application configuration file, letting you put whatever you want in there.
     
     Craig Andera
     */

    public class XmlSerializerConfigSectionHandler : IConfigurationSectionHandler
    {
        public object Create(
             object parent,
             object configContext,
             System.Xml.XmlNode section)
        {
            XPathNavigator nav = section.CreateNavigator();
            string typename = (string)nav.Evaluate("string(@type)");
            Type t = Type.GetType(typename);
            XmlSerializer ser = new XmlSerializer(t);
            return ser.Deserialize(new XmlNodeReader(section));
        }
    }

}
