using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Shared
{
    public class QuestionDto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public required string Content { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        public required DateTime Date { get; set; }
        public string? Theme { get; set; }

        public int UserId { get; set; }
      
        public StateDto StateQuestion { get; set; }
        public List<ResponseDto>? Responses { get; set; }

        public enum StateDto
        {
            Pendiente,
            Respondida
        }
    }



}
