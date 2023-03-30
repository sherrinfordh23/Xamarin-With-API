using CrossPlatformProject.Data;
using CrossPlatformProject.Models;
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
    public partial class GetAllUsersPage : ContentPage
    {
        public GetAllUsersPage()
        {
            InitializeComponent();
            GetAllUsers();
        }

        private async void GetAllUsers()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);
            Uri uri = new Uri($"{DataSource.baseUrl}users/all");
            HttpResponseMessage response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();

            List<User> Users = new List<User>();
            var values = JObject.Parse(content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }


            foreach (var item in values["allUsers"])
            {
                Users.Add(new User
                {
                    email = item["email"].ToString(),
                    name = item["name"].ToString(),
                    uid = item["uid"].ToString()
                });
            }

            listOfUsers.ItemsSource = Users;

        }

        private void listOfUsers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}