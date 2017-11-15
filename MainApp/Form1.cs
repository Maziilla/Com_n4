using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interface;

namespace MainApp
{

    
    public partial class Form1 : Form, IMainApp
    {
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        public Bitmap Image
        {
            get
            {
                return (Bitmap)pictureBox.Image;
            }
            set
            {
                pictureBox.Image = value;
            }
        }
        //public void FindPlugins()
        //{
        //    AppDomain pliginsDomain = AppDomain.CreateDomain("Plagins domain");
        //    // папка с плагинами
        //    string folder = AppDomain.CurrentDomain.BaseDirectory;

        //    // dll-файлы в этой папке
        //    string[] files = Directory.GetFiles(folder, "*.dll");

        //    foreach (string file in files)
        //        try
        //        {
        //            //  pliginsDomain.Load(AssemblyName.GetAssemblyName(file)).GetTypes;// .LoadFile(file);

        //            // pliginsDomain.c
        //            //pliginsDomain.DoCallBack(() => Assembly.LoadFile(file));
        //            pliginsDomain.ExecuteAssembly("PlugIn.dll");

        //            foreach (Type type in pliginsDomain.GetAssemblies().Last().GetTypes())
        //            {
        //                Type iface = type.GetInterface("Interface.IPlugin");

        //                if (iface != null)
        //                {
        //                    // MainApp.vshost.exe
                            
        //                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
        //                    plugins.Add(plugin.Name, plugin);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
        //        }
        //    AppDomain.Unload(pliginsDomain);
        //}
        void CreatePluginsMenu()
        {
            foreach (string name in plugins.Keys)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(name);
                item.Click += OnPluginClick;
                фильтрыToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            Interface.IPlugin plugin = (Interface.IPlugin)plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform(this);
        }

        public Form1()
        {
            AppDomain pliginsDomain = AppDomain.CreateDomain("Plagins domain");

            InitializeComponent();
            var resident = (Find)pliginsDomain.CreateInstanceAndUnwrap(
                    Assembly.GetExecutingAssembly().FullName,
                    "MainApp.Find");
            plugins = resident.FindPlugins();
            AppDomain.Unload(pliginsDomain);
            CreatePluginsMenu();

            //FindPlugins();

        }
    }
    public class Find : MarshalByRefObject
    {
        public Dictionary<string, IPlugin> FindPlugins()
        {
            Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
            // папка с плагинами
            string folder = System.AppDomain.CurrentDomain.BaseDirectory;

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("Interface.IPlugin");

                        if (iface != null)
                        {
                            Interface.IPlugin plugin = (Interface.IPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin.Name, plugin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
            return plugins;
        }
    }
}
