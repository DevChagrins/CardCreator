using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;

namespace GameDesignCardBuilder
{
    class ViewModel
    {
        // Private data
        #region Data

        #region Commands
        ICommand saveCommand;
        #endregion

        string username;

        ObservableCollection<GenericCard> themesList;
        ThemeCard newThemeCard;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        // All of the functions for the backend
        #region Functions

        public ViewModel()
        {
            username = "Malcolm";
            themesList = new ObservableCollection<GenericCard>();
            newThemeCard = new ThemeCard(username);

            ThemeCard tryIt = new ThemeCard();
            tryIt.Theme = "Hello";
            tryIt.Type = "The type";
            tryIt.Author = "Malcolm";
            themesList.Add(tryIt);
        }

        public void SignalPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void SubmitItem(object card)
        {
            if (card != null)
            {
                GenericCard cardToSubmit = (GenericCard)card;

                GenericCard cloneCard = null;
                ObservableCollection<GenericCard> checkList = null;
                switch (cardToSubmit.GetStringType())
                {
                    case "Theme":
                        {
                            checkList = themesList;
                            cloneCard = new ThemeCard();
                            break;
                        }
                    default:
                        break;
                }

                GenericCard foundCard = null;
                if(checkList != null)
                {
                    foreach(GenericCard listedCard in checkList)
                    {
                        if(listedCard.Equals(card))
                        {
                            foundCard = listedCard;
                            break;
                        }
                    }

                    if(foundCard == null)
                    {
                        cloneCard.Clone(cardToSubmit);
                        checkList.Add(cloneCard);
                    }
                }
            }
        }

        public bool CheckSubmission(object card)
        {
            bool canSubmit = false;

            if(card != null)
            {
                GenericCard cardToSubmit = (GenericCard)card;
                canSubmit = cardToSubmit.CheckSubmit();
            }

            return canSubmit;
        }

        #endregion

        // Bindable properties for the view model
        #region Bindable Properties
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new ConditionalCommand(
                        param => this.SubmitItem(param),
                        param => this.CheckSubmission(param)
                    );
                }
                return saveCommand;
            }
        }

        public String UserName
        {
            get { return username; }
            set 
            { 
                username = value;

                if(newThemeCard != null)
                {
                    newThemeCard.Author = username;
                }
            }
        }

        public ObservableCollection<GenericCard> ThemesList
        {
            get { return themesList; }
            set { }
        }

        public ThemeCard NewThemeCard
        {
            get { return newThemeCard; }
            set { newThemeCard = value; }
        }
        #endregion
    }

    class ConditionalCommand : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;        

        public ConditionalCommand(Action<object> executeFunction)
            : this(executeFunction, null)
        {
        }

        public ConditionalCommand(Action<object> executeFunction, Predicate<object> canExecuteFunction)
        {
            if (executeFunction == null)
                throw new ArgumentNullException("execute");

            execute = executeFunction;
            canExecute = canExecuteFunction;           
        }
            
        public bool CanExecute(object parameters)
        {
            return canExecute == null ? true : canExecute(parameters);
        }
        
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        public void Execute(object parameters)
        {
            execute(parameters);
        }
    }
}
