using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour
{
    //boss prefab
    [SerializeField] GameObject bossPrefab;

    //where should the boss be spawned
    [SerializeField] GameObject bossSpawnpoint;

    //check if spawned variables
    bool bossHasSpawned = false;
    bool bossDefeated = false;

    //is the fight finished? (used to make the boss killed update happen once)
    bool finished = false;

    //boss UI
    [SerializeField] GameObject bossText;
    [SerializeField] Image healthBarGreen;
    [SerializeField] Image healthBorderBlue;
    [SerializeField] Image healthUnderRed;

    //boss
    EnemyController boss;


    // Start is called before the first frame update
    void Start()
    {
        //hide on start
        healthBarGreen.fillAmount = 0;
        healthBorderBlue.fillAmount = 0;
        healthUnderRed.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHasSpawned == true && bossDefeated == true && finished != true)
        {
            //update the round to say it ended, lowering the walls
            GameManager.Instance.UpdateRound();

            //hide hp bar and text
            healthBorderBlue.fillAmount = 0;
            healthUnderRed.fillAmount = 0;
            healthBarGreen.fillAmount = 0;
            bossText.SetActive(false);

            //finish
            finished = true;
        }

        //if the boss exists set the hp bar
        if(boss != null)
        {
            UpdateHealthBar(boss.GetCurrentHP(), boss.GetMaxHP());
        }

        //if the boss doesnt exist and it has been spawned already (it must have been defeated)
        if(boss == null && bossHasSpawned)
        {
            bossDefeated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //disable the trigger
        this.GetComponent<BoxCollider2D>().enabled = false;

        //spawn the boss
        Instantiate(bossPrefab, new Vector3(bossSpawnpoint.transform.position.x, bossSpawnpoint.transform.position.y, 0), Quaternion.identity);

        //show boss health bar (weird way of doing it,
        //setting progress to 0 to start, then 100 when it needs to be shown,
        //couldn't figure out how to get it to enable and disable ???)
        healthBorderBlue.fillAmount = 100;
        healthUnderRed.fillAmount = 100;
        bossText.SetActive(true);

        //get a reference to the boss
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyController>();

        //start the round
        GameManager.Instance.UpdateRound();
        bossHasSpawned = true;
    }

    public void UpdateHealthBar(float currentHp, float maxHp) //health bar from: 
    {
        //take the current and max hp
        //and set the healthbar to that %
        var hp = currentHp / maxHp;
        healthBarGreen.fillAmount = hp;
    }


}
