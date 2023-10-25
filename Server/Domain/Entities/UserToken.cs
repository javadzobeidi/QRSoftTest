using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

[Table("UserToken", Schema = "dbo")]
public class UserToken : IEntity
{


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid UserTokenId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset TokenDateTime { set; get; }
    public DateTimeOffset AccessTokenExpiresDateTime { set; get; }
   
    public User User { set; get; }

}
