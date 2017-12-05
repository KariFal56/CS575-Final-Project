using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DefectReport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefectListPage : ContentPage
    {
        private DefectListViewModel viewModel;  

        public DefectListPage()
        {
            InitializeComponent();
            viewModel = new DefectListViewModel(this.Navigation);
            this.BindingContext = viewModel;
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            //((App)App.Current).ResumeAtDefectReportId = -1;
            //DefectHistory.ItemsSource = await App.Database.GetItemsAsync();
        //}

        //Should do this async//

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //viewModel.ViewDefect(e);
            if (e.SelectedItem != null)
            {
                DefectReportItem selectedDefect = new DefectReportItem();
                selectedDefect = e.SelectedItem as DefectReportItem;
                await Navigation.PushAsync(new DefectEntryPage(selectedDefect));
            }
        }
    }
}
