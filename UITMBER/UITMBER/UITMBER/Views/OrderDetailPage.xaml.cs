using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITMBER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UITMBER.Models.Orders;

namespace UITMBER.Views
{
    public partial class OrderDetailPage : ContentPage
    {

        public OrderDetailPage()
        {
            InitializeComponent();
            BindingContext = new OrderDetailViewModel();
        }
    }
}