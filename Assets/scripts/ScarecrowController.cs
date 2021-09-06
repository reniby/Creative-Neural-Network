using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ScarecrowController : MonoBehaviour
{
    public int currCores = 0;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision) //PCores = 12, ECores = 13
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerCores.instance.numCores > 0 && currCores <= 5)
            {
                currCores += PlayerCores.instance.numCores;
                PlayerCores.instance.foundCores = currCores;
                if (PlayerCores.instance.numCores >= 5)
                {
                    PlayerCores.instance.numCores -= 5;
                }
                else
                {
                    PlayerCores.instance.numCores = 0;
                }
                
                anim.SetInteger("State", currCores);
            }
        }
    }



}
