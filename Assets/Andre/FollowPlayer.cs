using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float offset_x;
    public Transform player;

    public float lerp_value;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        offset_x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x + offset_x, transform.position.y, transform.position.z), lerp_value);
    }
}
