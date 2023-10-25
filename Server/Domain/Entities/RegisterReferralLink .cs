using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("RegisterReferralLink ", Schema = "dbo")]
public class RegisterReferralLink : IEntity
{
    [Key]
    public Guid Id { set; get; }
    public string InvCode { set; get; }
    public DateTimeOffset Expire{ set; get; }
    public int? UserId { set; get; }

    public User User { set; get; }
}
