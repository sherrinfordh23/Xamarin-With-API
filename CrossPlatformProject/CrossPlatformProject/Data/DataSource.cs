using CrossPlatformProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Data
{
    public class DataSource
    {
        public static string accessToken;
        public static string baseUrl = "https://taskmanager-project-fall2022-zmoya.ondigitalocean.app/v1/";
        public static User loggedInUser;

        public static List<TaskModel> listOfAssignedTasks;
        public static List<TaskModel> listOfCreatedTasks;
    }
}
