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
    public partial class OrdersPage : ContentPage
    {
        OrdersViewModel _viewModel;

        public OrdersPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new OrdersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}