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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel != null)
            {
                 await viewModel.PopulateCollection();
            }
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (viewModel != null)
            {
                await viewModel.ViewDefect(e);
            }
        }
    }
}
