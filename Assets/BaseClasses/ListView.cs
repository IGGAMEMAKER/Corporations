using UnityEngine;

public abstract class ListView : MonoBehaviour
{
    public GameObject Prefab;
    GameEntity[] Entities;

    public abstract void SetItem(Transform t, GameEntity entity);
    public virtual void DebugEntity(GameEntity entity)
    {

    }

    public void SetItems(GameEntity[] entities)
    {
        Entities = entities;
        
        Render(Entities, gameObject);
    }

    void Render(GameEntity[] entities, GameObject Container)
    {
        // remove all objects in this list
        foreach (Transform child in transform)
            Destroy(child.gameObject);


        for (int i = 0; i < entities.Length; i++)
        {
            var e = entities[i];

            DebugEntity(e);

            var o = Instantiate(Prefab, transform, false);

            SetItem(o.transform, e);
        }
    }
}