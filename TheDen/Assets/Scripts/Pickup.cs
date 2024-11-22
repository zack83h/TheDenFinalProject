using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //healing stats
    [SerializeField] int healingAmount;

    //get needed components
    private CapsuleCollider2D rb;
    public GameObject player;

    //audio
    [SerializeField] AudioClip pickupSound;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if colliding with the player heal them
        if (collision.gameObject.tag == "Player")
        {
            //negative so that it heals the player instead of taking damage
            collision.gameObject.GetComponent<PlayerController>().DamagePlayer(-healingAmount);
        }

        AudioSource.PlayClipAtPoint(pickupSound, player.transform.position, .5f);

        Destroy(gameObject);
    }

}
