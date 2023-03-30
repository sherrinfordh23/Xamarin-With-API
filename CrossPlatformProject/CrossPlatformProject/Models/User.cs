using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Models
{
    public class User
    {
        public string email { get; set; }
        public string name { get; set; }
        public string uid { get; set; }

        public string password { get; set; }

        public User(string email, string name, string uid, string password)
        {
            this.email = email;
            this.name = name;
            this.uid = uid;
            this.password = password;
        }

        public User(string email, string password, string name)
        {
            this.email = email;
            this.name = name;
            this.password = password;
        }

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public User()
        {

        }

    }
}
