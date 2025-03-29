using System.ComponentModel.DataAnnotations;

namespace _2_AspPract.Models
{
    public class BookDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }
        public string? Style { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }
        public string? OtherInfo { get; set; }
    }
}
