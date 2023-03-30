using CrossPlatformProject.Data;
using CrossPlatformProject.Menu;
using CrossPlatformProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformProject.Controllers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {


            bool emptyFields = txtEmail.Text == "" ||
                   txtPassword.Text == "";

            if (emptyFields)
            {
                await DisplayAlert("Alert", "Fields Cannot be empty!", "Ok");
                return;
            }




            LoginRequest loginRequest = new LoginRequest(txtEmail.Text, txtPassword.Text);
            StringContent jsonContent = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient client = new HttpClient();
            Uri uri = new Uri($"{DataSource.baseUrl}users/login");
            HttpResponseMessage response = await client.PostAsync(uri, jsonContent);

            Console.WriteLine();

            string content = await response.Content.ReadAsStringAsync();
            var values = JObject.Parse(content);
            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }

            DataSource.accessToken = values["token"].ToString();
            DataSource.loggedInUser = new User();
            DataSource.loggedInUser.email = txtEmail.Text;

            await Navigation.PushAsync(new Master());


        }

        private async void btnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }
}