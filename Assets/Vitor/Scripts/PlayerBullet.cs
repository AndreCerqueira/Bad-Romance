using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 6.5f;
    public Rigidbody2D rb;

    private ContactPoint2D[] contacts = new ContactPoint2D[16];
    private Tilemap tilemap;

    public GameObject explosionEffect;
    public GameObject smokeEffect;
    

    void Start()
    {
        // destroy bullet with Explode() after 5 seconds
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        rb.velocity = transform.right * speed;


        // make overlap circle to check for enemies and destroy them
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                //log
                Debug.Log("Hit enemy");

                // play explosion effect
                var obj1 = Instantiate(explosionEffect, transform.position, transform.rotation);
                var obj2 = Instantiate(smokeEffect, transform.position, transform.rotation);

                // play enemy puff particle prefab
                enemy.GetComponent<Marine>().PlayPuffParticle();

                

                // destroy enemy
                Destroy(enemy.gameObject);

                Destroy(obj1, 2f);
                Destroy(obj2, 2f);

                // destroy bullet
                Destroy(gameObject);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        } else if (collision.gameObject.CompareTag("Destroyable"))
        {
            // get the tilemap from the collision
            tilemap = collision.gameObject.GetComponent<Tilemap>();
            Debug.Log(tilemap);

            // destroy the cell that the bullet collided with
            int numContacts = collision.GetContacts(contacts);
            for (int i = 0; i < numContacts; i++)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(contacts[i].point);
                tilemap.SetTile(cellPosition, null);
                DestroyAdjacent(cellPosition);
            }
            
            Explode();
        } else if(collision.gameObject.CompareTag("Map"))
        {
            Explode();
        } else if (collision.gameObject.CompareTag("Building"))
        {
            // create a overlapcircle in the place of collision
            Collider2D[] hitBuildings = Physics2D.OverlapCircleAll(transform.position, 0.9f);

            // destroy all buildings in the overlapcircle
            foreach (Collider2D building in hitBuildings)
            {
                if (building.gameObject.CompareTag("Building"))
                {
                    Destroy(building.gameObject);
                    Explode();
                }
            }
        }
    }

    // destroy adjacent
    private void DestroyAdjacent(Vector3Int cellPosition)
    {
        // destroy the cell above 8
        //Vector3Int cellAbove = cellPosition + new Vector3Int(0, 1, 0);
        //tilemap.SetTile(cellAbove, null);

        // destroy the cell below 2
        Vector3Int cellBelow = cellPosition - new Vector3Int(0, 1, 0);
        tilemap.SetTile(cellBelow, null);

        // destroy the cell to the left 4
        Vector3Int cellLeft = cellPosition - new Vector3Int(1, 0, 0);
        tilemap.SetTile(cellLeft, null);

        // destroy the cell to the right 6
        Vector3Int cellRight = cellPosition + new Vector3Int(1, 0, 0);
        tilemap.SetTile(cellRight, null);

        // destroy the cell left bottom 1
        //Vector3Int cellLeftBottom = cellPosition - new Vector3Int(1, -1, 0);
        //tilemap.SetTile(cellLeftBottom, null);

        // destroy the cell right bottom 3
        //Vector3Int cellRightBottom = cellPosition + new Vector3Int(1, -1, 0);
        //tilemap.SetTile(cellRightBottom, null);

        // destroy the cell left top 7
        Vector3Int cellLeftTop = cellPosition - new Vector3Int(1, 1, 0);
        tilemap.SetTile(cellLeftTop, null);

        // destroy the cell right top 9
        //Vector3Int cellRightTop = cellPosition + new Vector3Int(1, 1, 0);
        //tilemap.SetTile(cellRightTop, null);
    }


    void Explode()
    {
        // play explosion effect
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Instantiate(smokeEffect, transform.position, transform.rotation);

        // destroy the bullet
        Destroy(gameObject);

    }


}
