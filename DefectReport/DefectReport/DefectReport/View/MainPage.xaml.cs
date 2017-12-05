using Xamarin.Forms;

namespace DefectReport
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel(this.Navigation); 
            
        }
    }
}
