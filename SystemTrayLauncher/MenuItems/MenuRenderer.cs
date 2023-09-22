using System.Drawing;
using System.Windows.Forms;
using SystemTrayLauncher.Settings;

namespace SystemTrayLauncher.MenuItems
{
    public class MenuRenderer : ToolStripProfessionalRenderer
    {
        public MenuRenderer() : base(new MenuRendererColors()) { }
    }

    public class MenuRendererColors : ProfessionalColorTable
    {
        public Color SelectedColor { get; set; }

        public MenuRendererColors()
        {
            SelectedColor = GlobalSettings.COLOR_SELECTED;
        }

        public override Color MenuItemSelected
        {
            get { return SelectedColor; }
        }

        public override Color MenuItemBorder
        {
            get { return SelectedColor; }
        }

        public override Color MenuBorder
        {
            get { return GlobalSettings.COLOR_TEXT; }
        }
    }
}
