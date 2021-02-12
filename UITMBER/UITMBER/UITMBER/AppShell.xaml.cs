using System;
using System.Collections.Generic;
using UITMBER.ViewModels;
using UITMBER.Views;
using Xamarin.Forms;

namespace UITMBER
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(OrderDetailPage), typeof(OrderDetailPage));
        }

    }
}
