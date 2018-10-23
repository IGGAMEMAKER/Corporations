using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public GameObject AdvertRendererObject;

    // resources
    public GameObject MenuResourceViewObject;

    public ViewManager()
    {

    }

    public ViewManager(GameObject advertRendererObject, GameObject menuResourceViewObject)
    {
        AdvertRendererObject = advertRendererObject;
        MenuResourceViewObject = menuResourceViewObject;
    }

    public void RedrawResources(TeamResource resources, uint clients)
    {
        MenuResourceView menuView = MenuResourceViewObject.GetComponent<MenuResourceView>();
        menuView.RedrawResources(resources, clients);
    }

    public void RedrawAds(List<Advert> adverts)
    {
        AdvertRenderer advertRenderer = AdvertRendererObject.GetComponent<AdvertRenderer>();
        advertRenderer.UpdateList(adverts);
    }
}
