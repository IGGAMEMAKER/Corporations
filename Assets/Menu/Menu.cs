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
}
