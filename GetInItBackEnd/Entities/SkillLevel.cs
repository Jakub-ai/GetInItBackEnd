using System.Net;
using System.Runtime.Serialization;

namespace GetInItBackEnd.Entities;

public enum SkillLevel
{
    [EnumMember(Value = "Beginner")]
    Beginner = 1,
    [EnumMember(Value = "Basic")]
    Basic = 2,
    [EnumMember(Value = "Intermediate")]
    Intermediate = 3,
    [EnumMember(Value = "Advanced")]
    Advanced = 4,
    [EnumMember(Value = "Expert")]
    Expert = 5
}