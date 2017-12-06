using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DefectReport
{
    class DefectEntryViewModel : INotifyPropertyChanged    //Make sure to capture change events
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Navigation
        private INavigation navigation;

        //Commands
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #region BindingData

        public int Id { set; get; }  //This will not be changed here
        public string WorkOrderNumber { set; get; }  //ReadyOnly, can't be changed here

        int _count = 1;
        public int Count
        {
            set
            {
                if (value != 0)
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
                else
                {
                    Count = _count;
                }
            }
            get => _count;
        }

        string _defect;
        public string Defect
        {
            set
            {
                if (value != null)
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
            }
            get => _defect;
        }

        string _disposition;
        public string Disposition
        {
            set
            {
                if (value != null)
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
            }
            get => _disposition;
        }

        DateTime _date = DateTime.Now;
        public DateTime Date
        {
            set
            {
                if (value != null)
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
                else
                {
                    Date = _date;
                }
            }
            get => _date;
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

            UpdateCommand = new Command(async () => await UpdateDBItemAsync());
            DeleteCommand = new Command(async () => await DeleteDBItemAsync());
            CancelCommand = new Command(async () => await navigation.PopAsync());

            //set passed model parameters
            WorkOrderNumber = newDefect.WorkOrderNumber;
            if (newDefect.Id!=0)
            {
                Id = newDefect.Id;
                Date = newDefect.Date;
                Defect = newDefect.Defect;
                Disposition = newDefect.Disposition;
                Count = newDefect.Count;
            }

        }

        //Tasks
        async Task UpdateDBItemAsync()
    {
        var newDefect = new DefectReportItem();
        newDefect.WorkOrderNumber = WorkOrderNumber;
        newDefect.Count = Count;
        newDefect.Defect = Defect;
        newDefect.Disposition = Disposition;
        newDefect.Date = Date;
        newDefect.Id = Id;

        await App.Database.SaveItemAsync(newDefect);
            await navigation.PopAsync();
        }

        async Task DeleteDBItemAsync()
    {
        var oldDefect = new DefectReportItem();
        oldDefect.Id = Id;

        //Check that id is not zero
        if (oldDefect.Id != 0)
        {
            await App.Database.DeleteItemAsync(oldDefect);
        }
        await navigation.PopAsync();
    }

    }
}
