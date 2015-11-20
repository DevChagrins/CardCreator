using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows;

namespace GameDesignCardBuilder
{
    public enum CardEnum
    {
        Theme = "Theme",
        Genre = "Genre",
    }
    class CardFileReader
    {
        Dictionary<string, List<GenericCard>> populatedLists;

        const string themeDeckTag = "ThemeDeck";
        const string genreDeckTag = "GenreDeck";

        public CardFileReader()
        {
            populatedLists = new Dictionary<string, List<GenericCard>>();
        }

        public bool ReadCardFile(string file)
        {
            bool readSuccess = false;

            if(File.Exists(file) == true)
            {
                try
                {
                    XmlDocument cardDocument = new XmlDocument();
                    cardDocument.LoadXml(file);

                    XmlNode child = cardDocument.FirstChild;

                    if (child != null)
                    {
                        while (child != null)
                        {
                            XmlNodeList nodeList = child.ChildNodes;
                            switch (child.Name)
                            {
                                case themeDeckTag:
                                    {
                                        ParseThemes(nodeList);
                                        break;
                                    }
                                case genreDeckTag:
                                    {
                                        ParseGenres(nodeList);
                                        break;
                                    }
                            }

                            child = child.NextSibling;
                        }
                    }
                }
                catch(Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }

            return readSuccess;
        }

        void ParseThemes(XmlNodeList themeList)
        {
            string key = CardEnum.Theme.ToString();
            if (populatedLists.ContainsKey(key) == false)
            {
                populatedLists[key] = new List<GenericCard>();
            }

            ThemeCard newTheme = null;
            foreach(XmlNode theme in themeList)
            {
                newTheme = new ThemeCard();

                foreach(XmlNode child in theme.ChildNodes)
                {
                    switch(child.Name)
                    {
                        case "Author":
                            {
                                newTheme.Author = child.Value;
                                break;
                            }
                        case "Theme":
                            {
                                newTheme.Theme = child.Value;
                                break;
                            }
                        case "Type":
                            {
                                newTheme.Type = child.Value;
                                break;
                            }
                    }
                }

                populatedLists[key].Add(newTheme);
            }
        }

        void ParseGenres(XmlNodeList genreList)
        {
            string key = CardEnum.Genre.ToString();
            if (populatedLists.ContainsKey(key) == false)
            {
                populatedLists[key] = new List<GenericCard>();
            }
        }
    }
}
