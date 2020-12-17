public class AutorecruitWorkers : ProductUpgradeButton
{
    public override string GetButtonTitle() => "Hire max"; //  workers\nautomatically
    public override string GetBenefits() => "";

    public override ProductUpgrade upgrade => ProductUpgrade.AutorecruitWorkers;



    public override void Execute()
    {
        base.Execute();

        //AutoHire(Flagship);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        //AutoHire(Flagship);
    }
}
