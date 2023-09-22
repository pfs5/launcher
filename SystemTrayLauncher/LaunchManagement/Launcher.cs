using System.Collections.Generic;
using SystemTrayLauncher.LaunchItems;

namespace SystemTrayLauncher.LaunchManagement
{
    public class Launcher
    {
        public static Launcher Instance = new Launcher();

        public LaunchItemBase AddItem(LaunchItemBase item)
        {
            _items.Add(item);
            return item;
        }

        public List<LaunchItemBase> Items { get { return _items; } }

        #region
        private List<LaunchItemBase> _items = new List<LaunchItemBase>();
        #endregion
    }
}
