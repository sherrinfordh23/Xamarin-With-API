using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Models
{
    public class LoginRequest : User
    {
        public LoginRequest(string email, string password) :
            base(email, password)
        {
        }

    }
}
