using Assets;
using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyView : MonoBehaviour {
    SoundManager soundManager;
	
    // Use this for initialization
	void Start () {
        soundManager = new SoundManager();
	}
	
    public struct CompanyInfo
    {
        public long Cost;
        public List<ShareInfo> Shareholders;
    }

    void RenderSellShareButton(GameObject panel, int myCompanyId, int thisCompanyId)
    {
        GameObject SellButton = panel.transform.Find("SellShare").gameObject;
        Button sell = SellButton.GetComponent<Button>();

        sell.onClick.RemoveAllListeners();

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["sellerId"] = myCompanyId;
        parameters["buyerId"] = Balance.BASE_INVESTOR_ID;
        parameters["projectId"] = thisCompanyId;
        parameters["share"] = 1;
        sell.onClick.AddListener(delegate { BaseController.SendCommand(Commands.SHARES_SELL, parameters); });
    }

    void RenderBuyShareButton(GameObject panel, int myCompanyId, int thisCompanyId)
    {
        GameObject BuyButton = panel.transform.Find("BuyShare").gameObject;
        Button buy = BuyButton.GetComponent<Button>();

        buy.onClick.RemoveAllListeners();

        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters["sellerId"] = Balance.BASE_INVESTOR_ID;
        parameters["buyerId"] = myCompanyId;
        parameters["projectId"] = thisCompanyId;
        parameters["share"] = 1;
        buy.onClick.AddListener(delegate { BaseController.SendCommand(Commands.SHARES_BUY, parameters); });
    }

    void RenderShareButtons(GameObject panel, int myCompanyId, int thisCompanyId)
    {
        RenderSellShareButton(panel, myCompanyId, thisCompanyId);
        RenderBuyShareButton(panel, myCompanyId, thisCompanyId);
    }

    public void RenderBasePanel(Project project, int myCompanyId, int thisCompanyId)
    {
        GameObject panel = gameObject.transform.GetChild(0).gameObject;

        GameObject Share = panel.transform.Find("Share").gameObject;
        GameObject CompanyCost = panel.transform.Find("CompanyCost").gameObject;
        GameObject CompanyName = panel.transform.Find("CompanyName").gameObject;

        GameObject Steal = panel.transform.Find("Steal").gameObject;
        GameObject Coaching = panel.transform.Find("Coaching").gameObject;

        Button StealButton = Steal.GetComponent<Button>();
        Button CoachingButton = Coaching.GetComponent<Button>();

        StealButton.onClick.RemoveAllListeners();
        CoachingButton.onClick.RemoveAllListeners();

        RenderShareButtons(panel, myCompanyId, thisCompanyId);

        Share.GetComponentInChildren<UIHint>().SetHintObject(GetShareHint(project));
        CompanyName.GetComponent<Text>().text = project.Name;
    }
    
    public void Render(Project project, int myCompanyId)
    {
        RenderBasePanel(project, myCompanyId, project.Id);
    }

    private string GetShareHint(Project project)
    {
        return "ProjectId";
    }
}
