using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipSupporter.Config
{
    public sealed class PanelConfig : ConfigurationSection
    {
        public static PanelConfig GetConfig()
        {
            return (PanelConfig)ConfigurationManager.GetSection("PanelSections/PanelConfigs") ?? new PanelConfig();
        }

        /// <summary>
        /// 見出し名
        /// </summary>
        [ConfigurationProperty("TitleName", DefaultValue = "Test.html", IsRequired = true)]
        public string TitleName 
        {
            get
            {
                return (string)this["TitleName"];
            }
            set
            {
                this["TitleName"] = value;
            }
        }

        [ConfigurationProperty("PanelBasePath", DefaultValue = "Test.html", IsRequired = true)]
        public string PanelBasePath 
        {
            get
            {
                return (string)this["PanelBasePath"];
            }
            set
            {
                this["PanelBasePath"] = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PanelConfig()
        {
        }
    }
}
