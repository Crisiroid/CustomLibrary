using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomLibrary.Models
{
    public class Token
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string deviceId { get; set; }
        [Required]
        public string ipAddress { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public DateTime createdDate { get; set; }
    }
}
