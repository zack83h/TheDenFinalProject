using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool startingDirection = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = rb = GetComponent<Rigidbody2D>();
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

}
