using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Play_Intro : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
          videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
