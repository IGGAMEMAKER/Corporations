using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCampaignButton : ButtonController
{
    NicheType NicheType;
    public override void Execute()
    {
        var company = CompanyUtils.GenerateCompanyGroup(GameContext, "Mail mail ru");

        CompanyUtils.PlayAs(company, GameContext);
        CompanyUtils.AutoFillShareholders(GameContext, company, true);

        ScreenUtils.Navigate(GameContext, ScreenMode.NicheScreen, Constants.MENU_SELECTED_NICHE, NicheType);


        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void SetNiche(NicheType nicheType)
    {
        NicheType = nicheType;
    }
}
