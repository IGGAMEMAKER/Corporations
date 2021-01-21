using UnityEngine;
using UnityEngine.EventSystems;

public class NotifyListIfElementWasChosen : MonoBehaviour, IPointerClickHandler
{
    int ElementIndex;
    ListView ListView;

    internal void SetEntity(int index, ListView listView)
    {
        ElementIndex = index;
        ListView = listView;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        ListView.ChooseElement(ElementIndex);

        //Debug.Log("Chosen element: " + ElementIndex);
    }
}
