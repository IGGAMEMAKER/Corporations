using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowWorkerUpgrades : View, IPointerClickHandler
{
    bool showUpgrades = false;

    public GameObject Upgrades;

    void Start()
    {
        Render();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        showUpgrades = !showUpgrades;

        Render();
    }

    void Render()
    {
        Draw(Upgrades, showUpgrades);
    }

    void OnDisable()
    {
        showUpgrades = false;
        Render();
    }
}
