using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    protected virtual void Flip() //method used ot flip the character values
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
        //getting movement inputs
        ProcessInputs();

        //basic attacking
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BasicAttack();
        }
    }

    void FixedUpdate()
    {
        //physics calculations
        Move();
    }

    void ProcessInputs() //process player inputs
    {
        //get wasd inputs
        float movexX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //figure out what direction the player is move it
        //normalized to confine values in diagonal directions
        moveDirection = new Vector2(movexX, moveY).normalized;

        //change the way the player is facing
        if(movexX > 0 && isFacingLeft)
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

    void Move() //player movement
    {
        //set player velocity
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);


    }

    void BasicAttack() //player basic attack
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
    }

    void OnDrawGizmosSelected() //shows a circle with the range in the editor
    {
        if(attackPoint == null) //return just incase theres no attack point
            return;

        //draws the circle
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
