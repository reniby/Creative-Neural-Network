using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform spawnPoint;
    public Transform firePoint;
    public GameObject ballPrefab;

    public AudioTest music;

    public LayerMask enemyLayer;
   
    public float attackRate = 2f;
    float next = 0f;

    public float maxHealth = 50f;
    public float currHealth;
    float timeDead;
    bool dead = false;

    public float damage = 25f;
    public float speed = 10f;
    public static PlayerCombat instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currHealth = maxHealth;
        music = FindObjectOfType<AudioTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= next)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                next = Time.time + 1f / attackRate;
            }
        }

        if (dead && Time.time > timeDead + 1f) 
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<PlayerMovement>().enabled = true;
            animator.SetBool("Dead", false);
            dead = false;
            currHealth = maxHealth;
            this.transform.position = spawnPoint.transform.position;
        }

        if (this.transform.position.y < -5f && Time.time > timeDead + 3f)
        {
            Die();
        }
        
    }

    void Shoot()
    {
        //play attack anim
        animator.SetTrigger("Attack");
        Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        animator.SetBool("Dead", true);
        timeDead = Time.time;
        dead = true;
        music.died();
    }
}
