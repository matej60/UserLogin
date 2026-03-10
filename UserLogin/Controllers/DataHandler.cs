using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using UserLogin.Models;

namespace UserLogin.Controllers
{
    public class DataHandler
    {   
        public static string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string projectDir = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName;
        private string path = Path.Combine(projectDir, "Data","Users.xml");

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public User Login(string username, string passwd)
        {
           var user = Users.FirstOrDefault(u => u.Name == username);
            if (user == null) {
                return null;
            }
            return user.CheckPasswd(passwd) ? user : null; 
        }

        public void AddNewUser(string name, string passwd)
        {
            if(name == null || name == "" || passwd == null || passwd == "") { return; }
            if (Users.Any(u => u.Name == name))
            {
                MessageBox.Show("Uživatel již existuje");
                return;
            }
            Users.Add(new User(GetLastID()+1, name, passwd));
        }
        public void RemoveUser(User user) 
        { 
            Users.Remove(user);
        }
        public void ResetPass(User user, string passwdHash)
        {
            user.ChangePasswd(passwdHash);
        }

        public int GetLastID()
        {
            if (Users.Count == 0)
                return 0;

            return Users.Max(u => u.Id);
        }

    
        public void Import()
        {
            if (!File.Exists(path))
            {
                Users.Add(new Admin(0, "admin", Controllers.PasswordHelper.Hash("admin")));
                Export();
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>), new[] { typeof(Admin)});
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                Users = (ObservableCollection<User>)serializer.Deserialize(fs);
            }
        }
        public void Export()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<User>), new[] { typeof(Admin) });
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, Users);
            }
        }
    }
}
