using UnityEngine.EventSystems;

public class OnLeavingCompetitors : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<SegmentCompetitionManagerView>().OnShowCompetitionLabel();
    }
}
