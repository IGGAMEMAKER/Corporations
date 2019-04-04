using UnityEngine;

public abstract class ListView : MonoBehaviour
{
    public GameObject Prefab;
    GameEntity[] Entities;

    public abstract void SetItem(Transform t, GameEntity entity);

    public void SetItems(GameEntity[] entities)
    {
        Entities = entities;

        Render(Entities, gameObject);
    }

    void RemoveInstances(int amount, GameObject Container)
    {
        for (var i = 0; i < amount; i++)
            Destroy(Container.transform.GetChild(Container.transform.childCount - 1).gameObject);
    }

    void SpawnInstances(int amount, GameObject Container)
    {
        for (var i = 0; i < amount; i++)
            Instantiate(Prefab, Container.transform, false);
    }

    void ProvideEnoughInstances(GameEntity[] list, GameObject Container)
    {
        int childCount = Container.transform.childCount;

        int listCount = list.Length;

        if (listCount < childCount)
            RemoveInstances(childCount - listCount, Container);
        else
            SpawnInstances(listCount - childCount, Container);
    }

    void Render(GameEntity[] entities, GameObject Container)
    {
        ProvideEnoughInstances(entities, Container);

        for (int i = 0; i < entities.Length; i++)
            SetItem(Container.transform.GetChild(i), entities[i]);
    }
}