using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblExternalCardCustomerPayment")]
public partial class TblExternalCardCustomerPayment
{
    [Key]
    [Column("ExternalCardCustomerPayment_id")]
    public int ExternalCardCustomerPaymentId { get; set; }

    [Column("ExternalCardCustomer_id")]
    public int ExternalCardCustomerId { get; set; }

    [Column("UniqueID")]
    public Guid UniqueId { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(10)]
    public string? Result { get; set; }

    [StringLength(255)]
    public string? ResultDescription { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime InsertDate { get; set; }

    [ForeignKey("ExternalCardCustomerId")]
    [InverseProperty("TblExternalCardCustomerPayments")]
    public virtual TblExternalCardCustomer ExternalCardCustomer { get; set; } = null!;
}
