using Assets.Classes.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    List<GameObject> Interrupts;

    public GameObject DangerInterrupt;
    public GameObject InterruptPanel;

    // Use this for initialization
    void Start () {
        Interrupts = new List<GameObject>();
    }
	
    public void AddInterrupt(InterruptImportance importance, string prefabName, string linkToMenu)
    {
        float spacing = 50f;
        int x = Interrupts.Count;
        int y = 1;

        Vector3 pos = new Vector3(x, y, 0) * spacing;
        GameObject g = Instantiate(DangerInterrupt, pos, Quaternion.identity);

        g.transform.SetParent(InterruptPanel.transform, false);

        Interrupts.Add(g);
    }
}
