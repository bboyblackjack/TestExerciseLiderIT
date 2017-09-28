using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository
    {

        DataContext db = new DataContext();

        public User getUserById(int id)
        {
            return db.Users.Find(id);
        }

        public void insertUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public List<Message> getAllMessages()
        {
            return db.Set<Message>().ToList();
        }

        public void insertMessage(Message msg)
        {
            db.Messages.Add(msg);
        }
    }
}
