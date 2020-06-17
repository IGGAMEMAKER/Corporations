public abstract class CompanyUpgradeButton : UpgradedButtonController
{
    public abstract string GetButtonTitle();
    public abstract string GetBenefits();
    public override bool IsInteractable() => true;

    public abstract bool GetState();

    public abstract string GetHint();

    // TODO code was: GameEntity Company => GetFollowableCompany();
    // to get Flagship company

    GameEntity Company => Flagship;

    public override void ViewRender()
    {
        base.ViewRender();

        var links = GetComponent<ProductUpgradeLinks>();

        if (links == null)
            return;

        var state = GetState();

        // checkbox text
        links.Title.text = $"<b>{GetButtonTitle()}</b>\n{GetBenefits()}";

        links.Hint.SetHint("");
        links.CanvasGroup.alpha = state ? 1f : 0.25f;
    }
}
