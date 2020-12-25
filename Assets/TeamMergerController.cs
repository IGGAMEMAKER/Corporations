using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TeamMergerController : ButtonView, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static TeamMergerController Movable;
    public static TeamMergerController Target;

    private int topLayer = 2;
    private int bottomLayer = 1;

    public bool CanDrag = true;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On begin drag");
        SetSortingOrder(topLayer);

        Movable = this;
        Target = null;
    }

    void SetSortingOrder(int order)
    {
        // gameObject.transform.SetSiblingIndex(order);
        // GetComponent<Canvas>().sortingOrder = order;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (CanDrag)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        transform.localPosition = Vector3.left;
        SetSortingOrder(bottomLayer);

        MergeTeams();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");

        MergeTeams();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Target = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Target = null;
    }

    void MergeTeams()
    {
        if (Target != null && Movable != null && Target.GetInstanceID() != Movable.GetInstanceID())
        {
            var owner = Target.GetComponent<TeamPreview>().TeamInfo;
            var dependant = Movable.GetComponent<TeamPreview>().TeamInfo;

            bool merged = false;
            if (Teams.IsCanMergeTeams(Flagship, owner, dependant))
            {
                Teams.AttachTeamToTeam(dependant, owner);
                
                Debug.Log($"Attached {dependant.Name} to {owner}");

                PlaySound(Sound.Bubble7);

                Hide(Movable);

                merged = true;
            }

            Target = null;
            Movable = null;

            if (merged)
            {
                // Refresh();
                // Set
                // Navigate(ScreenMode.TeamScreen, );
            }
        }
    }

    public override void Execute()
    {
        
    }
}