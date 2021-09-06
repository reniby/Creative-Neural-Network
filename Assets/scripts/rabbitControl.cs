using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rabbitControl : MonoBehaviour
{

    private Animator anim;
    public Animator first;
    public Animator hasThree;
    public Animator hasFour;
    public Animator hasFive;
    public Animator done;
    public Animator play;

    public Button cont;
    public Button keep;
    public Button face;

    private bool given = false;
    private bool helped = false;

    private void Update()
    {
        int total = PlayerCores.instance.numCores + PlayerCores.instance.foundCores;

        helped = npcControl.instance.helped;

        if (hasThree != null)
        { 
            if (total == 0)
            {
                anim = first;
            }
            else if (total == 3)
            {
                anim = hasThree;
            }
            else if (total == 4)
            {
                anim = hasFour;
            }
            else if (total == 5)
            {
                anim = hasFive;
            }
            if (PlayerCores.instance.foundCores == 5)
            {
                anim = done;
            }
            if (helped)
            {
                anim = play;
            }
        }
        
    }

    public void playGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("IsOpen", true);
            if (anim != play)
            {
                cont.gameObject.SetActive(true);
                keep.gameObject.SetActive(false);
                face.gameObject.SetActive(false);
            }
            else
            {
                cont.gameObject.SetActive(false);
                keep.gameObject.SetActive(true);
                face.gameObject.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            first.SetBool("IsOpen", false);
            hasThree.SetBool("IsOpen", false);
            hasFour.SetBool("IsOpen", false);
            hasFive.SetBool("IsOpen", false);
            done.SetBool("IsOpen", false);
            cont.gameObject.SetActive(false);
            keep.gameObject.SetActive(false);
            face.gameObject.SetActive(false);
        }
    }

    public void giveThree()
    {
        if (!given)
        {
            PlayerCores.instance.numCores += 3;
            given = true;
        }
    }
}
