using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DBModels
{
    public class Album : IndexModel
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }

        [Required]
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

    }
}
