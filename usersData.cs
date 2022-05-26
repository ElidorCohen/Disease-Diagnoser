namespace WindowsFormsApp1
{
    public class usersData
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private int userscount = 4;

        public usersData()
        {
            Username = "defaultUsername";
            Password = "defPass";
            
        }

        public usersData(string username, string password)
        {
            Username = username;
            Password = password;
            
        }
        public string getUsername()
        {
            return Username;
        }
        public string getPassword()
        {
            return Password;
        }
        public int getUserCount()
        {
            return userscount;
        }
        

    }
}
