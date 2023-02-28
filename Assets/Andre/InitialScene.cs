using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Press Any Key to go to scene 1
        if (Input.anyKeyDown)
        {
            // load scene 1
            UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
        }
    }
}
