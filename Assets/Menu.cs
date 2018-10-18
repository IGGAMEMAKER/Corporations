using Assets.Classes.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    List<GameObject> Interrupts;
    GameObject DangerInterrupt;
    GameObject Canvas;

    // Use this for initialization
    void Start () {
        DangerInterrupt = GameObject.Find("DangerInterrupt");
        Canvas = GameObject.Find("Canvas");

        Interrupts = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void AddInterrupt(InterruptImportance importance, string prefabName, string linkToMenu)
    {
        float spacing = 50f;
        int x = Interrupts.Count;
        int y = 1;

        Vector3 pos = new Vector3(x, y, 0) * spacing;
        GameObject g = Instantiate(DangerInterrupt, pos, Quaternion.identity);

        g.transform.SetParent(Canvas.transform, false);

        Interrupts.Add(g);
    }
}
