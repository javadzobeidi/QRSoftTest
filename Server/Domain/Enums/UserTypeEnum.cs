using System.ComponentModel;

namespace Domain.Enums;

public enum UserTypeEnum
{
    [Description("admin")]
    Admin = 1,
    [Description("manager")]
    Manager = 2,
    [Description("customer")]
    Customer =3,
}
