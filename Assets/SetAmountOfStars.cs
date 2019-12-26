using Assets.Core;
using UnityEngine;

public class SetAmountOfStars : MonoBehaviour
{
    int stars;

    public void SetStars(int amount)
    {
        stars = amount;

        Render();
    }

    void Render()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(child.GetSiblingIndex() < stars);
    }
}
