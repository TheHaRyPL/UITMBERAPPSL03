using System;
using System.Collections.Generic;
using System.Text;
using UITMBER.Models.Register;
using UITMBER.Services.Register;
using UITMBER.Views;
using Xamarin.Forms;

namespace UITMBER.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string email;
        private string firsname;
        private string lastname;
        private string password;
        private string phonenumber;
        private string photo;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string FirstName
        {
            get => firsname;
            set => SetProperty(ref firsname, value);
        }
        public string LastName
        {
            get => lastname;
            set => SetProperty(ref lastname, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string PhoneNumber
        {
            get => phonenumber;
            set => SetProperty(ref phonenumber, value);
        }
        public string Photo
        {
            get => photo;
            set => SetProperty(ref photo, value);
        }
        RegisterService registerService => new RegisterService();

        public Command RegisterCommand { get; }
        public Command LoginPageCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
            LoginPageCommand = new Command(OnLoginClicked);
        }


        private async void OnRegisterClicked(object obj)
        {
            if (IsBusy)
                return;


            IsBusy = true;

            try
            {
                await registerService.Register(new RegisterRequest
            {
                Email = email,
                FirstName = firsname,
                LastName = lastname,
                Password = password,
                PhoneNumber = phonenumber,
                Photo = photo
            });
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {

                //Error handlig
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
