public class TestSystems : Feature
{
    public TestSystems(Contexts contexts) : base("Test Systems")
    {
        Add(new TestRunnerSystem(contexts));
    }
}
