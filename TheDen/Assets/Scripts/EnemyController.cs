using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //movement variables
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    //health variables
    [SerializeField] int maxHp = 100;
    int currentHp;

    //damage
    [SerializeField] int damage;


    // Start is called before the first frame update
    void Start()
    {
        //set the rb
        rb = GetComponent<Rigidbody2D>();

        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        Move();
    }

    void Move()
    {
        //-1 = left, down
        //1 = right, up
        float movexX = -1;
        moveDirection = new Vector2(movexX, 0).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
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

}
