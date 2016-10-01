using _7days2mod_recipe_editor_app.Models;
using _7days2mod_recipe_editor_app.Services;
using _7days2mod_recipe_editor_app.UIElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _7days2mod_recipe_editor_app.UserControls
{
    public partial class ItemsTab : UserControl, INotifyPropertyChanged
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private Config config = new Config();
        public ObservableCollection<Item> _itemData;
        public ObservableCollection<Item> itemData
        {
            get
            {
                return _itemData;
            }
            set
            {
                _itemData = value;
                OnPropertyChanged("_itemData");
                ItemsList.ItemsSource = _itemData;
            }
        }

        public ItemsTab()
        {
            InitializeComponent();
        }

        //INotifyPropertyChanged methods
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void ListViewItems_Preview(object sender, MouseButtonEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi != null)
            {
                Item item = (Item)lvi.Content;

                //image
                selectedItemImage.Source = null;
                if (File.Exists("ItemIcons/" + item.name + ".png"))
                {
                    var uri = new Uri(item.image);
                    var bitmap = new BitmapImage(uri);
                    selectedItemImage.Source = bitmap;
                }
            }
        }

        private void ItemsListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                ItemsList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ItemsList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
}
