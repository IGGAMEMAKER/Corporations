using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateView(Human human, int index)
    {
        GameObject NameObject = gameObject.transform.Find("Name").gameObject;
        NameObject.GetComponent<Text>().text = human.FullName;

        GameObject MoraleBar = gameObject.transform.Find("ProgressBar").gameObject;
        MoraleBar.GetComponent<ProgressBar>().SetValue(human.Morale);


    }
}
