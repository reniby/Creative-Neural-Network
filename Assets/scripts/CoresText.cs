using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CoresText : MonoBehaviour
{
    public Image[] cores;
    private int found = 0;

    public Image[] extra;


    // Start is called before the first frame update
    private void Start()
    {
        foreach (Image core in cores)
        {
            core.color = new Color(0, 0, 0, 1);
        }
        foreach (Image core in extra)
        {
            core.color = new Color(0, 0, 0, 0.5f);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        int totalCores = PlayerCores.instance.numCores; //in inventory vs in scarecrow

        if (totalCores > found)
        {
            for (int i = found; i < totalCores; i++)
            {
                if (i < 5)
                {
                    cores[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
                else
                {
                    extra[i-5].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
            }
        }
        else if (totalCores < found)
        {
            for (int i = totalCores; i < found; i++)
            {
                if (i < 5)
                {
                    cores[i].GetComponent<Image>().color = new Color(0, 0, 0, 1);
                }
                else
                {
                    extra[i - 5].GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                }
            }
        }

        if (totalCores >= 5)
        {
            for (int i = 0; i < 3; i++)
            {
                extra[i].GetComponent<Image>().enabled = true;
            }
        }

        found = totalCores;
    }
}
