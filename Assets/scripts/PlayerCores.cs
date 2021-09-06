using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCores : MonoBehaviour
{

    public static PlayerCores instance;
    public int numCores = 0;
    public int foundCores = 0;

    public int ecores = 0;
    public int pcores = 0;

    public AudioTest music;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        music = FindObjectOfType<AudioTest>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D core) //PCores = 12, ECores = 13
    {
        if (core.gameObject.CompareTag("Cores"))
        {
            bool pc = true;

            if (core.gameObject.layer == 12) //Platform Core
            {
                Draw.instance.maxLength += 2;
                pcores++;
                pc = true;
            }
            else if (core.gameObject.layer == 13)
            {
                PlayerCombat.instance.damage += 20;
                PlayerCombat.instance.currHealth = PlayerCombat.instance.maxHealth;
                PlayerCombat.instance.speed += 2.5f;
                PlayerCombat.instance.attackRate -= 0.2f;
                ecores++;
                pc = false;
            }

            music.coreUpdate(pc);

            Destroy(core.gameObject);
            numCores++;

        }       
    }
}
