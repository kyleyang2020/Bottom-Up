using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHit : MonoBehaviour
{
    public float knockbackForce = 500f;  // Adjustable knockback force
    public float knockbackDuration = 0.5f;  // Duration of knockback effect

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object hit is an enemy
        if (collision.CompareTag("ENEMY"))
        {
            // Get the enemy's state manager or controller
            EnemyStateManager enemy = collision.GetComponent<EnemyStateManager>();

            if (enemy != null)
            {
                // Calculate the knockback direction
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

                // Apply knockback to the enemy
                enemy.ApplyKnockback(knockbackDirection, knockbackForce, knockbackDuration);
            }
        }
    }
}
