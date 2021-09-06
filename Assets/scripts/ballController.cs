using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{

    public Rigidbody2D rb;

    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * PlayerCombat.instance.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            Destroy(gameObject);
            enemy.TakeDamage(PlayerCombat.instance.damage);
        }
    }

}
