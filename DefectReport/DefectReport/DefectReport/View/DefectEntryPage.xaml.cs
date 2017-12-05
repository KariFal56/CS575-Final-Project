using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DefectReport
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefectEntryPage : ContentPage
    {
        public DefectEntryPage(DefectReportItem newDefect)
        {
            InitializeComponent();
            this.BindingContext = new DefectEntryViewModel(this.Navigation, newDefect); 
        }
    }
}