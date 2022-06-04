using System.ComponentModel.DataAnnotations;

namespace gravimetry_api.Classes
{
    public class LoginInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
