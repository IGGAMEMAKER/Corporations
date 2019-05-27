using Entitas;

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

}
