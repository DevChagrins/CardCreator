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
    class CardFileWriter
    {
        public Dictionary<string, List<GenericCard>> populatedLists;

        const string themeDeckTag = "ThemeDeck";
        const string genreDeckTag = "GenreDeck";
        const string cardTag = "Card";

        public CardFileWriter()
        {
            populatedLists = new Dictionary<string, List<GenericCard>>();
        }

        public void WriteCardFile(string file)
        {
            if (File.Exists(file) == true)
            {
                try
                {
                    XmlWriter writer = XmlWriter.Create(file);

                    writer.WriteStartDocument();

                    foreach(KeyValuePair<string, List<GenericCard>> deck in populatedLists)
                    {
                        string deckName = deck.Key + "Deck";
                        writer.WriteStartElement(deckName);

                        foreach(GenericCard card in deck.Value)
                        {
                            card.WriteData(writer);
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndDocument();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString());
                }
            }
        }
    }
}
