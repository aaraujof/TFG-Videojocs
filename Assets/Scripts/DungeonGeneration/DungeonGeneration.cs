using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public List<GameObject> rooms;
    public List<string> roomSides = new List<string>();
    public List<Vector2> unavailablePositions = new List<Vector2>();
    public Dictionary<Vector2, string> map = new Dictionary<Vector2, string>();
  
    private RoomTemplates templates;
    private int randomRoom;
    private int randomRoomTemplate;
    private int numRooms;
    private int maxNumRooms;

    private int templateRoom;
    private EnemyTemplates enemyTemplates;
    private ItemsTemplates itemsTemplates;

    public List<GameObject> roomsCreated;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        maxNumRooms = Random.Range(15, 26);

        initialRoom(templates, transform.position);

        //int count = 0; 
        for (int i = 0; i < maxNumRooms; i++)
        {
            instantiateRooms(templates, rooms[i], unavailablePositions[i]);
            //Debug.Log("InstantiateRoom: " + rooms[i].name + " Position: " + unavailablePositions[i]);
            //count++;
        }

        //Debug.Log(count);

        for (int i = 0; i < rooms.Count; i++)
        {
            closeRooms(rooms[i], unavailablePositions[i]);
            //Debug.Log("Room: " + rooms[i].name + " Position: " + unavailablePositions[i]);
        }

        enemyTemplates = GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyTemplates>();
        enemyTemplates.SpawnEnemies(unavailablePositions);
        enemyTemplates.SpawnBoss(unavailablePositions);
        itemsTemplates = GameObject.FindGameObjectWithTag("Items").GetComponent<ItemsTemplates>();
        itemsTemplates.SpawnItems(unavailablePositions);
    }

    void Update()
    {
        //
    }

    private bool checkPosition(Vector2 position, List<Vector2> positons)
    {
        if (positons.Contains(position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool checkNeighborTop(Vector2 position, List<Vector2> positons)
    {
        Vector2 top = new Vector2(position.x, position.y + 10);

        if (positons.Contains(top))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool checkNeighborBottom(Vector2 position, List<Vector2> positons)
    {
        Vector2 bottom = new Vector2(position.x, position.y - 10);

        if (positons.Contains(bottom))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool checkNeighborLeft(Vector2 position, List<Vector2> positons)
    {
        Vector2 left = new Vector2(position.x - 18, position.y);

        if (positons.Contains(left))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool checkNeighborRight(Vector2 position, List<Vector2> positons)
    {
        Vector2 right = new Vector2(position.x + 18, position.y);

        if (positons.Contains(right))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int numRoomDoors(string roomName)
    {
        int doors = 0;

        if (roomName.Contains("L"))
        {
            doors++;
        }
        if (roomName.Contains("R"))
        {
            doors++;
        }
        if (roomName.Contains("T"))
        {
            doors++;
        }
        if (roomName.Contains("B"))
        {
            doors++;
        }

        return doors;
    }

    private List<string> typeRoomDoors(string roomName)
    {
        List<string> doors = new List<string>();

        if (roomName.Contains("L"))
        {
            doors.Add("L");
        }
        if (roomName.Contains("R"))
        {
            doors.Add("R");
        }
        if (roomName.Contains("T"))
        {
            doors.Add("T");
        }
        if (roomName.Contains("B"))
        {
            doors.Add("B");
        }

        return doors;
    }

    private void initialRoom(RoomTemplates templates, Vector2 position)
    {
        randomRoom = Random.Range(0, 4);

        if (randomRoom == 0)
        {
            randomRoomTemplate = Random.Range(1, templates.topRooms.Length);
            Instantiate(templates.topRooms[randomRoomTemplate], position, templates.topRooms[randomRoomTemplate].transform.rotation);
            unavailablePositions.Add(transform.position);
            rooms.Add(templates.topRooms[randomRoomTemplate]);
        }
        else if (randomRoom == 1)
        {
            randomRoomTemplate = Random.Range(1, templates.bottomRooms.Length);
            Instantiate(templates.bottomRooms[randomRoomTemplate], position, templates.bottomRooms[randomRoomTemplate].transform.rotation);
            unavailablePositions.Add(transform.position);
            rooms.Add(templates.bottomRooms[randomRoomTemplate]);
        }
        else if (randomRoom == 2)
        {
            randomRoomTemplate = Random.Range(1, templates.leftRooms.Length);
            Instantiate(templates.leftRooms[randomRoomTemplate], position, templates.leftRooms[randomRoomTemplate].transform.rotation);
            unavailablePositions.Add(transform.position);
            rooms.Add(templates.leftRooms[randomRoomTemplate]);
        }
        else if (randomRoom == 3)
        {
            randomRoomTemplate = Random.Range(1, templates.rightRooms.Length);
            Instantiate(templates.rightRooms[randomRoomTemplate], position, templates.rightRooms[randomRoomTemplate].transform.rotation);
            unavailablePositions.Add(transform.position);
            rooms.Add(templates.rightRooms[randomRoomTemplate]);
        }

        this.numRooms++;
    }

    private void instantiateRooms(RoomTemplates templates, GameObject room, Vector2 position)
    {
        bool top = checkNeighborTop(position, this.unavailablePositions);
        bool bottom = checkNeighborBottom(position, this.unavailablePositions);
        bool left = checkNeighborLeft(position, this.unavailablePositions);
        bool right = checkNeighborRight(position, this.unavailablePositions);

        int numDoors = numRoomDoors(room.name);
        List<string> typeDoors = typeRoomDoors(room.name);
        string roomLetter;
        List<string> sides = new List<string>();

        for (int i = 0; i < numDoors; i++)
        {
            roomLetter = typeDoors[Random.Range(0, typeDoors.Count)];

            if (roomLetter == "R")
            {
                Vector2 pos = new Vector2(position.x + 18, position.y);

                if(checkPosition(pos, this.unavailablePositions) == false)
                {
                    top = checkNeighborTop(pos, this.unavailablePositions);
                    bottom = checkNeighborBottom(pos, this.unavailablePositions);
                    right = checkNeighborRight(pos, this.unavailablePositions);

                    if (top == false && bottom == false && right == false)
                    {
                        templateRoom = Random.Range(0, templates.leftRooms.Length);
                        Instantiate(templates.leftRooms[templateRoom], pos, templates.leftRooms[templateRoom].transform.rotation);
                        rooms.Add(templates.leftRooms[templateRoom]);
                    }
                    else if (top == true && bottom == true && right == true)
                    {
                        Instantiate(templates.leftRooms[7], pos, templates.leftRooms[7].transform.rotation);
                        rooms.Add(templates.leftRooms[7]);
                    }
                    else 
                    {
                        List<Object> possibleRooms = possibleRoomsRight(pos, top, bottom, right);
                        randomRoom = Random.Range(0, possibleRooms.Count);

                        foreach (GameObject r in templates.leftRooms)
                        {
                            if (r.name == possibleRooms[randomRoom].name)
                            {
                                Instantiate(r, pos, r.transform.rotation);
                                rooms.Add(r);
                                break;
                            }
                        }
                    }

                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
            if (roomLetter == "L")
            {
                Vector2 pos = new Vector2(position.x - 18, position.y);

                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    top = checkNeighborTop(pos, this.unavailablePositions);
                    bottom = checkNeighborBottom(pos, this.unavailablePositions);
                    left = checkNeighborLeft(pos, this.unavailablePositions);

                    if (top == false && bottom == false && left == false)
                    {
                        templateRoom = Random.Range(0, templates.rightRooms.Length);
                        Instantiate(templates.rightRooms[templateRoom], pos, templates.rightRooms[templateRoom].transform.rotation);
                        rooms.Add(templates.rightRooms[templateRoom]);
                    }
                    else if(top == true && bottom == true && left == true)
                    {
                        Instantiate(templates.rightRooms[7], pos, templates.rightRooms[7].transform.rotation);
                        rooms.Add(templates.rightRooms[7]);
                    }
                    else
                    {
                        List<Object> possibleRooms = possibleRoomsLeft(pos, top, bottom, left);
                        randomRoom = Random.Range(0, possibleRooms.Count);

                        foreach (GameObject r in templates.rightRooms)
                        {
                            if (r.name == possibleRooms[randomRoom].name)
                            {
                                Instantiate(r, pos, r.transform.rotation);
                                rooms.Add(r);
                                break;
                            }
                        }
                    }

                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
            if (roomLetter == "B")
            {
                Vector2 pos = new Vector2(position.x, position.y - 10);

                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    right = checkNeighborRight(pos, this.unavailablePositions);
                    bottom = checkNeighborBottom(pos, this.unavailablePositions);
                    left = checkNeighborLeft(pos, this.unavailablePositions);

                    if (right == false && bottom == false && left == false)
                    {
                        templateRoom = Random.Range(0, templates.topRooms.Length);
                        Instantiate(templates.topRooms[templateRoom], pos, templates.topRooms[templateRoom].transform.rotation);
                        rooms.Add(templates.topRooms[templateRoom]);
                    }
                    else if (right == true && bottom == true && left == true)
                    {
                        Instantiate(templates.topRooms[7], pos, templates.topRooms[7].transform.rotation);
                        rooms.Add(templates.topRooms[7]);
                    }
                    else
                    {
                        List<Object> possibleRooms = possibleRoomsBottom(pos, bottom, left, right);
                        randomRoom = Random.Range(0, possibleRooms.Count);

                        foreach (GameObject r in templates.topRooms)
                        {
                            if (r.name == possibleRooms[randomRoom].name)
                            {
                                Instantiate(r, pos, r.transform.rotation);
                                rooms.Add(r);
                                break;
                            }
                        }
                    }

                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
            if (roomLetter == "T")
            {
                Vector2 pos = new Vector2(position.x, position.y + 10);

                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    right = checkNeighborRight(pos, this.unavailablePositions);
                    top = checkNeighborTop(pos, this.unavailablePositions);
                    left = checkNeighborLeft(pos, this.unavailablePositions);

                    if (right == false && top == false && left == false)
                    {
                        templateRoom = Random.Range(0, templates.bottomRooms.Length);
                        Instantiate(templates.bottomRooms[templateRoom], pos, templates.bottomRooms[templateRoom].transform.rotation);
                        rooms.Add(templates.bottomRooms[templateRoom]);
                    }
                    else if (right == true && top == true && left == true)
                    {
                        Instantiate(templates.bottomRooms[7], pos, templates.bottomRooms[7].transform.rotation);
                        rooms.Add(templates.bottomRooms[7]);
                    }
                    else
                    {
                        List<Object> possibleRooms = possibleRoomsTop(pos, top, left, right);
                        randomRoom = Random.Range(0, possibleRooms.Count);

                        foreach (GameObject r in templates.bottomRooms)
                        {
                            if (r.name == possibleRooms[randomRoom].name)
                            {
                                Instantiate(r, pos, r.transform.rotation);
                                rooms.Add(r);
                                break;
                            }
                        }
                    }

                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
        }
    }

    private List<Object> possibleRoomsRight(Vector2 position, bool top, bool bottom, bool right)
    {
        string topName, bottomName, rightName;

        List<Object> possibleRooms = new List<Object>(this.templates.leftRooms);

        if (top == true)
        {
            Vector2 pos = new Vector2(position.x, position.y + 10);
            topName = neighborTopName(pos);
            if (topName.Contains("B") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "T");
            }
        }

        if (bottom == true)
        {
            Vector2 pos = new Vector2(position.x, position.y - 10);
            bottomName = neighborBottomName(pos);

            if (bottomName.Contains("T") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "B");
            }
        }

        if (right == true)
        {
            Vector2 pos = new Vector2(position.x + 18, position.y);
            rightName = neighborRightName(pos);

            if (rightName.Contains("L") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "R");
            }
        }

        return possibleRooms;
    }

    private List<Object> possibleRoomsLeft(Vector2 position, bool top, bool bottom, bool left)
    {
        string topName, bottomName, leftName;

        List<Object> possibleRooms = new List<Object>(this.templates.rightRooms);

        if (top == true)
        {
            Vector2 pos = new Vector2(position.x, position.y + 10);
            topName = neighborTopName(pos);

            if (topName.Contains("B") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "T");
            }
        }

        if (bottom == true)
        {
            Vector2 pos = new Vector2(position.x, position.y - 10);
            bottomName = neighborBottomName(pos);

            if (bottomName.Contains("T") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "B");
            }
        }

        if (left == true)
        {
            Vector2 pos = new Vector2(position.x - 18, position.y);
            leftName = neighborLeftName(pos);

            if (leftName.Contains("R") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "L");
            }
        }

        return possibleRooms;
    }

    private List<Object> possibleRoomsTop(Vector2 position, bool top, bool left, bool right)
    {
        string topName, leftName, rightName;

        List<Object> possibleRooms = new List<Object>(this.templates.bottomRooms);

        if (top == true)
        {
            Vector2 pos = new Vector2(position.x, position.y + 10);
            topName = neighborTopName(pos);

            if (topName.Contains("B") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "T");
            }
        }

        if (right == true)
        {
            Vector2 pos = new Vector2(position.x + 18, position.y);
            rightName = neighborRightName(pos);

            if (rightName.Contains("L") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "R");
            }
        }

        if (left == true)
        {
            Vector2 pos = new Vector2(position.x - 18, position.y);
            leftName = neighborLeftName(pos);

            if (leftName.Contains("R") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "L");
            }
        }

        return possibleRooms;
    }

    private List<Object> possibleRoomsBottom(Vector2 position, bool bottom, bool left, bool right)
    {
        string bottomName, leftName, rightName;

        List<Object> possibleRooms = new List<Object>(this.templates.topRooms);

        if (bottom == true)
        {
            Vector2 pos = new Vector2(position.x, position.y - 10);
            bottomName = neighborBottomName(pos);

            if (bottomName.Contains("T") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "B");
            }
        }

        if (right == true)
        {
            Vector2 pos = new Vector2(position.x + 18, position.y);
            rightName = neighborRightName(pos);

            if (rightName.Contains("L") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "R");
            }
        }

        if (left == true)
        {
            Vector2 pos = new Vector2(position.x - 18, position.y);
            leftName = neighborLeftName(pos);

            if (leftName.Contains("R") == false)
            {
                possibleRooms = dropRooms(possibleRooms, "L");
            }
        }

        return possibleRooms;
    }

    private List<Object> dropRooms(List<Object> roomsList, string letter)
    {
        List<Object> newRoomsList = roomsList;

        for (int i = 0; i < roomsList.Count; i++)
        {
            if (roomsList[i].name.Contains(letter))
            {
                newRoomsList.RemoveRange(i, 1); ;
            }

        }

        return newRoomsList;
    }

    private string neighborTopName(Vector2 position)
    {
        return this.rooms[unavailablePositions.IndexOf(new Vector2(position.x, position.y))].name;
    }

    private string neighborBottomName(Vector2 position)
    {
        return this.rooms[unavailablePositions.IndexOf(new Vector2(position.x, position.y))].name;
    }
    private string neighborLeftName(Vector2 position)
    {
        return this.rooms[unavailablePositions.IndexOf(new Vector2(position.x, position.y))].name;
    }
    private string neighborRightName(Vector2 position)
    {
        return this.rooms[unavailablePositions.IndexOf(new Vector2(position.x, position.y))].name;
    }

    private void closeRooms(GameObject room, Vector2 position)
    {
        bool top;
        bool bottom;
        bool left;
        bool right;
        string roomLetter;
        List<string> sides = new List<string>();
        int numDoors = numRoomDoors(room.name);
        List<string> typeDoors = typeRoomDoors(room.name);

        for (int i = 0; i < numDoors; i++)
        {
            roomLetter = typeDoors[Random.Range(0, typeDoors.Count)];

            if (roomLetter == "R")
            {
                Vector2 pos = new Vector2(position.x + 18, position.y);
                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    top = checkNeighborTop(pos, this.unavailablePositions);
                    bottom = checkNeighborBottom(pos, this.unavailablePositions);
                    right = checkNeighborRight(pos, this.unavailablePositions);

                    if (top == false && bottom == false && right == false)
                    {
                        Instantiate(templates.leftRooms[0], pos, templates.leftRooms[0].transform.rotation);
                        rooms.Add(templates.leftRooms[0]);
                    }
                    else if (top == false && bottom == true && right == false)
                    {
                        Instantiate(templates.leftRooms[1], pos, templates.leftRooms[1].transform.rotation);
                        rooms.Add(templates.leftRooms[1]);
                    }
                    else if (top == false && bottom == false && right == true)
                    {
                        Instantiate(templates.leftRooms[2], pos, templates.leftRooms[2].transform.rotation);
                        rooms.Add(templates.leftRooms[2]);
                    }
                    else if (top == true && bottom == false && right == false)
                    {
                        Instantiate(templates.leftRooms[3], pos, templates.leftRooms[3].transform.rotation);
                        rooms.Add(templates.leftRooms[3]);
                    }
                    else if (top == false && bottom == true && right == true)
                    {
                        Instantiate(templates.leftRooms[4], pos, templates.leftRooms[4].transform.rotation);
                        rooms.Add(templates.leftRooms[4]);
                    }
                    else if (top == true && bottom == true && right == false)
                    {
                        Instantiate(templates.leftRooms[5], pos, templates.leftRooms[5].transform.rotation);
                        rooms.Add(templates.leftRooms[5]);
                    }
                    else if (top == true && bottom == false && right == true)
                    {
                        Instantiate(templates.leftRooms[6], pos, templates.leftRooms[6].transform.rotation);
                        rooms.Add(templates.leftRooms[6]);
                    }
                    else if (top == true && bottom == true && right == true)
                    {
                        Instantiate(templates.leftRooms[7], pos, templates.leftRooms[7].transform.rotation);
                        rooms.Add(templates.leftRooms[7]);
                    }
                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
            if (roomLetter == "L")
            {
                Vector2 pos = new Vector2(position.x - 18, position.y);

                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    top = checkNeighborTop(pos, this.unavailablePositions);
                    bottom = checkNeighborBottom(pos, this.unavailablePositions);
                    left = checkNeighborLeft(pos, this.unavailablePositions);

                    if (top == false && bottom == false && left == false)
                    {
                        Instantiate(templates.rightRooms[0], pos, templates.rightRooms[0].transform.rotation);
                        rooms.Add(templates.rightRooms[0]);
                    }
                    else if (top == false && bottom == true && left == false)
                    {
                        Instantiate(templates.rightRooms[1], pos, templates.rightRooms[1].transform.rotation);
                        rooms.Add(templates.rightRooms[1]);
                    }
                    else if (top == false && bottom == false && left == true)
                    {
                        Instantiate(templates.rightRooms[2], pos, templates.rightRooms[2].transform.rotation);
                        rooms.Add(templates.rightRooms[2]);
                    }
                    else if (top == true && bottom == false && left == false)
                    {
                        Instantiate(templates.rightRooms[3], pos, templates.rightRooms[3].transform.rotation);
                        rooms.Add(templates.rightRooms[3]);
                    }
                    else if (top == true && bottom == true && left == false)
                    {
                        Instantiate(templates.rightRooms[4], pos, templates.rightRooms[4].transform.rotation);
                        rooms.Add(templates.rightRooms[4]);
                    }
                    else if (top == false && bottom == true && left == true)
                    {
                        Instantiate(templates.rightRooms[5], pos, templates.rightRooms[5].transform.rotation);
                        rooms.Add(templates.rightRooms[5]);
                    }
                    else if (top == true && bottom == false && left == true)
                    {
                        Instantiate(templates.rightRooms[6], pos, templates.rightRooms[6].transform.rotation);
                        rooms.Add(templates.rightRooms[6]);
                    }
                    else if (top == true && bottom == true && left == true)
                    {
                        Instantiate(templates.rightRooms[7], pos, templates.rightRooms[7].transform.rotation);
                        rooms.Add(templates.rightRooms[7]);
                    }
                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
            if (roomLetter == "B")
            {
                Vector2 pos = new Vector2(position.x, position.y - 10);

                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    right = checkNeighborRight(pos, this.unavailablePositions);
                    bottom = checkNeighborBottom(pos, this.unavailablePositions);
                    left = checkNeighborLeft(pos, this.unavailablePositions);

                    if (right == false && bottom == false && left == false)
                    {
                        Instantiate(templates.topRooms[0], pos, templates.topRooms[0].transform.rotation);
                        rooms.Add(templates.topRooms[0]);
                    }
                    else if (right == false && bottom == true && left == false)
                    {
                        Instantiate(templates.topRooms[1], pos, templates.topRooms[1].transform.rotation);
                        rooms.Add(templates.topRooms[1]);
                    }
                    else if (right == false && bottom == false && left == true)
                    {
                        Instantiate(templates.topRooms[2], pos, templates.topRooms[2].transform.rotation);
                        rooms.Add(templates.topRooms[2]);
                    }
                    else if (right == true && bottom == false && left == false)
                    {
                        Instantiate(templates.topRooms[3], pos, templates.topRooms[3].transform.rotation);
                        rooms.Add(templates.topRooms[3]);
                    }
                    else if (right == false && bottom == true && left == true)
                    {
                        Instantiate(templates.topRooms[4], pos, templates.topRooms[4].transform.rotation);
                        rooms.Add(templates.topRooms[4]);
                    }
                    else if (right == true && bottom == true && left == false)
                    {
                        Instantiate(templates.topRooms[5], pos, templates.topRooms[5].transform.rotation);
                        rooms.Add(templates.topRooms[5]);
                    }
                    else if (right == true && bottom == false && left == true)
                    {
                        Instantiate(templates.topRooms[6], pos, templates.topRooms[6].transform.rotation);
                        rooms.Add(templates.topRooms[6]);
                    }
                    else if (right == true && bottom == true && left == true)
                    {
                        Instantiate(templates.topRooms[7], pos, templates.topRooms[7].transform.rotation);
                        rooms.Add(templates.topRooms[7]);
                    }
                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
            if (roomLetter == "T")
            {
                Vector2 pos = new Vector2(position.x, position.y + 10);

                if (checkPosition(pos, this.unavailablePositions) == false)
                {
                    right = checkNeighborRight(pos, this.unavailablePositions);
                    top = checkNeighborTop(pos, this.unavailablePositions);
                    left = checkNeighborLeft(pos, this.unavailablePositions);

                    if (right == false && top == false && left == false)
                    {
                        Instantiate(templates.bottomRooms[0], pos, templates.bottomRooms[0].transform.rotation);
                        rooms.Add(templates.bottomRooms[0]);
                    }
                    else if (right == false && top == false && left == true)
                    {
                        Instantiate(templates.bottomRooms[1], pos, templates.bottomRooms[1].transform.rotation);
                        rooms.Add(templates.bottomRooms[1]);
                    }
                    else if (right == true && top == false && left == false)
                    {
                        Instantiate(templates.bottomRooms[2], pos, templates.bottomRooms[2].transform.rotation);
                        rooms.Add(templates.bottomRooms[2]);
                    }
                    else if (right == false && top == true && left == false)
                    {
                        Instantiate(templates.bottomRooms[3], pos, templates.bottomRooms[3].transform.rotation);
                        rooms.Add(templates.bottomRooms[3]);
                    }
                    else if (right == true && top == false && left == true)
                    {
                        Instantiate(templates.bottomRooms[4], pos, templates.bottomRooms[4].transform.rotation);
                        rooms.Add(templates.bottomRooms[4]);
                    }
                    else if (right == false && top == true && left == true)
                    {
                        Instantiate(templates.bottomRooms[5], pos, templates.bottomRooms[5].transform.rotation);
                        rooms.Add(templates.bottomRooms[5]);
                    }
                    else if (right == true && top == true && left == false)
                    {
                        Instantiate(templates.bottomRooms[6], pos, templates.bottomRooms[6].transform.rotation);
                        rooms.Add(templates.bottomRooms[6]);
                    }
                    else if (right == true && top == true && left == true)
                    {
                        Instantiate(templates.bottomRooms[7], pos, templates.bottomRooms[7].transform.rotation);
                        rooms.Add(templates.bottomRooms[7]);
                    }
                    unavailablePositions.Add(pos);
                    this.numRooms++;
                }
                typeDoors.Remove(roomLetter);
            }
        }
    }
}
