using System;
using System.IO;
using System.Windows.Forms;

namespace SystemTrayLauncher.LaunchItems
{
    public class LaunchItemBase
    {
        public LaunchItemBase(string name, string tooltip)
        {
            _name = name;
            _tooltip = tooltip;
            IsSearchable = true;
        }

        public virtual void Execute() { }

        public LaunchItemBase SetSearchable(bool value) { IsSearchable = value; return this; }

        public string Name { get { return _name; } }
        public string Tooltip { get { return _tooltip; } }

        public bool IsSearchable { get; set; }

        #region Private
        private string _name;
        private string _tooltip;
        #endregion
    }

    public class LaunchItem_OpenFile : LaunchItemBase
    {
        public LaunchItem_OpenFile(string name, string tooltip, string filePath) : base(name, tooltip)
        {
            _filePath = filePath;
        }

        public override void Execute()
        {
            if (!File.Exists(_filePath))
            {
                throw new Exception($"File {_filePath} not found.");
            }

            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                pProcess.StartInfo.FileName = _filePath;
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = false;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                pProcess.StartInfo.CreateNoWindow = false;
                pProcess.Start();
            }
        }

        private string _filePath;
    }

    public class LaunchItem_CommandLine : LaunchItemBase
    {
        public LaunchItem_CommandLine(string name, string tooltip, string commandLine, string arguments, bool openWindow) : base(name, tooltip)
        {
            _commandLine = commandLine;
            _arguments = arguments;
            _openWindow = openWindow;
        }

        public override void Execute()
        {
            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                if (_openWindow)
                {
                    pProcess.StartInfo.FileName = _commandLine;
                    pProcess.StartInfo.Arguments = _arguments;
                    pProcess.StartInfo.UseShellExecute = true;
                    pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    pProcess.StartInfo.CreateNoWindow = false;
                    pProcess.Start();
                }
                else
                {
                    pProcess.StartInfo.FileName = _commandLine;
                    pProcess.StartInfo.Arguments = _arguments;
                    pProcess.StartInfo.UseShellExecute = false;
                    pProcess.StartInfo.RedirectStandardOutput = true;
                    pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    pProcess.StartInfo.CreateNoWindow = true;
                    pProcess.Start();
                }

            }
        }

        private string _commandLine;
        private string _arguments;
        private bool _openWindow;
    }

    public class LaunchItem_Exit : LaunchItemBase
    {
        public LaunchItem_Exit(string name, string tooltip) : base(name, tooltip)
        {
        }

        public override void Execute()
        {
            Application.Exit();
        }
    }
}
