using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcControl : MonoBehaviour
{

    private Animator anim;
    public Animator before;
    public Animator after;
    public Animator zero;

    public Button contin;
    public Button yes;
    public Button no;

    public AudioTest music;

    public static npcControl instance;
    public bool helped = false;

    private void Start()
    {
        instance = this;
        helped = false;
        anim = zero;
        before.SetBool("IsOpen", false);
        after.SetBool("IsOpen", false);
        zero.SetBool("IsOpen", false);
        music = FindObjectOfType<AudioTest>();
    }

    private void Update()
    {
        //Debug.Log(anim + " " + helped);
        if (PlayerCores.instance.numCores == 0 && !helped)
        {
            anim = zero;
        }
        else if (!helped)
        {
            anim = before;
        }
        else
        {
            anim = after;
        }

    }

    public void giveCore()
    {
        PlayerCores.instance.numCores -= 1;
        helped = true;
        music.bun();
        anim.SetBool("IsOpen", false);
        anim = after;
    }

    public void cont()
    {
        contin.gameObject.SetActive(true);
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
    }

    public void ask()
    {
        contin.gameObject.SetActive(false);
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("IsOpen", true);

            if (anim == before)
            {
                ask();
            }
            else
            {
                cont();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            before.SetBool("IsOpen", false);
            after.SetBool("IsOpen", false);
            zero.SetBool("IsOpen", false);

            contin.gameObject.SetActive(false);
            yes.gameObject.SetActive(false);
            no.gameObject.SetActive(false);
        }
    }
}
