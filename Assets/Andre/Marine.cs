using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marine : MonoBehaviour
{
    public BulletBehaviour projectilePrefab;
    public Transform launchOffset;
    public bool isShooting = false;
    public bool playerInRange = false;
    float timeToStopShooting = 3f;

    // header
    [Header("Fall")]
    public bool isGrounded = false;
    public float fallingTime = 0f;
    public float fallingTimeToDie = 5f;

// puff particle
    public GameObject puffParticle;

    // header
    [Header("Particles")]
    public GameObject bangParticle;
    public Vector3 bangOffset;
    public Vector3 bangRotation;
    public bool bangDestroy;
    public float bangDestroyTime;
    // bang audio
    public AudioClip bangAudio;

    // Start is called before the first frame update
    void Start()
    {
            StartCoroutine(Shoot());
        
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            timeToStopShooting = 3f;
            GetComponent<Animator>().SetBool("isAttacking", true);
        }


        if (!isGrounded)
        {
            fallingTime += Time.deltaTime;
            if (fallingTime >= fallingTimeToDie)
            {
                // destroy marine if it falls from high place
                StartCoroutine(DieAfterFall());
            }
        }
        else
        {
            fallingTime = 0f;
        }
    }

// public PlayPuffParticle();
    public void PlayPuffParticle()
    {
        GameObject puff = Instantiate(puffParticle, launchOffset.position, launchOffset.rotation);
        Destroy(puff, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Destroyable") || collision.gameObject.CompareTag("Building"))
        {
            // destroy marine if it falls from high place
            isGrounded = true;
        }
        {
            // destroy marine if it falls from high place
            isGrounded = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Destroyable") || collision.gameObject.CompareTag("Building"))
        {
            isGrounded = false;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map") || collision.gameObject.CompareTag("Destroyable") || collision.gameObject.CompareTag("Building"))
        {
            isGrounded = true;
        }
    }

    // void play bang audio
    public void PlayBangAudio()
    {
        AudioSource.PlayClipAtPoint(bangAudio, transform.position);
    }

    
    void LaunchProjectile()
    {

        BulletBehaviour projectile = Instantiate(projectilePrefab, launchOffset.position, launchOffset.rotation);

        // play bang audio

// active particle effect
        // GameObject bang = Instantiate(bangParticle, launchOffset.position + bangOffset, launchOffset.rotation * Quaternion.Euler(bangRotation));
        // Destroy(bang, 0.5f);

        // bullet lifetime 
        // if (bangDestroy)
        //     Destroy(bang, bangDestroyTime);

        Destroy(projectile.gameObject, 4f);
        // if flipX is true, then the projectile will be launched to the left
        if (GetComponent<SpriteRenderer>().flipX)
        {
            projectile.transform.Rotate(0, 180, 0);
        }

        GetComponent<Animator>().SetBool("isAttacking", false);
    }


    // check if player enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            FacePlayer();
        }
    }


    // trigger stays on the player
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            FacePlayer();
        }
    }


    // check if player exits the trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    // function that rotates the marine to face the player
    void FacePlayer()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 marinePos = transform.position;
        
        GetComponent<SpriteRenderer>().flipX = !(playerPos.x < marinePos.x);

        // change the bang offset
        if (GetComponent<SpriteRenderer>().flipX)
        {
            bangOffset = new Vector3(0.7f, 0, 0);
            bangRotation = new Vector3(0, 0, -90);
        }
        else
        {
            bangOffset = new Vector3(-0.7f, 0, 0);
            bangRotation = new Vector3(0, 0, 90);
        }

    } 


    // ienumerator to die after some fall
    IEnumerator DieAfterFall()
    {
        while (!isGrounded)
        {
            // wait for fixed update
            yield return new WaitForFixedUpdate();
        }

        // play puff particle
        GameObject puff = Instantiate(puffParticle, transform.position, transform.rotation);
        Destroy(puff, 2f);

        Destroy(gameObject);
    }


    // ienumerator to shoot the projectile
    private bool isReloading = false;
    IEnumerator Shoot()
    {
        while (true)
        {
            if (!playerInRange)
            {
                // yield return fixed update
                yield return new WaitForFixedUpdate();
                continue;
            }

            if (isShooting)
            {
                // yield return fixed update
                yield return new WaitForFixedUpdate();
                continue;
            }
            isShooting = true;


            while (isShooting)
            {
                yield return new WaitForSeconds(0.75f);

                // after some time isShooting will be false
                // log timeToStopShooting
                timeToStopShooting -= 0.5f;
                if (timeToStopShooting <= 0)
                {
                    isShooting = false;
                }
                else {
                    // bool attack animation
                    GetComponent<Animator>().SetBool("isAttacking", true);
                // LaunchProjectile();
                }
            }
        }
    }
}
