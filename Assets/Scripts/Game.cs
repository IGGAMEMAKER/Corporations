using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    int money;
    public Text moneyText;

	// Use this for initialization
	void Start () {
        money = 0;
	}
	
	// Update is called once per frame
	void Update () {
        moneyText.text = "" + money;
	}

    public void MakeMoney ()
    {
        money++;
        print("MOAR MONEY!" + money);
    }
}
