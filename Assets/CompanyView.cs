using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompanyView : View, IPointerClickHandler
{
    bool showWorkers = false;

    public GameObject Workers;

    void Start()
    {
        Render();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        showWorkers = !showWorkers;

        Render();
    }

    void Render()
    {
        Draw(Workers, showWorkers);
    }

    void OnDisable()
    {
        showWorkers = false;
        Render();
    }
}
