using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTemplates : MonoBehaviour
{
    public List<GameObject> items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItems(List<Vector2> roomsSpawned)
    {
        int nextItem = 8;

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
                    nextItem = nextItem + 5;
                    count--;
                }
            }
        }
    }
}
