using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class ResponseDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public required string Content { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        public required DateTime Date { get; set; }

        public int? QuestionId { get; set; }

        public int AdminId { get; set; }
    }

}
