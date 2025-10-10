using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SystemUserType", Schema = "List")]
public partial class SystemUserType
{
    [Key]
    [Column("UserType_id")]
    public byte UserTypeId { get; set; }

    [StringLength(25)]
    public string? Name { get; set; }
}
