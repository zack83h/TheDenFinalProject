using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //movement variables
    [SerializeField] float moveSpeed;
    public GameObject player;
    private Rigidbody2D rb;
    //private float distance;
    //private Vector2 moveDirection;

    //health variables
    [SerializeField] int maxHp = 100;
    int currentHp;

    //damage
    [SerializeField] int damage;

    //freeze on hit timer
    float currentFreezeCd = -1f;
    [SerializeField] float freezeTimer = 0.5f;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")
;    }


    // Start is called before the first frame update
    void Start()
    {
        //set the rb
        rb = GetComponent<Rigidbody2D>();

        currentHp = maxHp;
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }

    // Update is called once per frame
    void Update()
    {
        //movement
        //Move();


        //if the timer is going, decrease it
        if (currentFreezeCd > 0)
        {
            currentFreezeCd -= Time.deltaTime;
        }

        //when the timer ends, unfreeze the enemy
        if (currentFreezeCd < 0)
        {
            Move();
        }
    }

    void Move()
    {

        //-Old movement System-
        //-1 = left, down
        //1 = right, up
        //float movexX = -1;
        //moveDirection = new Vector2(movexX, 0).normalized;
        //rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        //new movement system: following the player


        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        //subtract the damage from the enemy
        currentHp -= damage;
        Debug.Log(name + " took " + damage + " damage.");
        Debug.Log(name + " CurrentHp: " + currentHp);

        //kill the enemy is if hits 0 hp
        if (currentHp <= 0)
        {
            Debug.Log(name + " died.");
            Die();
        }

        StopEnemy();
    }

    //when called, destroy the enemy
    void Die()
    {
        //TO DO: death animation, diable enemy instead of destroying

        //update score
        GameManager.Instance.IncreaseScore(5);

        //decrease kill amount
        GameManager.Instance.DecreaseKills();

        //delete the actor
        Destroy(gameObject);
    }


    //checking for collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if colliding with the player deal damage
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DamagePlayer(damage);
        }
    }

    void StopEnemy()
    {
        currentFreezeCd = freezeTimer;
    }


}
