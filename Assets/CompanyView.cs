using Assets;
using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyView : MonoBehaviour {
    bool toggle = true;
    SoundManager soundManager;
	
    // Use this for initialization
	void Start () {
        soundManager = new SoundManager();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject actionsPanel = gameObject.transform.GetChild(1).gameObject;
        actionsPanel.SetActive(!toggle);
    }

    public void ToggleActionPanel()
    {
        toggle = !toggle;
        soundManager.PlayToggleSound();
    }

    public void RenderActionPanel(Project project)
    {
        GameObject actionsPanel = gameObject.transform.GetChild(1).gameObject;
        actionsPanel.SetActive(!toggle);
    }

    public void RenderBasePanel(Project project)
    {
        GameObject panel = gameObject.transform.GetChild(0).gameObject;

        GameObject Share = panel.transform.Find("Share").gameObject;
        GameObject CompanyCost = panel.transform.Find("CompanyCost").gameObject;
        GameObject CompanyName = panel.transform.Find("CompanyName").gameObject;

        Button button = panel.transform.Find("Button").gameObject.GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { ToggleActionPanel(); });

        Share.GetComponentInChildren<UIHint>().SetHintObject(GetShareHint(project));
        CompanyName.GetComponent<Text>().text = project.Name;
    }
    
    public void Render(Project project)
    {
        RenderBasePanel(project);

        RenderActionPanel(project);
    }

    private string GetShareHint(Project project)
    {
        return "ProjectId";
    }
}
