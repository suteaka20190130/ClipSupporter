using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using FunctionLibraryBP;

namespace ClipSupporter.Panel
{
    public partial class ButtonPanel : ClipSupporter.BladePanel
    {
        public int AreaLeft { get; set; }
        public int AreaTop { get; set; }
        public int AreaCellWidth { get; set; }
        public int AreaCellHeight { get; set; }

        public ButtonPanel(Config.PanelConfigElement cfg) : base(cfg)
        {
            InitializeComponent();

            AreaLeft = 3;
            AreaTop = 15;
            AreaCellWidth = ButtonArea.Width / cfg.AreaXSize;
            AreaCellHeight = ButtonArea.Height / cfg.AreaYSize;

            Assembly assembly = Assembly.LoadFrom(cfg.AssemblyName+".dll");
            Type myType = assembly.GetType(cfg.ClassName);
            MainInstance = Activator.CreateInstance(myType);

            this.Controls.Remove(ButtonArea);

            foreach (Config.ControlConfigElement cfgCtrl in cfg.Controls)
            {
                this.Controls.Add((Control)CreateContorl(cfgCtrl));
            }
        }

        delegate void DelSample(object sender, EventArgs eventArgs);
        private object CreateContorl(Config.ControlConfigElement cfg)
        {
            var asm = typeof(Form).Assembly;
            Type myType = asm.GetType(cfg.ControlClassName);
            object control = Activator.CreateInstance(myType);
            Type tp = control.GetType();
            PropertyInfo pInfo;

            // Text
            pInfo = tp.GetProperty("Text");
            pInfo.SetValue(control, cfg.Text);
            // Left
            pInfo = tp.GetProperty("Left");
            pInfo.SetValue(control, AreaLeft);
            // Top
            pInfo = tp.GetProperty("Top");
            pInfo.SetValue(control, AreaTop);
            // Width
            pInfo = tp.GetProperty("Width");
            pInfo.SetValue(control, AreaCellWidth * cfg.XSize);
            AreaLeft += AreaCellWidth * cfg.XSize;
            // Height
            pInfo = tp.GetProperty("Height");
            pInfo.SetValue(control, AreaCellHeight * cfg.YSize);
            //AreaTop += AreaCellHeight * cfg.YSize;

            // EventHandler
            EventInfo eInfo = tp.GetEvent(cfg.EventName);
            Type tpClick = eInfo.EventHandlerType;

            MethodInfo mInfo = MainInstance.GetType().GetMethod(cfg.EventMethodName);

            Delegate d = Delegate.CreateDelegate(tpClick, MainInstance, mInfo);
            MethodInfo minfo2 = eInfo.GetAddMethod();
            minfo2.Invoke(control, new Object[] { d });

            return control;
        }

        private void ButtonPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
