using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    public class FunctionalCharacteristic : IEntity
    {
        [Column("Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public ICollection<Role> Roles { get; set; }
        public virtual ICollection<FunctionalCharacteristicByRole> FunctionalCharacteristicsByRole { get; set; }
    }
}
