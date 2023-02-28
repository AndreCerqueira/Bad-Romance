using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CannonBallBehaviour : MonoBehaviour
{
    // explosion particles prefab
    public GameObject explosionParticles;
    public GameObject smokeParticles;

    private Tilemap tilemap;
    private ContactPoint2D[] contacts = new ContactPoint2D[16];

    private bool gotPlayerContact = false;

    // audio
    public AudioClip explosionAudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // make overlap circle to check for player to deal damage
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        foreach (Collider2D player in hitPlayer)
        {
            if (player.gameObject.CompareTag("Player") && !gotPlayerContact)
            {
                // play explosion audio
                AudioSource.PlayClipAtPoint(explosionAudio, transform.position);


            GameObject exp = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            GameObject smoke = Instantiate(smokeParticles, transform.position, Quaternion.identity);
            Destroy(exp, 2f);
            Destroy(smoke, 2f);

                //log
                Debug.Log("Hit player");
                gotPlayerContact = true;
                // deal damage to player
                PlayerStats playerStats = player.gameObject.GetComponent<PlayerStats>();
                playerStats.TakeDamage(30f);

                // get particle in this parent
                ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
                // stop particle from continuing
                foreach (ParticleSystem particle in particles)
                {
                    particle.Stop();
                }


                // detatch child in this parent
                transform.DetachChildren();
                
                
                

                
            Destroy(gameObject);
            }
        }
    }


    // destroy the bullet when it collides with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Grid") && collision.gameObject.name != "launchOffset")
        {
                // play explosion audio
                AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
            // instantiate explosion particles
            GameObject exp = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            GameObject smoke = Instantiate(smokeParticles, transform.position, Quaternion.identity);
            Destroy(exp, 2f);
            Destroy(smoke, 2f);


                // get particle in this parent
                ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
                // stop particle from continuing
                foreach (ParticleSystem particle in particles)
                {
                    particle.Stop();
                }


                // detatch child in this parent
                transform.DetachChildren();
                


            Destroy(gameObject);
        }
    }

    // on collision enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Destroyable") || collision.gameObject.CompareTag("Building"))
        {
                // play explosion audio
                AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
            // instantiate explosion particles
            GameObject exp = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            GameObject smoke = Instantiate(smokeParticles, transform.position, Quaternion.identity);

         /*   //
             void CmdDetachParticles()
     {
         // T$$anonymous$$s splits the particle off so it doesn't get deleted with the parent
         emit.transform.parent = null;
 
         // t$$anonymous$$s stops the particle from creating more bits
         emit.Stop();
     }

     

            CmdDetachParticles();*/



                // get particle in this parent
                ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
                // stop particle from continuing
                foreach (ParticleSystem particle in particles)
                {
                    particle.Stop();
                }


                // detatch child in this parent
                transform.DetachChildren();
                


            // destroy the particles
            Destroy(exp, 2f);
            Destroy(smoke, 2f);

            
        if (collision.gameObject.CompareTag("Destroyable")){

            // get the tilemap from the collision
            tilemap = collision.gameObject.GetComponent<Tilemap>();
            Debug.Log(tilemap);

            // destroy the cell that the bullet collided with
            int numContacts = collision.GetContacts(contacts);
            for (int i = 0; i < numContacts; i++)
            {
                Vector3Int cellPosition = tilemap.WorldToCell(contacts[i].point);
                tilemap.SetTile(cellPosition, null);
                //DestroyAdjacent(cellPosition);
            }
            }
        }

/*
        void DestroyAdjacent(Vector3Int cellPosition)
        {


            // destroy the cell to the left 4
            Vector3Int cellLeft = cellPosition - new Vector3Int(1, 0, 0);
            tilemap.SetTile(cellLeft, null);

            // destroy the cell to the right 6
            Vector3Int cellRight = cellPosition + new Vector3Int(1, 0, 0);
            tilemap.SetTile(cellRight, null);
            // destroy the bullet
            Destroy(gameObject);
        }
*/
        // destroy the bullet
        Destroy(gameObject);
    }
}
