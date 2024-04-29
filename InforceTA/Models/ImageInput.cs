using System.ComponentModel.DataAnnotations;

namespace InforceTA.Models
{
    public class ImageInput
    {
        public string Base64Code { get; set; } 
        public int AlbumId { get; set; }
    }
}
