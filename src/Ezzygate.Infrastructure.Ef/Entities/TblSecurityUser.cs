using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityUser")]
[Index("SuUsername", Name = "IX_tblSecurityUser_Username", IsUnique = true)]
public partial class TblSecurityUser
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("su_Username")]
    [StringLength(50)]
    public string SuUsername { get; set; } = null!;

    [Column("su_Name")]
    [StringLength(50)]
    public string SuName { get; set; } = null!;

    [Column("su_IsAdmin")]
    public bool SuIsAdmin { get; set; }

    [Column("su_IsActive")]
    public bool SuIsActive { get; set; }

    [Column("su_Mail")]
    [StringLength(255)]
    public string SuMail { get; set; } = null!;

    [Column("su_SMS")]
    [StringLength(10)]
    [Unicode(false)]
    public string SuSms { get; set; } = null!;

    [Column("su_MailEmergency")]
    [StringLength(255)]
    public string SuMailEmergency { get; set; } = null!;

    [Column("su_IsLogged")]
    public bool SuIsLogged { get; set; }

    [InverseProperty("SdgUser")]
    public virtual ICollection<TblSecurityDocumentGroup> TblSecurityDocumentGroups { get; set; } = new List<TblSecurityDocumentGroup>();

    [InverseProperty("SlUserNavigation")]
    public virtual ICollection<TblSecurityLog> TblSecurityLogs { get; set; } = new List<TblSecurityLog>();

    [InverseProperty("SudcUserNavigation")]
    public virtual ICollection<TblSecurityUserDebitCompany> TblSecurityUserDebitCompanies { get; set; } = new List<TblSecurityUserDebitCompany>();

    [InverseProperty("SugUserNavigation")]
    public virtual ICollection<TblSecurityUserGroup> TblSecurityUserGroups { get; set; } = new List<TblSecurityUserGroup>();

    [InverseProperty("SumUserNavigation")]
    public virtual ICollection<TblSecurityUserMerchant> TblSecurityUserMerchants { get; set; } = new List<TblSecurityUserMerchant>();
}
