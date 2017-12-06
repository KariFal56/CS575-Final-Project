using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;

namespace DefectReport
{
    class MainViewModel : INotifyPropertyChanged    //Make sure to capture change events
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Navigation
        private INavigation navigation;

        //Commands
        public ICommand NavigateHistoryCommand { get; private set; }
        public ICommand NavigateEntryCommand { get; private set; }

        string _enteredWorkOrderNumber; 
        public string EnteredWorkOrder
        {
            set
            {
                if (value != null)
                {
                    if (value.Equals(_enteredWorkOrderNumber, StringComparison.Ordinal))
                    {
                        return;
                    }
                    else
                    {
                        _enteredWorkOrderNumber = value;
                        OnPropertyChanged();
                    }
                }
            }
            get
            {
                return _enteredWorkOrderNumber;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public MainViewModel(INavigation _navigation)
        {
            navigation = _navigation;

            NavigateEntryCommand = new Command(async () => await DefectEntryTask());
            NavigateHistoryCommand = new Command(async () => await navigation.PushAsync(new DefectListPage()));
        }

        async Task DefectEntryTask()
    {
        DefectReportItem WorkOrderItem;
        WorkOrderItem = new DefectReportItem();
        WorkOrderItem.WorkOrderNumber = EnteredWorkOrder;

            await navigation.PushAsync(new DefectEntryPage(WorkOrderItem));
       }
    }
}


