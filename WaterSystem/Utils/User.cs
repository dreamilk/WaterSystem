using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterSystem.Utils
{
     public class User
    {
        private String name;
        private String password;
        private int permission;

        public User(String name,String password,int permission)
        {
            this.name = name;
            this.password = password;
            this.permission = permission;
        }

        public String getName()
        {
            return name;
        }

        public int getPermission()
        {
            return permission;
        }

        public string getPassword()
        {
            return password;
        }
    }
}
