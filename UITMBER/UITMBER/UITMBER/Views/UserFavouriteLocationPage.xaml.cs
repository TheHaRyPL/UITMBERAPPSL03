using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITMBER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UITMBER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserFavouriteLocationPage : ContentPage
    {
        UserFavouriteLocationsViewModel _viewModel;
        public UserFavouriteLocationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new UserFavouriteLocationsViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}