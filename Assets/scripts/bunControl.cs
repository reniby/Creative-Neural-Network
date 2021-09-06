using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bunControl : MonoBehaviour
{

    private Animator anim;
    public Animator first;
    public Animator done;

    public Button cont;
    public Button keep;
    public Button face;

    private void Start()
    {
        anim = first;
    }

    private void Update()
    {
        int total = PlayerCores.instance.foundCores;
        
        if (total < 5)
        {
            anim = first;
        }
        else
        {
            anim = done;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("IsOpen", true);
            if (anim == first)
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
            done.SetBool("IsOpen", false);
            cont.gameObject.SetActive(false);
            keep.gameObject.SetActive(false);
            face.gameObject.SetActive(false);
        }
    }

}
