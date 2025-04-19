using System.ComponentModel.DataAnnotations;

namespace GestionProductos.Models
{
    public class User: BaseEntity
    {
        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
