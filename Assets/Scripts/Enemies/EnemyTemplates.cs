using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplates : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> bosses;
    public List<GameObject> spawnedEnemies;
    public List<GameObject> spawnedBoss;

    private int randomNumEnemies;
    private int randomEnemy;
    private int randomEnemyPositionX;
    private int randomEnemyPositionY;
    private Vector2 randomEnemyPosition;
    private int randomBoss;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(List<Vector2> roomsSpawned)
    {
        for (int i = 1; i < roomsSpawned.Count - 1; i++)
        {
            randomNumEnemies = Random.Range(2, 5);
            for (int x = 0; x < randomNumEnemies; x++)
            {
                randomEnemy = Random.Range(0, enemies.Count);
                randomEnemyPositionX = Random.Range(-7, +7);
                randomEnemyPositionY = Random.Range(-3, +3);
                randomEnemyPosition = new Vector2(roomsSpawned[i].x + randomEnemyPositionX, roomsSpawned[i].y + randomEnemyPositionY);
                Instantiate(enemies[randomEnemy], randomEnemyPosition, enemies[randomEnemy].transform.rotation);
                spawnedEnemies.Add(enemies[randomEnemy]);
            }
        }
    }

    public void SpawnBoss(List<Vector2> roomsSpawned)
    {
        int lastRoom = roomsSpawned.Count-1;
        randomBoss = Random.Range(0, bosses.Count);

        Instantiate(bosses[randomBoss], roomsSpawned[lastRoom], bosses[randomBoss].transform.rotation);
        spawnedBoss.Add(bosses[randomBoss]);
    }
}
