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
	
	// Update is called once per frame
	void Update () {
    }

    public struct CompanyInfo
    {
        public long Cost;
        public List<ShareInfo> Shareholders;
    }

    void RenderShareButtons(GameObject panel)
    {
        GameObject SellButton = panel.transform.Find("SellShare").gameObject;
        GameObject BuyButton = panel.transform.Find("BuyShare").gameObject;

        Button sell = SellButton.GetComponent<Button>();
        Button buy = BuyButton.GetComponent<Button>();

        sell.onClick.RemoveAllListeners();
        buy.onClick.RemoveAllListeners();


    }

    public void RenderBasePanel(Project project)
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

        Share.GetComponentInChildren<UIHint>().SetHintObject(GetShareHint(project));
        CompanyName.GetComponent<Text>().text = project.Name;
    }
    
    public void Render(Project project)
    {
        RenderBasePanel(project);
    }

    private string GetShareHint(Project project)
    {
        return "ProjectId";
    }
}
