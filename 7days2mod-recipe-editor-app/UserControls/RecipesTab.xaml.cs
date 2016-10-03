using _7days2mod_recipe_editor_app.Services;
using _7days2mod_recipe_editor_app.UIElements;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace _7days2mod_recipe_editor_app.UserControls
{
    public partial class RecipesTab : UserControl, INotifyPropertyChanged
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private Config config = new Config();
        public ObservableCollection<Models.Recipe> _recipeData;
        public ObservableCollection<Models.Recipe> recipeData
        {
            get
            {
                return _recipeData;
            }
            set
            {
                _recipeData = value;
                OnPropertyChanged("_recipeData");
                RecipesList.ItemsSource = _recipeData;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(RecipesList.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("craft_area_view");
                view.GroupDescriptions.Add(groupDescription);
                view.SortDescriptions.Add(new SortDescription("craft_area_view", ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("craft_tool", ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));
                view.Filter = rNameFilter;
            }
        }
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
            }
        }
        public ObservableCollection<Models.Item> _itemData;
        public ObservableCollection<Models.Item> itemData
        {
            get
            {
                return _itemData;
            }
            set
            {
                _itemData = value;
                OnPropertyChanged("_itemData");
            }
        }

        public RecipesTab()
        {
            InitializeComponent();
        }

        //INotifyPropertyChanged methods
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void ListViewRecipes_Preview(object sender, MouseButtonEventArgs e)
        {
            var lvi = sender as ListViewItem;
            if (lvi != null)
            {
                Models.Recipe recipe = (Models.Recipe)lvi.Content;

                //image
                selectedRecipeImage.Source = null;
                if (File.Exists("ItemIcons/" + recipe.name + ".png"))
                {
                    var uri = new Uri(recipe.image);
                    var bitmap = new BitmapImage(uri);
                    selectedRecipeImage.Source = bitmap;
                }
                selectedRecipeName.Text = recipe.name;
                selectedRecipeCount.Text = recipe.count;
                selectedRecipeCraftArea.Text = recipe.craft_area;
                selectedRecipeCraftTool.Text = recipe.craft_tool;
                selectedRecipeCraftExpGain.Text = recipe.craft_exp_gain;
                selectedRecipeCraftExpGainCalc.Text = recipe.craft_exp_gain; //calculate from ingredients
                selectedRecipeCraftTime.Text = recipe.craft_time;
                selectedRecipeCraftTimeCalc.Text = recipe.craft_time; //calculate from ingredients
                selectedRecipeMaterialBased.Text = "" + recipe.material_based;
                selectedRecipeTooltip.Text = recipe.tooltip;
                ObservableCollection<Models.Recipe> viewrecipe = new ObservableCollection<Models.Recipe>();
                viewrecipe.Add(recipe);
                selectedRecipeIngredientsList.ItemsSource = viewrecipe;
            }
        }

        private void RecipesListColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                RecipesList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            RecipesList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private bool rNameFilter(object recipe)
        {
            if (string.IsNullOrEmpty(recipeNameFilter.Text))
                return true;
            else
                return ((recipe as Models.Recipe).name.IndexOf(recipeNameFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void recipeNameFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RecipesList.ItemsSource).Refresh();
        }

        private void recipeDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var index = ((Button)sender).Tag;
            var recipe = recipeData.LastOrDefault(p => p.index == (int)index);
            recipeData.Remove(recipe);
            CollectionViewSource.GetDefaultView(RecipesList.ItemsSource).Refresh();
        }

        private void filterClear_Click(object sender, RoutedEventArgs e)
        {
            recipeNameFilter.Text = "";
        }

        private void recipeAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
