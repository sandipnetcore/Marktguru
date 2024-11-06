namespace Marktguru.BusinessLogic.Users
{
    /// <summary>
    /// User 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id from the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Roles Defined for the User. 
        /// User can have multiple users
        /// </summary>
        public List<string> UserRoles { get; set; }


        /// <summary>
        /// This is dummy UserList. In realtime we can directly query it from the Database.
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

    /// <summary>
    /// We will use this while creating/updating/ deleting the product.
    /// We are using authorization, but for more convinience I created the roles.
    /// Though roles were not asked
    /// </summary>
    public static class Roles
    {
        /// <summary>
        /// Performs only admin roles.
        /// </summary>
        public const string AdminRole = "Admin";


        /// <summary>
        /// Performs only the User roles
        /// </summary>
        public const string UserRole = "User";
    }
}
