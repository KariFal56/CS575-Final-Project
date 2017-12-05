using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace DefectReport
{
    class MainViewModel : INotifyPropertyChanged    //Make sure to capture change events
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
        public Command NavigateHistoryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await navigation.PushAsync(new DefectListPage());
                });
            }
        }

        public Command NavigateEntryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrWhiteSpace(EnteredWorkOrder) && EnteredWorkOrder.Length < 40)
                    {
                        WorkOrderItem = new DefectReportItem();
                        WorkOrderItem.WorkOrderNumber = EnteredWorkOrder;

                        await navigation.PushAsync(new DefectEntryPage(WorkOrderItem));
                    }
                });
            }
        }

        //Work Order Information to pass to Defect Entry Page
        public DefectReportItem WorkOrderItem { get; set; }

        string _enteredWorkOrderNumber="123456789";  //Set Default Value
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
                        OnPropertyChanged(nameof(EnteredWorkOrder));
                    }
                }
                else
                {
                    value = _enteredWorkOrderNumber;
                }
            }
            get
            {
                return EnteredWorkOrder;
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
        }
    }
}


