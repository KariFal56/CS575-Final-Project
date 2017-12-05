using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DefectReport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel(this.Navigation); 
            
        }
    }
}
