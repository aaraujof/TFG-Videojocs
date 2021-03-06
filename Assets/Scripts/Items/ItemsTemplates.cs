using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTemplates : MonoBehaviour
{
    public List<GameObject> items;

    public void SpawnItems(List<Vector2> roomsSpawned)
    {
        int nextItem = 7;

        int count = items.Count;
        for (int i = 1; i < roomsSpawned.Count - 1; i++)
        {
            if (i == nextItem)
            {
                if (count > 0)
                {
                    int randomItem = Random.Range(0, items.Count);
                    Instantiate(items[randomItem], roomsSpawned[i], items[randomItem].transform.rotation);
                    items.Remove(items[randomItem]);
                    nextItem = nextItem + 7;
                    count--;
                }
            }
        }
    }
}
