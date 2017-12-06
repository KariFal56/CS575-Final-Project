using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace DefectReport
{
    class DefectListViewModel : INotifyPropertyChanged    //Make sure to capture change events
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<DefectReportItem> _defectReportList;
        public ObservableCollection<DefectReportItem> DefectReportList
        {
            get
            {
                return _defectReportList;
            }
            set
            {
                if (Equals(value, _defectReportList)) return;

                _defectReportList = value;
                OnPropertyChanged();
            }
        }

        //Navigation
        private INavigation navigation;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task ViewDefect(SelectedItemChangedEventArgs e)
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
        }

        async public Task PopulateCollection()
        {
            DefectReportList = new ObservableCollection<DefectReportItem>(await App.Database.GetItemsAsync());
        }
    }
}
