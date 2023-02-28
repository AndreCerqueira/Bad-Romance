using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScene : MonoBehaviour
{
    bool waitTime = false;
    // Start is called before the first frame update
    void Start()
    {
        // wait for 3 seconds then change wiatTime to true
        StartCoroutine(WaitTime());
    }

    // Update is called once per frame
    void Update()
    {
        // Press Any Key to go to scene 1
        if (waitTime && Input.anyKeyDown)
        {
            // load current level
            GameProgress.LoadCurrentLevel();
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1f);
        waitTime = true;
    }

}
