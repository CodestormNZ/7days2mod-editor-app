using _7days2mod_recipe_editor_app.Models;
using _7days2mod_recipe_editor_app.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using WinForms = System.Windows.Forms;


namespace _7days2mod_recipe_editor_app
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Recipe> _recipeData;
        public ObservableCollection<Recipe> recipeData
        {
            get
            {
                return _recipeData;
            }
            set
            {
                _recipeData = value;
                OnPropertyChanged("_recipeData");
            }
        }
        private ObservableCollection<Block> _blockData;
        public ObservableCollection<Block> blockData
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
        private ObservableCollection<Item> _itemData;
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
            }
        }

        private Config config = new Config();

        public MainWindow()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            XMLService xmlloader = new XMLService(config);

            //set datacontext so we can access properties via Bindings in xaml
            DataContext = this;

            //load data from xml files
            recipeData = new ObservableCollection<Recipe>();
            blockData = new ObservableCollection<Block>();
            itemData = new ObservableCollection<Item>();
            itemData = xmlloader.loadItems(config.defaultFolder);
            blockData = xmlloader.loadBlocks(config.defaultFolder);
            recipeData = xmlloader.loadRecipes(config.defaultFolder);

            InitializeComponent();

            //set data in user controls
            itemsTab.itemData = itemData;
            blocksTab.blockData = blockData;
            recipesTab.recipeData = recipeData;
            recipesTab.itemData = itemData;
            recipesTab.blockData = blockData;

            //set folder path in options
            selectedFolder.Text = config.defaultFolder;

            stopwatch.Stop();
            statusNotice.Text = "XML Loaded (" + stopwatch.ElapsedMilliseconds + " ms)";
        }

        //INotifyPropertyChanged methods
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        // OPTIONS TAB
        public WinForms.FolderBrowserDialog folderBrowserDialog { get; private set; }

        private void folderButton_Click(object sender, RoutedEventArgs e)
        {
            folderBrowserDialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == WinForms.DialogResult.OK) {
                selectedFolder.Text = folderBrowserDialog.SelectedPath;
                //TODO: Update config and refresh data models
                //TODO: Store path to app settings so it persists between executions
                //TODO: Notify if path does not contain required files.
            }
        }

    }
}
