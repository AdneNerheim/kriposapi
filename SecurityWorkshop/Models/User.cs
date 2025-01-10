using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace SecurityWorkshop.Models;

[Table("users")]
public class User
{
    [Key]
    [JsonIgnore]
    public Guid id { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    [JsonIgnore]
    [Column("reset_pin")]
    public int resetPin { get; set; }
}