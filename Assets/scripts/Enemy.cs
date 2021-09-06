using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public float maxHealth = 80;
    private float currHealth;

    public LayerMask player;
    public Transform self;
    public int damage = 50;

    float next = 0f;
    public float attackRate = 2f;

    public GameObject CoreModel;
    public bool bat;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    private void Update()
    {
        Collider2D[] close = Physics2D.OverlapCircleAll(self.position, 1f, player);
        if (Time.time >= next && close != null)
        {
            foreach (Collider2D player in close)
            {
                player.GetComponent<PlayerCombat>().TakeDamage(damage);
            }
            next = Time.time + 1f / attackRate;
        }
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        anim.SetTrigger("Hurt");

        if (currHealth <= 0)
        {
            DropCore(this.GetComponent<Transform>().position);
            Die();
        }
    }

    void Die()
    {
        

        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        if (bat) { 
            GetComponent<batMove>().dead = true;
        }

        this.enabled = false;
    }

    void DropCore(UnityEngine.Vector3 spawn)
    {
        GameObject core = Instantiate(CoreModel, spawn, Quaternion.identity);
    }

}
