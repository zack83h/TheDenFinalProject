using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //movement variables
    [SerializeField] float moveSpeed; //player movespeed
    private Vector2 moveDirection; //player movement direction
    private Rigidbody2D rb;

    //change sprite facing
    public bool isFacingLeft;
    public bool isGrounded;
    public bool isJumping;
    public bool spawnFacingLeft; //spawn direction
    private Vector2 facingLeft; //position at local scale to flip the character right to left

    //attack veriables
    [SerializeField] LayerMask enemyLayers; //layer containing all the enemies
    [SerializeField] Transform attackPoint; //center attack point for the weapon
    [SerializeField] float attackRange; //range of the attack, radius of attack
    [SerializeField] int attackDamage; //damage odf attack
    [SerializeField] GameObject weapon; //weapon sprite

    //attack cooldown variables
    float currentCd = 0f;
    [SerializeField] float attackSpeed = 1f;

    //player health
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;

    //game over
    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //setting up rigidbody
        rb = GetComponent<Rigidbody2D>();
        Vector3 location = transform.position;
        Vector3 scale = transform.localScale;

        //set it so the player is facing left on spawn
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        if(spawnFacingLeft)
        {
            transform.localScale = facingLeft;
            isFacingLeft = true;
        }

        //set starting hp
        currentHp = maxHp;

    }

    protected virtual void Flip() //method used ot flip the character values https://www.youtube.com/watch?v=dlYoy4galr4 | video used to implement 
    {
        if(isFacingLeft) //facing left
        {
            transform.localScale = facingLeft; //make player face left
        }

        if(!isFacingLeft) //facing right
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); //make player face right
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            //getting movement inputs
            ProcessInputs();

            if (currentCd > 0)
            {
                currentCd -= Time.deltaTime;
            }

            if (currentCd <= 0)
            {
                weapon.GetComponent<SpriteRenderer>().enabled = false;
            }

            //basic attacking
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (currentCd <= 0)
                {
                    currentCd = attackSpeed;
                    BasicAttack();
                }
            }
        }
    }

    void FixedUpdate()
    {
        //physics calculations
        if(!isGameOver)
            Move();
    }

    void ProcessInputs() //process player inputs: https://www.youtube.com/watch?v=Cry7FOHZGN4
    {
        //get wasd inputs
        float movexX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //figure out what direction the player is move it
        //normalized to confine values in diagonal directions
        moveDirection = new Vector2(movexX, moveY).normalized;

        //change the way the player is facing https://www.youtube.com/watch?v=dlYoy4galr4
        if (movexX > 0 && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
        }

        if (movexX < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
        }
    }

    void Move() //player movement: https://www.youtube.com/watch?v=Cry7FOHZGN4 (using velocity instead of position)
    {
        //set player velocity
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void BasicAttack() //player basic attack https://www.youtube.com/watch?v=sPiVz1k-fEs | most combat related stuff from this video
    {
        //Detect enemies in range of attack
        //apply damage to the enemies
        //maybe add animation later on

        //detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage enemies in range
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(25);
        }

        //show the sword to show its damaging
        weapon.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnDrawGizmosSelected() //shows a circle with the range in the editor
    {
        if(attackPoint == null) //return just incase theres no attack point
            return;

        //draws the circle
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    internal void DamagePlayer(int damage)
    {
        //update the hp, -= allows for healing and damage
        //send in positive number for damage and negative for healing
        currentHp -= damage;

        //if they heal above max, set to max
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        //if they hit <= 0 hp, die
        if(currentHp <= 0)
        {
            Die();
        }

        //update the UI
        UIManagerHealth.Instance.UpdateHealthBar(currentHp,maxHp);
    }
    void Die()
    {
        Debug.Log("Died");

        //start game over
        GameManager.Instance.InitiateGameOver();
        isGameOver = true;


        //hide the player
        GetComponent<SpriteRenderer>().enabled = false;
        weapon.GetComponent<SpriteRenderer>().enabled = false;

    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public int GetCurrentHp()
    {
        return currentHp;
    }

}
