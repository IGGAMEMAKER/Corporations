public class AIGroupSystems : Feature
{
    public AIGroupSystems(Contexts contexts) : base("AI Group Systems") {
        Add(new AIManageGroupSystems(contexts));
        Add(new CheckBankruptciesSystems(contexts));
    }
}