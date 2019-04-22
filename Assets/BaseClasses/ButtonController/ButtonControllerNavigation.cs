using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract partial class ButtonController : MonoBehaviour
{
    public void NavigateToNiche(NicheType niche)
    {
        Navigate(ScreenMode.NicheScreen, niche);
    }

    public void NavigateToProjectScreen(int companyId)
    {
        Navigate(ScreenMode.ProjectScreen, companyId);
    }

    public void NavigateToIndustry(IndustryType industry)
    {
        Navigate(ScreenMode.IndustryScreen, industry);
    }

    //public void NavigateToBusinessScreen(int companyId)
    //{
    //    Navigate(ScreenMode.BusinessScreen, companyId);
    //}

    public void Navigate(ScreenMode screenMode, object data)
    {
        MenuUtils.Navigate(GameContext, screenMode, data);
    }

    public void ReNavigate()
    {
        var m = MenuUtils.GetMenu(GameContext);

        m.ReplaceMenu(m.menu.ScreenMode, m.menu.Data);
    }
}
