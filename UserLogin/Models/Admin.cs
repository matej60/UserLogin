using System;

namespace UserLogin.Models
{
    [Serializable]
    public class Admin : User
    {

        public override string Title => Name + " (admin)";

        public Admin() { }

        public Admin(int id, string name, string passwordHash) : base(id, name, passwordHash) 
        {
        }
    }
}
