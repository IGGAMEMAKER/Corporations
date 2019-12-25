using UnityEngine;

public class RenderRandomAmountOfStars : MonoBehaviour
{
    private void OnEnable()
    {
        int amountOfStars = Random.Range(1, 6);

        GetComponent<SetAmountOfStars>().SetStars(amountOfStars);
    }
}
