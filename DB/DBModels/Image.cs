using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DBModels
{
    public class Image : IndexModel
    {
        public string BlobReference { get; set; } 
        public string URL { get; set; }

        [Required]
        public int AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public Album Album { get; set; }
    }
}
