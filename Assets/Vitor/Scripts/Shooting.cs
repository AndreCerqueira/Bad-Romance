using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootingPoint;
    public float bulletSpeed = 5f;
    public GameObject bullet;

    private bool canShoot = true;
    private Animator animator;


    void Start()
    {
     // get animator component
    animator = GetComponent<Animator>();
    animator.SetBool("canShoot", true);

    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)))
    {
        // trigger attack animation
        animator.SetTrigger("Attack");
        // start shooting coroutine
        StartCoroutine(Shoot());
    }
        
    }

    IEnumerator Shoot()
{
    canShoot = false; // set flag to false to prevent shooting again

    // set animation canShoot to false
    animator.SetBool("canShoot", false);

    // wait for 0.5 seconds
    yield return new WaitForSeconds(0.4f);
    Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
    yield return new WaitForSeconds(0.5f); // wait for 0.5 seconds
    canShoot = true; // set flag to true to allow shooting again

    animator.SetBool("canShoot", true);
}
}
