using System.Drawing;
using System.Windows.Forms;

namespace SystemTrayLauncher.Settings
{
    public static class GlobalSettings
    {
        public static Color COLOR_BACKGROUND = Color.FromArgb(39, 39, 42);
        public static Color COLOR_TEXT = Color.FromArgb(212, 212, 212);
        public static Color COLOR_SELECTED = Color.FromArgb(0, 122, 204);
        public static Color COLOR_SELECTED_DOWN = Color.FromArgb(0, 102, 184);

        public static string CONFIG_FILE = "Config.json";
        public static string CONFIG_SCRIPT = "OpenConfig.bat";

        public static string GetConfigFolder()
        {
            return Application.StartupPath;
        }
        public static string GetConfigPath()
        {
            return GetConfigFolder() + "\\" + GlobalSettings.CONFIG_FILE;
        }

    }
}
