using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatTrigger : MonoBehaviour
{
    
    //location of empty object to move the camera too, also holds the spawner
    [SerializeField] GameObject locationObject; //spawner/cam loc
    Transform locationToMove;

    //number of enemies this trigger spawns
    [SerializeField] int killsNeeded;


    // Start is called before the first frame update
    void Start()
    {
        locationToMove = locationObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when the trigger is entered move the camera
        Vector3 newPos = new Vector3(locationToMove.position.x, locationToMove.position.y, -1);
        CameraController.Instance.UpdatePos(newPos);

        //disable the trigger
        this.GetComponent<BoxCollider2D>().enabled = false;

        //update that the round started
        GameManager.Instance.UpdateRound();

        //update number of enemies that need to be killed
        GameManager.Instance.SetToKill(killsNeeded);

        //Debug.Log("Calling Started");

        //start the round
        locationObject.GetComponent<Spawner>().StartRound(5);

    }

}
