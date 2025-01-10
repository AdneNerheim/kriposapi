using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SecurityWorkshop.Models;

[Table("top_secret")]
public class TopSecret
{
    [Key]
    [JsonIgnore]
    public Guid id { get; set; }
    [Column("code_name")]
    public string codeName { get; set; }
    [Column("real_name")]
    public string realName { get; set; }
}