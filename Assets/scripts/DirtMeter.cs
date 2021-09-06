using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtMeter : MonoBehaviour
{

    private Image meter;
    
    // Start is called before the first frame update
    private void Start()
    {
        meter = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        meter.fillAmount = 1 - (Draw.instance.lineLength / Draw.instance.maxLength);
    }
}
