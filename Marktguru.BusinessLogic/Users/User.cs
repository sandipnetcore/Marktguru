using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marktguru.BusinessLogic.Users
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> UserRoles { get; set; }


        /// <summary>
        /// This is dummy UserList. In realtime we can directly query it from the Database
        /// </summary>
        internal static List<User> AllUsers
        {
            get
            {
                return new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        UserName = "User1",
                        Password = "User1",
                        UserRoles = new List<string>(){Roles.AdminRole, Roles.UserRole}
                    },
                    new User()
                    {
                        Id = 2,
                        UserName = "User2",
                        Password = "User2",
                        UserRoles = new List<string>(){Roles.AdminRole}
                    },
                    new User()
                    {
                        Id = 3,
                        UserName = "User3",
                        Password = "User3",
                        UserRoles = new List<string>(){Roles.UserRole}
                    },
                };
            }
        }
    }

    public static class Roles
    {
        public const string AdminRole = "Admin";

        public const string UserRole = "User";
    }
}
