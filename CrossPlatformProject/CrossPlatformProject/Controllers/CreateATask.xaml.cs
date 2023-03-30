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
    public partial class CreateATask : ContentPage
    {
        public CreateATask()
        {
            InitializeComponent();
            PopulatePickerUser();
        }


        private async void PopulatePickerUser()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);
            Uri uri = new Uri($"{DataSource.baseUrl}users/all");
            HttpResponseMessage response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();

            List<User> Users = new List<User>();
            var values = JObject.Parse(content);

            foreach (var item in values["allUsers"])
            {

                if (item["email"].ToString() == DataSource.loggedInUser.email)
                    continue;

                Users.Add(new User
                {
                    email = item["email"].ToString(),
                    name = item["name"].ToString(),
                    uid = item["uid"].ToString()
                });
            }

            pickerUser.ItemsSource = Users;
            pickerUser.SelectedIndex = 0;

        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {
            txtTaskDescription.Text = "";
            pickerUser.SelectedIndex = 0;
        }

        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            bool emptyFields = txtTaskDescription.Text == "";
            if (emptyFields)
            {
                await DisplayAlert("Alert", "Fields Cannot be Empty!", "OK");
                return;
            }


            CreateATaskRequest createATaskRequest = new CreateATaskRequest(txtTaskDescription.Text, ((User)pickerUser.SelectedItem).uid);
            StringContent jsonContent = new StringContent(
                JsonConvert.SerializeObject(createATaskRequest),
                Encoding.UTF8,
                "application/json"
            );

            Uri uri = new Uri($"{DataSource.baseUrl}tasks/");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);
            HttpResponseMessage response = await client.PostAsync(uri, jsonContent);


            string content = await response.Content.ReadAsStringAsync();
            var values = JObject.Parse(content);
            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }


            txtTaskDescription.Text = "";
            pickerUser.SelectedIndex = 0;

        }
    }
}