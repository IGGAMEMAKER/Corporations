using UnityEngine;

public class SetAmountOfStars : View
{
    private void OnEnable()
    {
        int amountOfStars = Random.Range(1, 6);

        foreach (Transform child in transform)
            child.gameObject.SetActive(child.GetSiblingIndex() < amountOfStars);
    }
}
