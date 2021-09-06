using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGeneration : MonoBehaviour
{

    public Transform[] starting;
    public GameObject[] rooms; //index 0=LR, 1=LRB, 2=LRT, 3=LRBT
    public GameObject startRoom;
    public GameObject puzzRoom;
    public GameObject emptyRoom;
    public GameObject[] core;

    private GameObject currRoom;

    public GameObject[] enemyRooms;
    public List<GameObject> buns;

    private float easyE = 3;
    private float easyP = 3;

    private List<int> types = new List<int>();
    private List<string> typeNames = new List<string>();

    private bool sr = true;

    private int dir;
    public float moveAmt = 9f;

    public float time;
    public float startTime = 0.25f;

    public float minX = -4f;
    public float maxX = 23f;
    public float maxY = 27f;
    public bool stop = false;
    public LayerMask room;
    public LayerMask bushLayer;

    private int upCount = 0;
    private float sx;
    private float sy;

    private void Start()
    {
        int randStart = UnityEngine.Random.Range(0, starting.Length);
        transform.position = starting[randStart].position;
        Instantiate(startRoom, transform.position, UnityEngine.Quaternion.identity);
        sx = transform.position.x;
        sy = transform.position.y;

        

        types.Add(4); //combat, puzzle, bunny, empty -- not including start room
        types.Add(4);
        types.Add(4);
        types.Add(3);
        typeNames.Add("crooms");
        typeNames.Add("prooms");
        typeNames.Add("empty");
        typeNames.Add("brooms");

        dir = UnityEngine.Random.Range(1, 5);
    }

    private void Update()
    {
        if (time <= 0 && stop == false)
        {
            Move();
            time = startTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public void fill()
    {
        for (float i = -4; i <= 23; i += 9)
        {
            for (float j = 0; j <= 27; j += 9)
            {
                if (i != sx || j != sy)
                {
                    int roomType = UnityEngine.Random.Range(0, typeNames.Count);
                    UnityEngine.Vector3 roomSpawn = new UnityEngine.Vector3(i, j-1, 0);

                    if (typeNames[roomType].Equals("crooms"))
                    {
                        //if enemy: instantiate enemy room, based on difficulty level (much = easy, bat = hard)
                        int diff = UnityEngine.Random.Range(0, 4);
                        if (diff < easyE)
                        {
                            Instantiate(enemyRooms[0], roomSpawn, UnityEngine.Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(enemyRooms[1], roomSpawn, UnityEngine.Quaternion.identity);
                        }
                    }
                    else if (typeNames[roomType].Equals("prooms"))
                    {
                        //if puzzle: check for bushes, add one (order in front or behind)
                        Collider2D[] bushes = Physics2D.OverlapBoxAll(new UnityEngine.Vector3(i, j, 0), new UnityEngine.Vector2(8, 8), 0f, bushLayer);
                        int choice = UnityEngine.Random.Range(0, bushes.Length);
                        int diff = UnityEngine.Random.Range(0, 4);
                        if (diff < easyP && bushes.Length > 0)
                        {
                            Instantiate(core[0], bushes[choice].transform);
                        }
                        else if (bushes.Length > 0)
                        {
                            Instantiate(core[1], bushes[choice].transform);
                        }
                        Instantiate(puzzRoom, roomSpawn, UnityEngine.Quaternion.identity);

                    }
                    else if (typeNames[roomType].Equals("brooms"))
                    {
                        //if bun: instantiate necessary platform, instantiate bunny
                        int r = UnityEngine.Random.Range(0, buns.Count);
                        GameObject instance = Instantiate(buns[r], roomSpawn, UnityEngine.Quaternion.identity);

                        buns.RemoveAt(r);
                    }
                    else if (typeNames[roomType].Equals("empty"))
                    {
                        Instantiate(emptyRoom, roomSpawn, UnityEngine.Quaternion.identity);
                    }

                    types[roomType] -= 1;
                    if (types[roomType] == 0)
                    {
                        types.RemoveAt(roomType);
                        typeNames.RemoveAt(roomType);
                    }
                }
            }
        }
    }

    private void Move()
    {
        if (dir == 1 || dir == 2) //Move Right
        {
            if (transform.position.x < maxX)
            {
                sr = false;
                upCount = 0;

                UnityEngine.Vector2 newPos = new UnityEngine.Vector2(transform.position.x + moveAmt, transform.position.y);
                transform.position = newPos;

                int rand = UnityEngine.Random.Range(0, 4); //any room can spawn (not startroom)
                currRoom = Instantiate(rooms[rand], transform.position, UnityEngine.Quaternion.identity);

                dir = UnityEngine.Random.Range(1, 6);
                if (dir == 3)
                {
                    dir = 1;
                }
                else if (dir == 4)
                {
                    dir = 5;
                }
            } 
            else
            {
                dir = 5;
            }
        }
        else if (dir == 3 || dir == 4) //Move Left
        {
            if (transform.position.x > minX)
            {
                sr = false;
                upCount = 0;

                UnityEngine.Vector2 newPos = new UnityEngine.Vector2(transform.position.x - moveAmt, transform.position.y);
                transform.position = newPos;

                int rand = UnityEngine.Random.Range(0, 4); //any room can spawn
                currRoom = Instantiate(rooms[rand], transform.position, UnityEngine.Quaternion.identity);

                dir = UnityEngine.Random.Range(3, 6);
            }
            else
            {
                dir = 5;
            }
        }
        else if (dir == 5) //Move Up
        {
            upCount++;
            
            if (transform.position.y < maxY)
            {
                Collider2D roomDetect = Physics2D.OverlapCircle(transform.position, 1, room);
                
                //if (roomDetect.GetComponent<RoomType>().type != 2 && roomDetect.GetComponent<RoomType>().type != 3 && sr == false) //no top, to have top must be 2 or 3
                //{
                //    if (upCount >= 2)
                //    {
                //        roomDetect.GetComponent<RoomType>().DestroyRoom(currRoom); //this doesn't work, why?
                //        currRoom = Instantiate(rooms[3], transform.position, UnityEngine.Quaternion.identity);
                //    }
                //    else
                //    {
                //        roomDetect.GetComponent<RoomType>().DestroyRoom(currRoom);
                //        int randTop = UnityEngine.Random.Range(2, 4);
                //        currRoom = Instantiate(rooms[randTop], transform.position, UnityEngine.Quaternion.identity);
                //    }
                //}

                sr = false;

                UnityEngine.Vector2 newPos = new UnityEngine.Vector2(transform.position.x, transform.position.y + moveAmt);
                transform.position = newPos;

                int rand = UnityEngine.Random.Range(2, 4); //need a bottom opening, 1 or 3
                if (rand == 2)
                {
                    rand = 1;
                }
                currRoom = Instantiate(rooms[rand], transform.position, UnityEngine.Quaternion.identity);

                dir = UnityEngine.Random.Range(1, 6);
            }
            else
            {
                stop = true;
                Invoke("fill", 1f);
            } 
        }
    }
}
