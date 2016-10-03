using _7days2mod_recipe_editor_app.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;

namespace _7days2mod_recipe_editor_app.Services
{
    class XMLService
    {
        private string blocksXMLfile { get; set; }
        private string itemsXMLfile { get; set; }
        private string recipesXMLfile { get; set; }
        public ObservableCollection<Recipe> recipeData;
        public ObservableCollection<Block> blockData;
        public ObservableCollection<Item> itemData;
        private Config _config;

        public XMLService (Config config)
        {
            _config = config;
            recipeData = new ObservableCollection<Recipe>();
            blockData = new ObservableCollection<Block>();
            itemData = new ObservableCollection<Item>();
        }

        public ObservableCollection<Block> loadBlocks(string _selectedFolder)
        {
            if (File.Exists(_selectedFolder + _config.blocksXMLfile))
            {
                XDocument doc = XDocument.Load(_selectedFolder + _config.blocksXMLfile);
                var blocks = doc.Descendants("block");
                var blockindex = 0;
                foreach (var block in blocks)
                {
                    Block iblock = new Block();
                    iblock.index = blockindex++;
                    foreach (var attr in block.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "id":
                                iblock.id = (string)block.Attribute("id");
                                break;
                            case "name":
                                iblock.name = (string)block.Attribute("name");
                                break;
                        }
                    }
                    ObservableCollection<BlockProperty> properties = new ObservableCollection<BlockProperty>();
                    foreach (var propertyElement in block.Elements("property"))
                    {
                        BlockProperty property = new BlockProperty();
                        if (string.IsNullOrEmpty((string)propertyElement.Attribute("class")))
                        {
                            foreach (var propattr in propertyElement.Attributes())
                            {
                                switch (propattr.Name.LocalName)
                                {
                                    case "name":
                                        property.name = (string)propertyElement.Attribute("name");
                                        break;
                                    case "value":
                                        property.value = (string)propertyElement.Attribute("value");
                                        break;
                                    case "param1":
                                        property.param1 = (string)propertyElement.Attribute("param1");
                                        break;
                                }
                            }
                            properties.Add(property);
                        }
                        else
                        {
                            property._class = (string)propertyElement.Attribute("class");
                            foreach (var classpropElement in propertyElement.Elements("property"))
                            {
                                BlockClassProperty classproperty = new BlockClassProperty();
                                foreach (var classpropattr in classpropElement.Attributes())
                                {
                                    switch (classpropattr.Name.LocalName)
                                    {
                                        case "name":
                                            classproperty.name = (string)classpropElement.Attribute("name");
                                            break;
                                        case "value":
                                            classproperty.value = (string)classpropElement.Attribute("value");
                                            break;
                                    }
                                }
                                if (property.classproperties == null)
                                {
                                    property.classproperties = new ObservableCollection<BlockClassProperty>();
                                }
                                property.classproperties.Add(classproperty);
                            }
                            properties.Add(property);
                        }
                    }

                    ObservableCollection<BlockDrop> drops = new ObservableCollection<BlockDrop>();
                    foreach (var dropElement in block.Elements("drop"))
                    {
                        BlockDrop drop = new BlockDrop();
                        if (string.IsNullOrEmpty((string)dropElement.Attribute("class")))
                        {
                            foreach (var dropattr in dropElement.Attributes())
                            {
                                switch (dropattr.Name.LocalName)
                                {
                                    case "event":
                                        drop._event = (string)dropElement.Attribute("event");
                                        break;
                                    case "name":
                                        drop.name = (string)dropElement.Attribute("name");
                                        break;
                                    case "count":
                                        drop.count = (string)dropElement.Attribute("count");
                                        break;
                                    case "prob":
                                        drop.prob = (string)dropElement.Attribute("prob");
                                        break;
                                    case "stick_chance":
                                        drop.stick_chance = (string)dropElement.Attribute("stick_chance");
                                        break;
                                    case "tool_category":
                                        drop.tool_category = (string)dropElement.Attribute("tool_category");
                                        break;
                                }
                            }
                            drops.Add(drop);
                        }
                    }
                    iblock.properties = properties;
                    iblock.drops = drops;
                    blockData.Add(iblock);
                }
            }
            return blockData;
        }

