using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipSupporter.Config
{
    public sealed class ControlConfigElement : ConfigurationElement
    {
        public string ControlName { get; set; }

        /// <summary>
        /// コントロールのクラス名
        /// Blankの場合はSkipする
        /// </summary>
        [ConfigurationProperty("ControlClassName", DefaultValue = "ControlClassName", IsRequired = true)]
        public string ControlClassName
        {
            get
            {
                return (string)this["ControlClassName"];
            }
            set
            {
                this["ControlClassName"] = value;
            }
        }

        /// <summary>
        /// コントロールの処理を割り当てるイベント名
        /// </summary>
        [ConfigurationProperty("EventName", DefaultValue = "EventName", IsRequired = true)]
        public string EventName
        {
            get
            {
                return (string)this["EventName"];
            }
            set
            {
                this["EventName"] = value;
            }
        }

        /// <summary>
        /// コントロールの処理名
        /// </summary>
        [ConfigurationProperty("EventMethodName", DefaultValue = "EventMethodName", IsRequired = true)]
        public string EventMethodName
        {
            get
            {
                return (string)this["EventMethodName"];
            }
            set
            {
                this["EventMethodName"] = value;
            }
        }

        /// <summary>
        /// コントロールのXサイズ
        /// </summary>
        [ConfigurationProperty("XSize", DefaultValue = 1, IsRequired = true)]
        public int XSize
        {
            get
            {
                return (int)this["XSize"];
            }
            set
            {
                this["XSize"] = value;
            }
        }

        /// <summary>
        /// コントロールのYサイズ
        /// </summary>
        [ConfigurationProperty("YSize", DefaultValue = 1, IsRequired = true)]
        public int YSize
        {
            get
            {
                return (int)this["YSize"];
            }
            set
            {
                this["YSize"] = value;
            }
        }

        /// <summary>
        /// コントロールの表示名
        /// </summary>
        [ConfigurationProperty("Text", DefaultValue = "Text", IsRequired = true)]
        public string Text
        {
            get
            {
                return (string)this["Text"];
            }
            set
            {
                this["Text"] = value;
            }
        }
    }
}
