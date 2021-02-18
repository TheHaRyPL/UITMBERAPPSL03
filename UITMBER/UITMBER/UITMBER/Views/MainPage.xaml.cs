using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITMBER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace UITMBER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            //  var mapSpan = MapSpan.FromCenterAndRadius(new Position(), new Distance());

            // mapControl.MoveToRegion(mapSpan);
        }


        protected override void OnAppearing()
        {

            (BindingContext as MainViewModel).GetCurrentLocationCommand.Execute(true);

            base.OnAppearing();
        }
    }
}