using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    //Spawn Rate
    [SerializeField] float spawnRate = 2f;
    //enemy prefab
    [SerializeField] GameObject enemyPrefab;

    //how many enemies should be spawned
    int counter;

    //spawning bounds
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    //timer
    float timer;


    ////array of enemies to spawn (5 in this case)
    //EnemyController[] toSpawn = new EnemyController[5];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        //after 2 seconds spawn an enemy and decrease the number 
        //of enemies that still need to spawn
        if (timer <= 0 && counter > 0)
        {
            //Debug.Log("Counter:" + counter);
            counter--;
            SpawnEnemy();
            timer = spawnRate;
        }

    }

    public void NumberToSpawn(int num) //set how many enemies should spawn that round
    {
        counter = num;
    }

    public void StartRound(int toSpawn) //start the round and set the bounds
    {
        counter = toSpawn;

        timer = 1f;
        //Debug.Log("Starting round");


        // left and right sides of the screen
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x;
        
        // top and bottom sides of the screen - .3 and .7 used so they cant spawn out of bounds
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, .3f, 0)).y;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, .7f, 0)).y;
    }

    void SpawnEnemy()
    {
        //Debug.Log("Spawning");
        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);
        Instantiate(enemyPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
}
