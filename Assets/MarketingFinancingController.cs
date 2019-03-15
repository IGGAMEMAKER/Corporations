using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketingFinancingController : MonoBehaviour
{
    Dropdown Dropdown;
    List<String> Options;

    // Start is called before the first frame update
    void Start()
    {
        Dropdown = GetComponent<Dropdown>();

        Options = new List<string>();

        Options.Add("None");
        Options.Add("Reinvest Profit");
        Options.Add("Reinvest Profit");
        Options.Add("Reinvest all");

        UpdateList();
    }

    void UpdateList()
    {
        //gameEntities = GetProperList();

        Dropdown.ClearOptions();
        Dropdown.AddOptions(Options);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateList();
    }
}
