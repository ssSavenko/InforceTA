using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DBModels
{
    public class User : IndexModel
    {

        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public bool isAdmin { get; set; }
    }
}
