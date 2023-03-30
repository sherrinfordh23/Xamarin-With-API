using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatformProject.Models
{
    public class SignUpRequest : User
    {
        public SignUpRequest(string email, string password, string name) : 
            base(email, password, name)
        {

        }
    }
}
