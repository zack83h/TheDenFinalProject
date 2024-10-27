using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; //player transform
    public Transform target; //TO DO, camera moves when entering a certain area

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        //move the camera with whatever its connected to
        Vector3 connectedPos = new Vector3(player.position.x, player.position.y, -1);
        transform.position = connectedPos;
    }
}
