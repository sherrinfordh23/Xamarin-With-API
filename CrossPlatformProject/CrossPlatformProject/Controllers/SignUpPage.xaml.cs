using CrossPlatformProject.Data;
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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {

            bool emptyFields = txtEmail.Text == "" ||
                   txtPassword.Text == "" ||
                   txtPasswordRep.Text == "";
            bool passwordsMatch = txtPassword.Text == txtPasswordRep.Text;

            if (emptyFields)
            {
                await DisplayAlert("Alert", "Fields Cannot be empty!", "Ok");
                return;
            }

            if (!passwordsMatch)
            {
                await DisplayAlert("Alert", "Passwords do not match!", "Ok");
                return;
            }



            SignUpRequest loginRequest = new SignUpRequest(txtEmail.Text, txtPassword.Text, txtName.Text);
            StringContent jsonContent = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json"
            );

            HttpClient client = new HttpClient();
            Uri uri = new Uri($"{DataSource.baseUrl}users/signup");
            HttpResponseMessage response = await client.PostAsync(uri, jsonContent);

            Console.WriteLine();

            string content = await response.Content.ReadAsStringAsync();
            var values = JObject.Parse(content);
            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }

            await Navigation.PopAsync();
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}