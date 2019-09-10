using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CreateCompanyController : View
{
    public GameObject StartupButton;

    void OnEnable()
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, SelectedNiche);
        var startCapital = NicheUtils.GetStartCapital(niche);

        if (MyGroupEntity != null)
        {
            SetActions(true, $"Create a startup and attach it to group {MyGroupEntity.company.Name}\n\n " +
                $"This will cost {Format.Money(startCapital)}");
        }
        else if (MyProductEntity == null)
        {
            SetActions(true, "Create a startup and become it's CEO!");
        }
        else
        {
            SetActions(false, "You have one startup already!\n" +
                "Promote it to group of companies or sell it if you want to start new project");
        }
    }

    void SetActions(bool interactible, string hint)
    {
        StartupButton.GetComponent<Button>().interactable = interactible;
        StartupButton.GetComponent<Hint>().SetHint(hint);
    }
}
