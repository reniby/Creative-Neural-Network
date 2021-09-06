using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public LayerMask room;
    public LevelGeneration lg;

    public int roomAudio = 0;

    void Update()
    {
        Collider2D detect = Physics2D.OverlapCircle(transform.position, 1, room);

        if (detect == null && lg.stop == true) //spawn random room 
        {
            int rand = Random.Range(0, lg.rooms.Length); //number of rooms
            Instantiate(lg.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
