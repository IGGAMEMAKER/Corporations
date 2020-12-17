public class ConceptToggleButton : ProductUpgradeButton
{
    public override string GetButtonTitle() => $"Upgrade concept";
    public override string GetBenefits() => "";

    public override ProductUpgrade upgrade => ProductUpgrade.SimpleConcept;
}
