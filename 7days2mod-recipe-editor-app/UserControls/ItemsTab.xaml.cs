using _7days2mod_recipe_editor_app.Models;
using _7days2mod_recipe_editor_app.Services;
using _7days2mod_recipe_editor_app.UIElements;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ItemsList.ItemsSource);
                view.Filter = iNameFilter;
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
                try
                {
                    var uri = new Uri(item.image);
                    var bitmap = new BitmapImage(uri);
                    selectedItemImage.Source = bitmap;
                }
                catch
                {

                }
                selectedItemName.Text = item.name;
                selectedItemID.Text = " (" + item.id + ")";
                ObservableCollection<Models.Item> viewitem = new ObservableCollection<Models.Item>();
                viewitem.Add(item);
                selectedItemPropertiesList.ItemsSource = viewitem;
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

        //editing
        private void property_edit_Click(object sender, RoutedEventArgs e)
        {
            //do stuff
        }

        //Filters
        private bool iNameFilter(object item)
        {
            if (string.IsNullOrEmpty(itemNameFilter.Text))
                return true;
            else
                return ((item as Models.Item).name.IndexOf(itemNameFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void itemNameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ItemsList.ItemsSource).Refresh();
        }

        private void filterClear_Click(object sender, RoutedEventArgs e)
        {
            itemNameFilter.Text = "";
        }
    }
}
