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
    public partial class GetTasksAssignedTo : ContentPage
    {
        public GetTasksAssignedTo()
        {
            InitializeComponent();
            GetTasksAssignedToMethod();
        }

        protected override void OnAppearing()
        {            
            listOfTasks.ItemsSource = null;
            listOfTasks.SelectedItem = null;
            listOfTasks.ItemsSource = DataSource.listOfAssignedTasks;
            base.OnAppearing();
        }



        private async void GetTasksAssignedToMethod()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);
            Uri uri = new Uri($"{DataSource.baseUrl}tasks/assignedto/");
            HttpResponseMessage response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();

            List<TaskModel> Tasks = new List<TaskModel>();
            var values = JObject.Parse(content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }


            foreach (var item in values["allTasks"])
            {
                Tasks.Add(new TaskModel
                    {
                        createdByName = item["createdByName"].ToString(),
                        description = item["description"].ToString(),
                        done = (Boolean) item["done"],
                        taskUid = item["taskUid"].ToString()
                }
                );
            }

            listOfTasks.ItemsSource = Tasks;
            DataSource.listOfAssignedTasks = Tasks;

        }

        private void listOfTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(((ListView)sender).SelectedItem != null)
            {
                var updatePage = new UpdateATaskPage((TaskModel)e.SelectedItem);
                Navigation.PushAsync(updatePage);
            }


        }
    }
}