using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonManagementSystem.RepositoryServices.EntityFramework
{
    public class Notification
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
    }
}
