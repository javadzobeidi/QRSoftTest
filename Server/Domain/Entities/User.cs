using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("User", Schema = "dbo")]
public class User: BaseAuditableEntity
{
    public User()
    {
        _userTokens = new List<UserToken>();

    }
    [Key]
    public int UserId { set; get; }
    public string UserName { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Password { set; get; }
    public string PasswordSalt { set; get; }
    public string? Email { set; get; }
    public string? Job { set; get; }
    public int UserTypeId { set; get; }

    public int FailedCount { set; get; }
    public bool UserLocked { set; get; }
    public string GivenName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }
    private readonly List<UserToken> _userTokens;
    public IReadOnlyCollection<UserToken> UserTokens => _userTokens;

    public UserToken AddUserToken()
    {
        UserToken us = new UserToken
        {
            UserTokenId = Guid.NewGuid(),
            TokenDateTime = DateTimeOffset.UtcNow,
            AccessTokenExpiresDateTime = DateTimeOffset.UtcNow.AddMonths(3)
        };
        _userTokens.Add(us);
        return us;
    }
}
