using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.UserFavouriteLocation;
using UITMBER.Services.UserFavouriteLocation;
using Xamarin.Forms;

namespace UITMBER.ViewModels
{
    class UserFavouriteLocationsViewModel : BaseViewModel
    {
        public IUserFavouriteLocationService _ufavLocationService => DependencyService.Get<IUserFavouriteLocationService>();

        private LocationRequest _selectedItem;

        public ObservableCollection<LocationRequest> Locations { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<LocationRequest> ItemTapped { get; }

        public UserFavouriteLocationsViewModel()
        {
            Title = "Favourite Locations";
            Locations = new ObservableCollection<LocationRequest>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<LocationRequest>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Locations.Clear();
                var locations = await _ufavLocationService.GetMyLocationsAsync();
                var items = locations.Select(x => x.ToLocationRequest());

                foreach (var item in items)
                {
                    Locations.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public LocationRequest SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {   
        }

        async void OnItemSelected(LocationRequest item)
        {
            if (item == null)
                return;

        }
    }
}
