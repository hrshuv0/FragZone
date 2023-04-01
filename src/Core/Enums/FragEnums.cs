using System.ComponentModel;

namespace Core.Enums;

public enum Genre
{
    [Description("First Person")]
    FirstPerson = 1,
    
    [Description("Battle Ground")]
    BattleGround,
    
    [Description("Racing")]
    Racing,
    
    [Description("Shooter")]
    Shooter,
    
    [Description("Action")]
    Action
}

public enum Mode
{
    [Description("Multi Player")]
    MultiPlayer = 1,
    
    [Description("Solo")]
    Solo
}