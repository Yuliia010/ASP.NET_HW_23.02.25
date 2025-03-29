using System.ComponentModel.DataAnnotations;

namespace _2_AspPract.Models
{
    public class BookDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author_name { get; set; }
        public string? Style { get; set; }
        [DataType(DataType.Date)]
        public DateTime? First_publish_year { get; set; }
        public string? OtherInfo { get; set; }
    }
}
