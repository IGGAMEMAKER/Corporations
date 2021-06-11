using UnityEngine;
using UnityEngine.EventSystems;
// copied from enlargeOnAppearance
// call StartAnimation() if necessary
[RequireComponent(typeof(EnlargeOnDemand))]
public class EnlargeOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<EnlargeOnDemand>().StartAnimation();
    }
}
