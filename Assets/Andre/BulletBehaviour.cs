using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public PlayerStats playerStats;
    public float speed = 6.5f;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        transform.position -= transform.right * Time.deltaTime * speed;
    }

    // destroy the bullet when it collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Building"))
        {
            Destroy(gameObject);
            
            if (collision.gameObject.CompareTag("Player"))
            {
                playerStats.TakeDamage(10f);
            }
        }
    }

}
