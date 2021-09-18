using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    public class FunctionalCharacteristicByRole : IEntity
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("PublicationDate")]
        public DateTime PublicationDate { get; set; }

        [Column("RoleId")]
        public int RoleId { get; set; }

        [Column("FunctionalCharacteristicId")]
        public int FunctionalCharacteristicId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
        public virtual FunctionalCharacteristic FunctionalCharacteristic { get; set; }
    }
}
