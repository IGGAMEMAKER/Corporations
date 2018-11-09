using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerAvatarView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    Sprite GetSpecialisationSprite(WorkerSpecialisation specialisation)
    {
        switch (specialisation)
        {
            case WorkerSpecialisation.Manager:
                return Resources.Load<Sprite>("collaboration");

            case WorkerSpecialisation.Marketer:
                return Resources.Load<Sprite>("promotion");

            default:
                return Resources.Load<Sprite>("Coding");

        }
    }

    public void SetAvatar (int level, WorkerSpecialisation specialisation)
    {
        gameObject.GetComponentInChildren<Text>().text = level.ToString();

        gameObject.transform.Find("Specialisation").gameObject.GetComponent<Image>().sprite = GetSpecialisationSprite(specialisation);
    }
}
