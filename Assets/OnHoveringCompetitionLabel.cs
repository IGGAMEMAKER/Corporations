using UnityEngine.EventSystems;

public class OnHoveringCompetitionLabel : View, IPointerEnterHandler, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<SegmentCompetitionManagerView>().OnToggleCompetition();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<SegmentCompetitionManagerView>().OnShowCompetingCompaneis();
    }
}
