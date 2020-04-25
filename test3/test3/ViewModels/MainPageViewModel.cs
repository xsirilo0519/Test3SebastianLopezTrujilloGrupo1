using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test3.Models;
using test3.Service;

namespace test3.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private UserRequest _user;
        private Response _problem;
        private bool _isEnabled;
        private string _solution;
        private DelegateCommand _registerCommand;
        private DelegateCommand _sendCommand;
        public MainPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            IsEnabled = true;
            Title = "Grupo 1,Sebastian López";
            User = new UserRequest();
            Problem = new Response();
            
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));
        public DelegateCommand SendCommand => _sendCommand ?? (_sendCommand = new DelegateCommand(SendAsync));

        private async void SendAsync()
        {
            bool isValid = await ValidateData2Async();
            if (!isValid)
            {
                return;
            }
            await App.Current.MainPage.DisplayAlert("Congratulation!", User.username, "Accept");
            await App.Current.MainPage.DisplayAlert("Congratulation!", Solution, "Accept");

            SolutionRequest solutionrequest = new SolutionRequest
            {
               username=User.username,
               solution=Solution
            };
            
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool status = await _apiService.PutAsync(url, "/Account", "/SendSolution", solutionrequest, "bearer",Problem.token);

            if (status)
            {
                await App.Current.MainPage.DisplayAlert("Congratulation!", "The answer was registered successfully!", "Accept");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "The answer was not registered successfully", "Accept");
            }
        }

        private async void RegisterAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

        
            IsEnabled = false;

            UserRequest userRequest = new UserRequest
            {
                document = User.document,
                username = User.username,
                firstName = User.firstName,
                lastName = User.lastName,
                phone = User.phone,
                group = User.group
            };
            string url = App.Current.Resources["UrlAPI"].ToString();
             Problem= await _apiService.GetProblemAsync(url, "/Account", "/GetProblem", userRequest);

            if (string.IsNullOrEmpty(Problem.token))
            {
                await App.Current.MainPage.DisplayAlert("Error","No pudo encontrar su problema", "Accept");
            }
        }
        public string Solution
        {
            get => _solution;
            set => SetProperty(ref _solution, value);
        }

        public Response Problem
        {
            get => _problem;
            set => SetProperty(ref _problem, value);
        }

        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async Task<bool> ValidateData2Async()
        {

            if (string.IsNullOrEmpty(User.username))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Email", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(Solution))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter the solution", "Accept");
                return false;
            }


            return true;
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(User.document))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Document", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(User.firstName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a First Name", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(User.lastName))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Last Name", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(User.username) )
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Email", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(User.phone))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Phone", "Accept");
                return false;
            }

            if (User.group==0)
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must select a Group", "Accept");
                return false;
            }


            return true;
        }
    }
}
