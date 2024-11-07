using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    private Transform player; //player transform
    public Transform target; //TO DO, camera moves when entering a certain area
    bool isTargeting = false;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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


        if (!isTargeting)
        {
            //move the camera with the player
            Vector3 connectedPos = new Vector3(player.position.x, player.position.y, -1);
            transform.position = connectedPos;
        }
    }

    public void UpdatePos(Vector3 areaPos)
    {
        //set the camera position to the new location
        transform.position = areaPos;
        UpdateTargeting();
    }

    public void UpdateTargeting()
    {
        if (isTargeting) isTargeting = false;
        else isTargeting = true;
    }

}
