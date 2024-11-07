using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    ////which trigger are we working with/what one is the player going to trigger first
    //[SerializeField] CombatTrigger currentTrigger;
    
    //is the current round going
    bool roundStarted = false;

    //how many enemies need to die for the round to end
    int toKill;

    //score, amount and textbox
    int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;



    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        //if the round was started put up the walls
        if (roundStarted)
        {
            ToggleableWalls.Instance.GetComponent<TilemapRenderer>().enabled = true;
            ToggleableWalls.Instance.GetComponent<TilemapCollider2D>().enabled = true;
        }
        else //when a round is not taken place (enemies are not spawning)
        {
            ToggleableWalls.Instance.GetComponent<TilemapRenderer>().enabled = false;
            ToggleableWalls.Instance.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }

    public void UpdateRound() //change whether or not a round was started
    {
        if (roundStarted) roundStarted = false;
        else roundStarted = true;
    }

    public void SetToKill(int kills) //set how many enemies need to be killed
    {
        toKill = kills;
    }

    public void DecreaseKills() //decrease number of enemies left to kill, check if the round should end/reset camera
    {
        toKill -= 1;

        //when all enemies are dead
        if (toKill == 0)
        {
            //end the round
            UpdateRound();
            //reset the camera
            CameraController.Instance.UpdateTargeting();
        }

        //Debug.Log("To kill: " + toKill);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

}

