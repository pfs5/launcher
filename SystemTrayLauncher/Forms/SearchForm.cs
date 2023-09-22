using System;
using System.Windows.Forms;
using SystemTrayLauncher.Settings;
using SystemTrayLauncher.LaunchManagement;
using SystemTrayLauncher.LaunchItems;

namespace SystemTrayLauncher.Forms
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            BackColor = GlobalSettings.COLOR_BACKGROUND;
        }

        protected override void OnShown(EventArgs e)
        {
            RefreshSearchResults();
        }

        private class SearchResultItem
        {
            public SearchResultItem(LaunchItemBase item)
            {
                _item = item;
            }

            public void Execute()
            {
                _item.Execute();
            }

            public override string ToString()
            {
                return _item.Name;
            }

            private LaunchItemBase _item;
        }

        private void RefreshSearchResults()
        {
            SearchResultsList.Items.Clear();

            string text = InputField.Text.ToLower().Trim();
            string [] tokens = text.Split(' ');

            int numItems = 0;
            foreach (LaunchItemBase item in Launcher.Instance.Items)
            {
                if (!item.IsSearchable)
                {
                    continue;
                }

                bool containsAll = true;
                foreach (string token in tokens)
                {
                    if (!item.Name.ToLower().Contains(token))
                    {
                        containsAll = false;
                        break;
                    }
                }

                if (containsAll)
                {
                    SearchResultsList.Items.Add(new SearchResultItem(item));
                    numItems++;
                }
            }

            if (numItems > 0)
            {
                SearchResultsList.SelectedIndex = 0;
            }
        }

        #region Events
        private void SearchForm_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void SearchForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void InputField_TextChanged(object sender, EventArgs e)
        {
            RefreshSearchResults();
        }
        #endregion

        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                int numSearchResults = SearchResultsList.Items.Count;
                if(numSearchResults > 1)
                {
                    SearchResultsList.SelectedIndex = (SearchResultsList.SelectedIndex + 1) % numSearchResults;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                int numSearchResults = SearchResultsList.Items.Count;
                if (numSearchResults > 1)
                {
                    int newIndex = (SearchResultsList.SelectedIndex - 1) % numSearchResults;
                    if (newIndex < 0)
                    {
                        newIndex += numSearchResults;
                    }

                    SearchResultsList.SelectedIndex = newIndex;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SearchResultItem item = null;
                try
                {
                    item = SearchResultsList.Items[SearchResultsList.SelectedIndex] as SearchResultItem;
                }
                catch { }

                if (item != null)
                {
                    item.Execute();
                }

                Close();
            }
        }

        private void InputField_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void InputField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

    }
}
