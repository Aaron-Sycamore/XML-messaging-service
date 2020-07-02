using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Part_1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Send_Click(object sender, EventArgs e)
        {
            string msg = Message.Text;
            string senderID = SenderID.Text;
            string resipantID = ReceverID.Text;
            string fLocation = Path.Combine(Request.PhysicalApplicationPath, "C:\\Temp\\messages.xml");
            if (File.Exists(fLocation))
            {
                XDocument xDoc = XDocument.Load(fLocation);
                XElement messages = xDoc.Element("Messages");
                messages.Add(new XElement("Message", new XElement("RecipantID", resipantID), new XElement("SenderID", senderID), new XElement("Msg", msg)));
                xDoc.Save(fLocation);

            }
            else
            {
                FileStream fstate = null;
                try
                {
                    fstate = new FileStream(fLocation, FileMode.CreateNew);
                    XmlTextWriter writer = new XmlTextWriter(fstate, System.Text.Encoding.Unicode);
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Messages");
                    writer.WriteStartElement("Message");
                    writer.WriteElementString("RecipantID", resipantID);
                    writer.WriteElementString("SenderID", senderID);
                    writer.WriteElementString("TimeStamp", DateTime.Now.ToString("T"));
                    writer.WriteElementString("Msg", msg);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                    fstate.Close();
                }
                finally
                {
                    if (fstate != null) fstate.Close();
                }
            }
        }
    }
}