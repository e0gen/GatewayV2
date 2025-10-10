using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LoginRole", Schema = "List")]
public partial class LoginRole
{
    [Key]
    [Column("LoginRole_id")]
    public byte LoginRoleId { get; set; }

    [Column("ParentID")]
    public byte? ParentId { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }

    public bool IsLoginWithEmail { get; set; }

    public bool IsLoginWithUsername { get; set; }

    [InverseProperty("LoginRole")]
    public virtual ICollection<LoginAccount> LoginAccounts { get; set; } = new List<LoginAccount>();
}
