using System.Windows.Forms;
using SystemTrayLauncher.Settings;
using System.Drawing;
using System.IO;

namespace SystemTrayLauncher.MenuItems
{
    public class CustomMenuItemBase : ToolStripMenuItem
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

            Disabled = false;
        }

        public bool Disabled { get; set; }

        private void CustomMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (Disabled)
            {
                return;
            }

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
            if (Disabled)
            {
                return;
            }

            if (GetCurrentParent() == null)
            {
                return;
            }

            MenuRenderer parentRenderer = (MenuRenderer)GetCurrentParent().Renderer;
            MenuRendererColors colorTable = (MenuRendererColors)parentRenderer.ColorTable;
            colorTable.SelectedColor = GlobalSettings.COLOR_SELECTED;
        }
    }
}
