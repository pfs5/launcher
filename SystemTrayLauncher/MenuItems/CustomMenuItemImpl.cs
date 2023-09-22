using System;
using System.IO;
using System.Windows.Forms;

namespace SystemTrayLauncher.MenuItems
{
    public class CustomMenuItem_OpenFile : CustomMenuItemBase
    {
        public CustomMenuItem_OpenFile(string name, string target, string tooltip) : base(name, tooltip)
        {
            _targetPath = target;

            Click += Execute;
        }


        private string _targetPath = "";

        private void Execute(object sender, EventArgs args)
        {
            if (Disabled)
            {
                return;
            }

            if (!File.Exists(_targetPath))
            {
                MessageBox.Show("File not found: " + _targetPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
                {
                    pProcess.StartInfo.FileName = _targetPath;
                    pProcess.StartInfo.UseShellExecute = false;
                    pProcess.StartInfo.RedirectStandardOutput = true;
                    pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    pProcess.StartInfo.CreateNoWindow = true;
                    pProcess.Start();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class CustomMenuItem_Command : CustomMenuItemBase
    {
        public CustomMenuItem_Command(string name, string command, string arguments, string tooltip) : base(name, tooltip)
        {
            _command = command;
            _arguments = arguments;

            Click += Execute;
        }


        private string _command = "";
        private string _arguments = "";

        private void Execute(object sender, EventArgs args)
        {
            if (Disabled)
            {
                return;
            }

            try
            {
                using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
                {
                    pProcess.StartInfo.FileName = _command;
                    pProcess.StartInfo.UseShellExecute = false;
                    pProcess.StartInfo.RedirectStandardOutput = true;
                    pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    pProcess.StartInfo.CreateNoWindow = true;
                    pProcess.StartInfo.Arguments = _arguments;
                    pProcess.Start();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class CustomMenuItem_Refresh : CustomMenuItemBase
    {
        public CustomMenuItem_Refresh(string name, string tooltip) : base(name, tooltip)
        {
            Click += Refresh;
        }

        private void Refresh(object sender, EventArgs args)
        {
            if (Disabled)
            {
                return;
            }

            // ptodo
            //CurrentContext.Refresh();
        }
    }

    public class CustomMenuItem_Exit : CustomMenuItemBase
    {
        public CustomMenuItem_Exit(string name, string tooltip) : base(name, tooltip)
        {
            Click += Exit;
        }

        private void Exit(object sender, EventArgs args)
        {
            if (Disabled)
            {
                return;
            }

            Application.Exit();
        }
    }

    public class CustomMenuItem_Collection : CustomMenuItemBase
    {
        public CustomMenuItem_Collection(string name, string tooltip, CustomMenuItemBase [] subItems) : base(name, tooltip)
        {
            DropDownItems.AddRange(subItems);
        }
    }
}
