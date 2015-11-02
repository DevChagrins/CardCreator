using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesignCardBuilder
{
    public class ThemeCard : GenericCard
    {
        public string Theme { get; set; }
        public string Type { get; set; }

        public ThemeCard() : this("")
        {
        }

        public ThemeCard(string author) : base(author)
        {
            Theme = "";
            Type = "";
        }

        public ThemeCard(ThemeCard clone) : this(clone.Author)
        {
            Theme = clone.Theme;
            Type = clone.Type;
        }

        public override void Clone(GenericCard clone)
        {
            if (clone != null)
            {
                ThemeCard cloneTheme = (ThemeCard)clone;
                Theme = cloneTheme.Theme;
                Type = cloneTheme.Type;
                Author = cloneTheme.Author;
            }
        }

        // Functions that are overridden
        #region Overrides

        public override string ToString()
        {
            return Theme + "\t\t" + Type + "\t\t" + base.ToString();
        }

        public override string GetStringType()
        {
            return "Theme";
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;

            if (obj != null)
            {
                ThemeCard checkCard = (ThemeCard)obj;

                if (Theme.Equals(checkCard.Theme, StringComparison.CurrentCultureIgnoreCase))
                {
                    isEqual = true;
                }
            }

            return isEqual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
