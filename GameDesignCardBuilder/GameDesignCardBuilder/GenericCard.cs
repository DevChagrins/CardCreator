using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameDesignCardBuilder
{
    public class GenericCard
    {
        public string Author { get; set; }
        public GenericCard()
        {
            Author = "";
        }

        public GenericCard(string author)
        {
            Author = author;
        }

        public GenericCard(GenericCard clone)
        {
            Author = clone.Author;
        }

        public virtual void Clone(GenericCard clone)
        {
            if(clone != null)
            {
                Author = clone.Author;
            }
        }

        public virtual bool CheckSubmit()
        {
            bool success = false;

            if (String.IsNullOrEmpty(Author) == false)
            {
                success = true;
            }
            
            return success;
        }

        public virtual string GetStringType()
        {
            return "Generic";
        }

        public virtual void WriteData(XmlWriter writer)
        {
            writer.WriteElementString("Author", Author);
        }

        // Functions that are overridden
        #region Overrides

        public override bool Equals(object obj)
        {
            return false;
        }
        
        public override string ToString()
        {
            return Author;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
