using System.Runtime.Serialization;

namespace Works.Application.Models.Enums;

public enum CountryCodes
{
    [EnumMember(Value = "DE")] Germany = 1,

    [EnumMember(Value = "PL")] Poland = 2,

    [EnumMember(Value = "US")] UnitedStates = 3

    //create more...
}