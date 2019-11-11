using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipSupporter.Config
{
    public class PanelSection : ConfigurationSection
    {
        public static ConfigurationSection GetConfigs()
        {
            var a = ConfigurationManager.GetSection("PanelGroup/KCPanel");

            return (ConfigurationSection)a;
        }


        [ConfigurationProperty("Panels")]
        [ConfigurationCollection(typeof(PanelConfigElementCollection), AddItemName = "Panel")]
        public PanelConfigElementCollection Panels
        {
            get { return base["Panels"] as PanelConfigElementCollection; }
        }
    }
}
