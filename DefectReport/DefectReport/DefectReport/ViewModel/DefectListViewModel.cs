using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace DefectReport
{
    class DefectListViewModel : INotifyPropertyChanged    //Make sure to capture change events
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Add observablecollection for defect list?

        //Navigation
        private INavigation navigation;

        //Binding Data
        int _id;
        public int Id
        {
            set
            {
                if (value.Equals(_id))
                {
                    return;
                }
                else
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return Id;
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

        public async void ViewDefect(SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                DefectReportItem selectedDefect = new DefectReportItem();
                selectedDefect = e.SelectedItem as DefectReportItem;
                await navigation.PushAsync(new DefectEntryPage(selectedDefect));
            }
          }

        public DefectListViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            Debug.WriteLine(LoadListTask());
        }

        private async Task<string> LoadListTask()
        {
            return await Task.Run(async () =>
            {
                // Reset the 'resume' id, since we just want to re-start here
                ((App)App.Current).ResumeAtDefectReportId = -1;
                await App.Database.GetItemsAsync(); 
                return "Thanks for waiting";         //Update Text       
            });
        }
    }
}
