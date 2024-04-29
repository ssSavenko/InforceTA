﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DBModels
{
    public class Dislike : IndexModel
    {
        [Required]
        public int ImageId { get; set; }
        [ForeignKey(nameof(ImageId))]
        public Image Image { get; set; } 

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
