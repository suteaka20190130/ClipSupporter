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

            var config = (PanelConfig)ConfigurationManager.GetSection("PanelSections/PanelConfig") ?? new PanelConfig();
            return config;
        }

        public static ConfigurationSectionCollection GetConfigs()
        {
            return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Sections;

        }

        /// <summary>
        /// 見出し名
        /// </summary>
        [ConfigurationProperty("TitleName", DefaultValue = "TitleName", IsRequired = true)]
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

        /// <summary>
        /// パネルのベースフォルダ名
        /// </summary>
        [ConfigurationProperty("FolderName", DefaultValue = "FolderName", IsRequired = true)]
        public string FolderName 
        {
            get
            {
                return (string)this["FolderName"];
            }
            set
            {
                this["FolderName"] = value;
            }
        }

        /// <summary>
        /// パネルに割り当てる機能のアセンブリ名
        /// </summary>
        [ConfigurationProperty("AssemblyName", DefaultValue = "AssemblyName", IsRequired = true)]
        public string AssemblyName 
        {
            get
            {
                return (string)this["AssemblyName"];
            }
            set
            {
                this["AssemblyName"] = value;
            }
        }

        /// <summary>
        /// パネルに割り当てる機能のクラス名
        /// </summary>
        [ConfigurationProperty("ClassName", DefaultValue = "ClassName", IsRequired = true)]
        public string ClassName
        {
            get
            {
                return (string)this["ClassName"];
            }
            set
            {
                this["ClassName"] = value;
            }
        }

        /// <summary>
        /// パネルの表示エリアXサイズ
        /// </summary>
        [ConfigurationProperty("AreaXSize", DefaultValue = 4, IsRequired = true)]
        public int AreaXSize
        {
            get
            {
                return (int)this["AreaXSize"];
            }
            set
            {
                this["AreaXSize"] = value;
            }
        }

        /// <summary>
        /// パネルの表示エリアYサイズ
        /// </summary>
        [ConfigurationProperty("AreaYSize", DefaultValue = 1, IsRequired = true)]
        public int AreaYSize
        {
            get
            {
                return (int)this["AreaYSize"];
            }
            set
            {
                this["AreaYSize"] = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PanelConfig()
        {
         }

        [ConfigurationProperty("Controls")]
        [ConfigurationCollection(typeof(ControlConfigElementCollection), AddItemName ="Control")]
        public ControlConfigElementCollection Controls
        {
            get { return base["Controls"] as ControlConfigElementCollection; }
        }
    }
}
