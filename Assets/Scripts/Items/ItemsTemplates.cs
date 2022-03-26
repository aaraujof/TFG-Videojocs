using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTemplates : MonoBehaviour
{
    public List<GameObject> items;

    private int randomNumItems;
    private int randomItem;

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
        if (roomsSpawned.Count > 25)
        {
            randomNumItems = 3;
        }
        else
        {
            randomNumItems = 2;
        }


        for (int i = 1; i < roomsSpawned.Count - 1; i++)
        {
            
        }
    }
}
