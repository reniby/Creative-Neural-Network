using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeControl : MonoBehaviour
{
    public Transform self;
    public LayerMask player;
    float next = 0f;
    public float attackRate = 2f;
    int damage = 10;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time >= next && other.gameObject.layer == 8)
        {
            other.GetComponent<PlayerCombat>().TakeDamage(damage);
            next = Time.time + 1f / attackRate;
        }
    }
}
