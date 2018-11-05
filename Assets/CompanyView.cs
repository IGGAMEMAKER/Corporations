using Assets.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    
    public void Render(Project project)
    {
        GameObject panel = gameObject.transform.GetChild(0).gameObject;

        GameObject Share = panel.transform.Find("Share").gameObject;
        GameObject CompanyCost = panel.transform.Find("CompanyCost").gameObject;
        GameObject CompanyName = panel.transform.Find("CompanyName").gameObject;

        Share.GetComponentInChildren<UIHint>().SetHintObject(GetShareHint(project));
        CompanyName.GetComponent<Text>().text = project.Name;
    }

    private string GetShareHint(Project project)
    {
        return "ProjectId";
    }
}
