using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Part_2
{
    public partial class SiteMaster : MasterPage
    {
        bool perg = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Receive_Click(object sender, EventArgs e)
        {
            string fLocation = Path.Combine(Request.PhysicalApplicationPath, "C:\\Temp\\messages.xml");

            if (File.Exists(fLocation))
            {
                //FileStream fs = new FileStream(fLocation, FileMode.Open);
                XmlDocument xd = new XmlDocument();
                xd.Load(fLocation);
                XmlNode node = xd;
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode child in children.Item(1))
                {
                    if (child.FirstChild.InnerText == ReceiverID.Text)
                    {
                        ListBox1.Items.Add("SenderID: " + child.FirstChild.NextSibling.InnerText);
                        ListBox1.Items.Add("Message: " + child.LastChild.InnerText);
                        ListBox1.Items.Add("\r\n");
                        if (perg == true)
                        {
                            child.FirstChild.InnerText = "";
                        }
                    }
                    xd.Save(fLocation);
                }
                if (perg == true)
                {
                    perg = false;
                }
            }
        }

        protected void Purge_CheckedChanged(object sender, EventArgs e)
        {
            perg = true;
        }
    }
}