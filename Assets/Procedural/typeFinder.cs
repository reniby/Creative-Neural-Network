using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class typeFinder : MonoBehaviour
{
    public int roomType;
    public AudioTest music;

    public void Start()
    {
        music = FindObjectOfType<AudioTest>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && roomType != null)
        {
            music.room(roomType);
        }
    }
}
