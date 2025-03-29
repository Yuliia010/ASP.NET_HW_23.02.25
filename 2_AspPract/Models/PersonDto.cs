using System.ComponentModel.DataAnnotations;

namespace _2_AspPract.Models
{
    public class PersonDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string? OtherInfo { get; set; }
    }
}
