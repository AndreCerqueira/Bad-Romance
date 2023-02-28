using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // currentHealth
    public float currentHealth = 100f;
    // maxHealth
    public float maxHealth = 100f;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    // take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (isDead)
                return;

            // die
            // play death animation
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<Animator>().SetBool("isDead", true);
            isDead = true;
            // disable player movement
            GetComponent<PlayerController>().enabled = false;


            // disable player controller
            GetComponent<PlayerController>().enabled = false;
            GetComponent<Shooting>().enabled = false;
            GetComponent<PlayerStats>().enabled = false;
            GetComponent<PlayerStats>().enabled = false;

            // freeze movement
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            // load lose scene
            StartCoroutine(WaitAndLoad());

            // change scene to game over

        }
        // update health slider in game manager
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateHealthSlider();
    }

    // ienumerator with 2secs UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScene");
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoseScene");
    }

}
