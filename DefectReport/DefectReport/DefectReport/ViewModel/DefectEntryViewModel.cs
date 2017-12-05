using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace DefectReport
{
    class DefectEntryViewModel : INotifyPropertyChanged    //Make sure to capture change events
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Navigation
        private INavigation navigation;

        //Need an ICommand to implement Commanding
        public interface ICommand
        {
            void Execute(object arg);
            bool CanExecute(object arg);
            event EventHandler CanExecuteChanged;
        }

        //Commands
        public Command UpdateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newDefect = new DefectReportItem();
                    newDefect.WorkOrderNumber = WorkOrderNumber;
                    newDefect.Count = Count;
                    newDefect.Defect = Defect;
                    newDefect.Disposition = Disposition;
                    newDefect.Date = Date;
                    newDefect.Id = Id;

                    //Add/Update Defect Item
                    await App.Database.SaveItemAsync(newDefect);
                    await navigation.PopAsync();
                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var oldDefect = new DefectReportItem();
                    oldDefect.Id = Id;

                    //Check that id is not zero
                    if (oldDefect.Id != 0)
                    {
                        await App.Database.DeleteItemAsync(oldDefect);
                    }
                    await navigation.PopAsync();
                });
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await navigation.PopAsync();
                });
            }
        }

        #region BindingData

        public int Id { set; get; }  //This will not be changed here
        public string WorkOrderNumber { set; get; }  //ReadyOnly, can't be changed here

        int _count;
        public int Count
        {
            set
            {
                if (value.Equals(_count))
                {
                    return;
                }
                else
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return Count;
            }
        }

        string _defect;
        public string Defect
        {
            set
            {
                if (value.Equals(_defect, StringComparison.Ordinal))
                {
                    return;
                }
                else
                {
                    _defect = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return Defect;
            }
        }

        string _disposition;
        public string Disposition
        {
            set
            {
                if (value.Equals(_disposition, StringComparison.Ordinal))
                {
                    return;
                }
                else
                {
                    _disposition = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return Disposition;
            }
        }

        DateTime _date;
        public DateTime Date
        {
            set
            {
                if (value.Equals(_date))
                {
                    return;
                }
                else
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return Date;
            }
        }

        #endregion

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DefectEntryViewModel(INavigation _navigation, DefectReportItem newDefect)
        {
            navigation = _navigation;

            //set passed model parameters
            WorkOrderNumber = newDefect.WorkOrderNumber;

        }
    }
}
