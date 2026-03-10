using System;

namespace UserLogin.Models
{
    [Serializable]
    public class User 
    {
        // Struktura připravená pro implementaci do DB
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }
        // možnost přidat datum posledního přihlášení

        public virtual string Title => Name + " (user)";


        public User() { }

        public User(int id, string name, string passwordHash) 
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            Created = DateTime.Now;
        }

        public bool CheckPasswd(string passwdHash)
        {
            return this.PasswordHash == passwdHash;
        }
        public void ChangePasswd(string passwdHash)
        {
            this.PasswordHash = passwdHash;
        }
    }
}
