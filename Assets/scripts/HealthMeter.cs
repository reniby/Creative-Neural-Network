using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    private Image health;

    // Start is called before the first frame update
    private void Start()
    {
        health = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        health.fillAmount = (PlayerCombat.instance.currHealth / PlayerCombat.instance.maxHealth);
    }
}
