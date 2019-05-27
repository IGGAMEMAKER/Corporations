using Entitas;
using System.Collections.Generic;

[Game]
public class HumanComponent : IComponent
{
    public int Id;
    public string Name;
    public string Surname;

    // skillset, character and perks later
}

[Game]
public class HumanSkillsComponent : IComponent
{
    //public Dictionary<>
}
