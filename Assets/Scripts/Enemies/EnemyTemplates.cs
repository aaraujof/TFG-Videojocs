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
    private Vector2 initial = new Vector2(0, 0);

    public void SpawnEnemies(List<Vector2> roomsSpawned)
    {
        for (int i = 1; i < roomsSpawned.Count - 1; i++)
        {
            randomNumEnemies = Random.Range(2, 6);
            for (int x = 0; x < randomNumEnemies; x++)
            {
                randomEnemy = Random.Range(0, enemies.Count);
                randomEnemyPositionX = Random.Range(-7, +8);
                randomEnemyPositionY = Random.Range(-3, +4);
                randomEnemyPosition = new Vector2(roomsSpawned[i].x + randomEnemyPositionX, roomsSpawned[i].y + randomEnemyPositionY);
                GameObject enemy = Instantiate(enemies[randomEnemy], randomEnemyPosition, enemies[randomEnemy].transform.rotation);
                assignRoom(enemy, roomsSpawned[i]);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    public void SpawnBoss(List<Vector2> roomsSpawned)
    {
        int lastRoom = roomsSpawned.Count - 1;
        randomBoss = Random.Range(0, bosses.Count);
        GameObject boss = Instantiate(bosses[randomBoss], roomsSpawned[lastRoom], bosses[randomBoss].transform.rotation);
        assignRoom(boss, roomsSpawned[lastRoom]);
        spawnedBoss.Add(boss);
    }

    void assignRoom(GameObject e, Vector2 r)
    {
        if (e.name == "Larva(Clone)")
        {
            if (e.GetComponent<LarvaController>().room == initial)
            {
                e.GetComponent<LarvaController>().SetValues(r);
            }
        }
        if (e.name == "Slime(Clone)")
        {
            if (e.GetComponent<SlimeController>().room == initial)
            {
                e.GetComponent<SlimeController>().SetValues(r);
            }
        }
        if (e.name == "Mouse(Clone)")
        {
            if (e.GetComponent<MouseController>().room == initial)
            {
                e.GetComponent<MouseController>().SetValues(r);
            }
        }
        if (e.name == "Mushroom(Clone)")
        {
            if (e.GetComponent<MushroomController>().room == initial)
            {
                e.GetComponent<MushroomController>().SetValues(r);
            }
        }
        if (e.name == "Bamboo(Clone)")
        {
            if (e.GetComponent<BambooController>().room == initial)
            {
                e.GetComponent<BambooController>().SetValues(r);
            }
        }
        if (e.name == "Skull(Clone)")
        {
            if (e.GetComponent<SkullController>().room == initial)
            {
                e.GetComponent<SkullController>().SetValues(r);
            }
        }
        if (e.name == "Frog(Clone)")
        {
            if (e.GetComponent<FrogController>().room == initial)
            {
                e.GetComponent<FrogController>().SetValues(r);
            }
        }
        if (e.name == "FrogGreen(Clone)")
        {
            if (e.GetComponent<FrogController>().room == initial)
            {
                e.GetComponent<FrogController>().SetValues(r);
            }
        }
        if (e.name == "Flam(Clone)")
        {
            if (e.GetComponent<FlamController>().room == initial)
            {
                e.GetComponent<FlamController>().SetValues(r);
            }
        }
        if (e.name == "Cyclop(Clone)")
        {
            if (e.GetComponent<CyclopController>().room == initial)
            {
                e.GetComponent<CyclopController>().SetValues(r);
            }
        }
    }
}