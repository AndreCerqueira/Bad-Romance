using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    // canon ball prefab
    public CannonBallBehaviour projectilePrefab;
    public GameObject cannonBallSpawnArea;
    public float cannonBallSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCannonBall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // cannon ball spawn ienumerator
    public IEnumerator SpawnCannonBall()
    {
        while (true)
        {

            // active animation Attack in all ships
            foreach (GameObject ship in GetAllShips())
            {
                ship.GetComponent<Animator>().SetTrigger("Attack");
            }

            /*
            // spawn cannon ball

            // spawn quantity random between 1 and 3
            int spawnQuantity = Random.Range(6, 9);
            // spawn cannon ball
            for (int i = 0; i < spawnQuantity; i++)
            {
                // spawn cannon ball
                CannonBallBehaviour cannonBall = Instantiate(projectilePrefab, GetCannonBallSpawnAreaPosition(), Quaternion.identity);
                // add force to cannon ball
                cannonBall.GetComponent<Rigidbody2D>().AddForce(Vector2.down * cannonBallSpeed);
                // wait for 0.5 seconds
                yield return new WaitForSeconds(0.5f);
            }*/

            yield return new WaitForSeconds(5f);
        }
    }


    public GameObject[] GetAllShips()
    {
        // get all ships
        GameObject[] ships = GameObject.FindGameObjectsWithTag("Ship");
        // return ships
        return ships;
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
        // return random position
        return new Vector3(cannonBallSpawnAreaPosition.x + randomPosition, cannonBallSpawnAreaPosition.y, cannonBallSpawnAreaPosition.z);
    }

}
