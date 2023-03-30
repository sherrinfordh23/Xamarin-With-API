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
    public partial class DeleteTaskPage : ContentPage
    {
        TaskModel clickedTask;
        public DeleteTaskPage(TaskModel tsk)
        {
            InitializeComponent();
            clickedTask = tsk;
            FillTaskDetails();
        }

        private void FillTaskDetails()
        {
            lblAssignedToName.Text = "Assinged to: " + clickedTask.assignedToName;
            lblDescription.Text = "Task description: " + clickedTask.description;
            lblDone.Text = "Status: " + clickedTask.done.ToString();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-access-token", DataSource.accessToken);

            Uri uri = new Uri($"{DataSource.baseUrl}tasks/{clickedTask.taskUid}");
            HttpResponseMessage response = await client.DeleteAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            var values = JObject.Parse(content);

            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert", values["error"].ToString(), "Ok");
                return;
            }

            DataSource.listOfCreatedTasks.Remove(clickedTask);

            await Navigation.PopAsync();

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}