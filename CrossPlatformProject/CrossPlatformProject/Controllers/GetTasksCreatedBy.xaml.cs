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
    public partial class GetTasksCreatedBy : ContentPage
    {

        
        public GetTasksCreatedBy()
        {
            InitializeComponent();
            GetTasksCreatedByMethod();
        }


        protected override void OnAppearing()
        {
            listOfTasks.ItemsSource = null;
            listOfTasks.SelectedItem = null;
            listOfTasks.ItemsSource = DataSource.listOfCreatedTasks;
            base.OnAppearing();
        }




        private async void GetTasksCreatedByMethod()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);
            Uri uri = new Uri($"{DataSource.baseUrl}tasks/createdby/");
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
                    assignedToName = item["assignedToName"].ToString(),
                    description = item["description"].ToString(),
                    done = (Boolean)item["done"],
                    taskUid = item["taskUid"].ToString()
                }
               );

            }

            listOfTasks.ItemsSource = Tasks;
            DataSource.listOfCreatedTasks = Tasks;
        }

        private void listOfTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                var deletePage = new DeleteTaskPage((TaskModel)e.SelectedItem);
                Navigation.PushAsync(deletePage);
            }

        }

        
    }
}