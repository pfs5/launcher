using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using SystemTrayLauncher.Settings;
using SystemTrayLauncher.MenuItems;
using SystemTrayLauncher.Forms;
using SystemTrayLauncher.LaunchItems;
using SystemTrayLauncher.LaunchManagement;

namespace SystemTrayLauncher
{
    public class MainApplicationContext : ApplicationContext
    {
        public static MainApplicationContext CurrentContext { get; set; }

        public MainApplicationContext()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = SystemTrayLauncher.Resources.AppIcon_White;
            _notifyIcon.Visible = true;

            Initialize();

            CurrentContext = this;
            
        }

        public void Initialize()
        {
            InitConfig();

            // Context menu strip
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Renderer = new MenuRenderer();
            menu.ShowImageMargin = false;
            menu.AutoSize = true;
            menu.ShowItemToolTips = true;

            menu.Closing += Menu_Closing;

            menu.BackColor = GlobalSettings.COLOR_BACKGROUND;
            menu.ForeColor = GlobalSettings.COLOR_BACKGROUND;
            _notifyIcon.ContextMenuStrip = menu;

            InitItems();

            HookManager.HookManager.KeyPress += HookManager_KeyPress;
            HookManager.HookManager.KeyUp += HookManager_KeyUp;
            HookManager.HookManager.KeyDown += HookManager_KeyDown;
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            // ptodo: test
            if (e.KeyCode == Keys.F1 && HookManager.HookManager.IsAltDown)
            {
                e.Handled = true;

                SearchForm f = new SearchForm();
                f.ShowDialog();
            }
        }

        public void InitItems()
        {
            ToolStripItemCollection items = _notifyIcon.ContextMenuStrip.Items;

            // Add items from config - files
            if (_config.Items_File != null)
            {
                foreach (ConfigItem_File configItem in _config.Items_File)
                {
                    string path = _config.ItemsRoot + "\\" + configItem.Target;
                    items.Add(new CustomMenuItem_Launcher(Launcher.Instance.AddItem(new LaunchItem_OpenFile(configItem.Name, configItem.Tooltip, path))));
                }
            }

            // Add items from config - command line
            if (_config.Items_CommandLine != null)
            {
                foreach (ConfigItem_CommandLine configItem in _config.Items_CommandLine)
                {
                    items.Add(new CustomMenuItem_Launcher(Launcher.Instance.AddItem(new LaunchItem_CommandLine(configItem.Name, configItem.Tooltip, configItem.Target, configItem.Arguments, configItem.OpenWindow))));
                }
            }

            // Settings
            items.Add("-");
            items.Add(new CustomMenuItem_LauncherCollection("Settings", "", new LaunchItemBase []
            {
                Launcher.Instance.AddItem(new LaunchItem_CommandLine("Open Config", "Open config file in text editor.", "explorer.exe", GlobalSettings.GetConfigPath(), false)).SetSearchable(false),
                Launcher.Instance.AddItem(new LaunchItem_CommandLine("Refresh TODO", "Refresh the config. TODO", "", "", false)).SetSearchable(false)
            }));

            
            items.Add("-");
            items.Add(new CustomMenuItem_Exit("Exit", ""));

        }

        #region Definitions
        private class Config
        {
            public string ItemsRoot { get; set; }
            public ConfigItem_File[] Items_File { get; set; }
            public ConfigItem_CommandLine[] Items_CommandLine { get; set; }
        }

        private class ConfigItem
        {
            public string Name { get; set; }
            public string Tooltip { get; set; }
        }
        private class ConfigItem_File : ConfigItem
        {
            public string Target { get; set; }

        }
        private class ConfigItem_CommandLine : ConfigItem
        {
            public string Target { get; set; }
            public string Arguments { get; set; }
            public bool OpenWindow { get; set; }
        }

        private class CustomMenuItemBase : ToolStripMenuItem
        {
            public CustomMenuItemBase(string name, string tooltip)
            {
                Text = name;
                Name = name;
                ToolTipText = tooltip;

                BackColor = GlobalSettings.COLOR_BACKGROUND;
                ForeColor = GlobalSettings.COLOR_TEXT;
                Font = new Font("Consolas", 12);

                AutoSize = true;
                Dock = DockStyle.Fill;

                MouseDown += CustomMenuItem_MouseDown;
                MouseUp += CustomMenuItem_MouseUp;
            }

            private void CustomMenuItem_MouseDown(object sender, MouseEventArgs e)
            {
                if (GetCurrentParent() == null)
                {
                    return;
                }

                MenuRenderer parentRenderer = (MenuRenderer)GetCurrentParent().Renderer;
                MenuRendererColors colorTable = (MenuRendererColors)parentRenderer.ColorTable;
                colorTable.SelectedColor = GlobalSettings.COLOR_SELECTED_DOWN;
            }
            private void CustomMenuItem_MouseUp(object sender, MouseEventArgs e)
            {
                if (GetCurrentParent() == null)
                {
                    return;
                }

                MenuRenderer parentRenderer = (MenuRenderer)GetCurrentParent().Renderer;
                MenuRendererColors colorTable = (MenuRendererColors)parentRenderer.ColorTable;
                colorTable.SelectedColor = GlobalSettings.COLOR_SELECTED;
            }
        }

        private class CustomMenuItem_Launcher : CustomMenuItemBase
        {
            public CustomMenuItem_Launcher(LaunchItemBase item) : base (item.Name, item.Tooltip)
            {
                _targetItem = item;

                Click += CustomMenuItem_OnClick;
            }

            private LaunchItemBase _targetItem;

            private void CustomMenuItem_OnClick(object sender, EventArgs args)
            {
                _targetItem.Execute();
            }
        }

        private class CustomMenuItem_LauncherCollection : CustomMenuItemBase
        {
            public CustomMenuItem_LauncherCollection(string name, string tooltip, LaunchItemBase[] items) : base(name, tooltip)
            {
                foreach (LaunchItemBase item in items)
                {
                    DropDownItems.Add(new CustomMenuItem_Launcher(item));
                }
            }
        }
        #endregion

        #region Private
        private Config _config;
        private NotifyIcon _notifyIcon;
        #endregion

        private void InitConfig()
        {
            // Config should be located where the .exe is.
            string rootPath = Application.StartupPath;
            string configPath = rootPath + "/" + GlobalSettings.CONFIG_FILE;
            if (!File.Exists(configPath))
            {
                return;
            }

            string configString = File.ReadAllText(configPath);
            _config = JsonSerializer.Deserialize<Config>(configString);
        }

        private void Menu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            ContextMenuStrip senderCasted = (ContextMenuStrip)sender;
            Point p = senderCasted.PointToClient(Control.MousePosition);
            if (senderCasted.DisplayRectangle.Contains(p))
            {
                e.Cancel = true;
            }
        }
    }
}
