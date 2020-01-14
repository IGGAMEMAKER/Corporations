using Assets.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWorkerRoleThatYouWantToHire : View
{
    public Dropdown Dropdown;
    public SpawnEmplyeesByWorkerRole SpawnEmployees;


    void Start()
    {
        Dropdown.onValueChanged.AddListener(SetRole);
    }

    void OnEnable()
    {
        var company = SelectedCompany;
        bool isControlled = company.isControlledByPlayer;

        Dropdown.gameObject.SetActive(isControlled);

        // set roles
        var options = new List<Dropdown.OptionData>();

        var roles = Teams.GetAvailableRoles(company);

        foreach (var role in roles)
            options.Add(new Dropdown.OptionData(role.ToString()));

        Dropdown.ClearOptions();
        Dropdown.AddOptions(options);

    }

    private void SetRole(int arg0)
    {
        var company = SelectedCompany;

        var roles = Teams.GetAvailableRoles(company);

        SpawnEmployees.SetWorkerRole(roles[arg0]);
    }
}
