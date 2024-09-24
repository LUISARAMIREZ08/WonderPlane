using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.models;

public class Message
{   
    [Key]
    public long Id { get; set; }

    [StringLength(255)]
    public required string Content { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
    public required DateTime Date { get; set; }

    public Forum Forum { get; set; }

}