        public ObservableCollection<Item> loadItems(string _selectedFolder)
        {
            if (File.Exists(_selectedFolder + _config.itemsXMLfile))
            {
                XDocument doc = XDocument.Load(_selectedFolder + _config.itemsXMLfile);
                var items = doc.Descendants("item");
                var itemindex = 0;
                foreach (var item in items)
                {
                    Item iitem = new Item();
                    iitem.index = itemindex++;
                    foreach (var attr in item.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "id":
                                iitem.id = (string)item.Attribute("id");
                                break;
                            case "name":
                                iitem.name = (string)item.Attribute("name");
                                break;
                        }
                    }
                    ObservableCollection<ItemProperty> properties = new ObservableCollection<ItemProperty>();
                    foreach (var propertyElement in item.Elements("property"))
                    {
                        ItemProperty property = new ItemProperty();
                        if (string.IsNullOrEmpty((string)propertyElement.Attribute("class")))
                        {
                            foreach (var propattr in propertyElement.Attributes())
                            {
                                switch (propattr.Name.LocalName)
                                {
                                    case "name":
                                        property.name = (string)propertyElement.Attribute("name");
                                        break;
                                    case "value":
                                        property.value = (string)propertyElement.Attribute("value");
                                        break;
                                    case "param1":
                                        property.param1 = (string)propertyElement.Attribute("param1");
                                        break;
                                }
                            }
                            properties.Add(property);
                        }
                        else
                        {
                            property._class = (string)propertyElement.Attribute("class");
                            foreach (var classpropElement in propertyElement.Elements("property"))
                            {
                                ItemClassProperty classproperty = new ItemClassProperty();
                                foreach (var classpropattr in classpropElement.Attributes())
                                {
                                    switch (classpropattr.Name.LocalName)
                                    {
                                        case "name":
                                            classproperty.name = (string)classpropElement.Attribute("name");
                                            break;
                                        case "value":
                                            classproperty.value = (string)classpropElement.Attribute("value");
                                            break;
                                        case "param1":
                                            classproperty.param1 = (string)classpropElement.Attribute("param1");
                                            break;
                                    }
                                }
                                if (property.classproperties == null)
                                {
                                    property.classproperties = new ObservableCollection<ItemClassProperty>();
                                }
                                property.classproperties.Add(classproperty);
                            }
                            properties.Add(property);
                        }
                    }

                    iitem.properties = properties;
                    itemData.Add(iitem);
                }
            }
            return itemData;
        }

        public ObservableCollection<Recipe> loadRecipes(string _selectedFolder)
        {
            if (File.Exists(_selectedFolder + _config.recipesXMLfile))
            {
                XDocument doc = XDocument.Load(_selectedFolder + _config.recipesXMLfile);
                var recipes = doc.Descendants("recipe");
                var recipeindex = 0;
                foreach (var recipe in recipes)
                {
                    Recipe irecipe = new Recipe();
                    irecipe.index = recipeindex++;
                    foreach (var attr in recipe.Attributes())
                    {
                        switch (attr.Name.LocalName)
                        {
                            case "name":
                                irecipe.name = (string)recipe.Attribute("name");
                                break;
                            case "count":
                                irecipe.count = (string)recipe.Attribute("count");
                                break;
                            case "craft_area":
                                irecipe.craft_area = (string)recipe.Attribute("craft_area");
                                break;
                            case "craft_tool":
                                irecipe.craft_tool = (string)recipe.Attribute("craft_tool");
                                break;
                            case "craft_exp_gain":
                                irecipe.craft_exp_gain = (string)recipe.Attribute("craft_exp_gain");
                                break;
                            case "craft_time":
                                irecipe.craft_time = (string)recipe.Attribute("craft_time");
                                break;
                            case "material_based":
                                irecipe.material_based = (bool)recipe.Attribute("material_based");
                                break;
                            case "tooltip":
                                irecipe.tooltip = (string)recipe.Attribute("tooltip");
                                break;
                        }
                    }
                    ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
                    foreach (var ingredientElement in recipe.Elements("ingredient"))
                    {
                        Ingredient ingredient = new Ingredient();
                        foreach (var ingattr in ingredientElement.Attributes())
                        {
                            switch (ingattr.Name.LocalName)
                            {
                                case "name":
                                    ingredient.name = (string)ingredientElement.Attribute("name");
                                    break;
                                case "count":
                                    ingredient.count = (string)ingredientElement.Attribute("count");
                                    break;
                            }
                        }
                        ingredients.Add(ingredient);
                    }
                    irecipe.ingredients = ingredients;
                    recipeData.Add(irecipe);
                }
            }
            return recipeData;
        }

    }
}
