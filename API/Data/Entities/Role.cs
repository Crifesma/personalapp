using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data.Entities
{
    public class Role : IEntity
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<FunctionalCharacteristic> FunctionalCharacteristics { get; set; }

        public virtual ICollection<FunctionalCharacteristicByRole> FunctionalCharacteristicsByRole { get; set; }
    }
}
