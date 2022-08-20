using System.ComponentModel.DataAnnotations;

namespace gravimetry_api.Classes
{
    public class IncidentNoteInputModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public bool IsPublic { get; set; }
    }
}
