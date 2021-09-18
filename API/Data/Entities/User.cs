using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data.Entities
{
    public class User : IEntity
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("UserName")]
        public string UserName { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("FullName")]
        public string FullName { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Age")]
        public int Age { get; set; }
        [Column("RoleId")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
