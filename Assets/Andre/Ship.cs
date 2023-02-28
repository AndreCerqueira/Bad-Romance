using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private CannonBallBehaviour projectilePrefab;
    private GameObject cannonBallSpawnArea;
    private float cannonBallSpeed;

    private GameObject cam;
    public float parallaxEffectY;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        // get variables from ShipManager
        projectilePrefab = GameObject.Find("ShipManager").GetComponent<ShipManager>().projectilePrefab;
        cannonBallSpawnArea = GameObject.Find("ShipManager").GetComponent<ShipManager>().cannonBallSpawnArea;
        cannonBallSpeed = GameObject.Find("ShipManager").GetComponent<ShipManager>().cannonBallSpeed;

        // get camera
        cam = Camera.main.gameObject;

        //log
        Debug.Log("Ship Start");

    }

    // Update is called once per frame

    void Update()
    {
        
       transform.position = new Vector3(transform.position.x, cam.transform.position.y * parallaxEffectY + offsetY, transform.position.z);
    }


    public void SpawnCannonBalls()
    {
        //log
        Debug.Log("SpawnCannonBalls");

        int spawnQuantity = Random.Range(1, 2);
        // spawn cannon ball
        for (int i = 0; i < spawnQuantity; i++)
        {
            // spawn cannon ball
            CannonBallBehaviour cannonBall = Instantiate(projectilePrefab, GetCannonBallSpawnAreaPosition(), Quaternion.identity);

            // add force to cannon ball
            cannonBall.GetComponent<Rigidbody2D>().AddForce(Vector2.down * cannonBallSpeed);
        }
    }


    // get cannon ball spawn area position random between min and max that are the length of the spawn area
    public Vector3 GetCannonBallSpawnAreaPosition()
    {
        // get cannon ball spawn area position
        Vector3 cannonBallSpawnAreaPosition = cannonBallSpawnArea.transform.position;
        // get cannon ball spawn area length
        float cannonBallSpawnAreaLength = cannonBallSpawnArea.transform.localScale.x;
        // get random position between min and max
        float randomPosition = Random.Range(-cannonBallSpawnAreaLength / 2, cannonBallSpawnAreaLength / 2);

        // random y position
        float randomYPosition = Random.Range(-cannonBallSpawnAreaLength / 2, cannonBallSpawnAreaLength / 2) + 30;

        // return random position
        return new Vector3(cannonBallSpawnAreaPosition.x + randomPosition, randomYPosition, cannonBallSpawnAreaPosition.z);
    }
}
