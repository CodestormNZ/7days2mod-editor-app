using _7days2mod_recipe_editor_app.Services;
using _7days2mod_recipe_editor_app.UIElements;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace _7days2mod_recipe_editor_app.UserControls
{
    public partial class BlocksTab : UserControl, INotifyPropertyChanged
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private Config config = new Config();
        public ObservableCollection<Models.Block> _blockData;
        public ObservableCollection<Models.Block> blockData
        {
            get
            {
                return _blockData;
            }
            set
            {
                _blockData = value;
                OnPropertyChanged("_blockData");
                BlocksList.ItemsSource = _blockData;
            }
        }

        public BlocksTab()
        {
            InitializeComponent();
        }

        //INotifyPropertyChanged methods
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void ListViewBlocks_Preview(object sender, MouseButtonEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi != null)
            {
                Models.Block block = (Models.Block)lvi.Content;

                //image
                selectedBlockImage.Source = null;
                if (File.Exists("ItemIcons/" + block.name + ".png"))
                {
                    var uri = new Uri(block.image);
                    var bitmap = new BitmapImage(uri);
                    selectedBlockImage.Source = bitmap;
                }
            }
        }

        private void BlocksListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                BlocksList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            BlocksList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
}
