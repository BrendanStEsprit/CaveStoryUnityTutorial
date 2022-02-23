using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : Enemy
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Player player;
    float jumptimer=3;
    float timer;
    bool abletojump;
    // Start is called before the first frame update
    void Start()
    {
        abletojump = true;
        player = Player.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtPlayer();
        float distance = Vector2.Distance(player.transform.position, this.transform.position);
        if(distance<5)
        {
            
            
            Debug.Log("I Am Active");
            if (abletojump == true)
            {
                if ((player.transform.position.x > this.transform.position.x))
                {
                    rb.AddForce(new Vector2(2, 5), ForceMode2D.Impulse);
                    abletojump = false;
                    timer = 1;
                }

                if ((player.transform.position.x < this.transform.position.x))
                {
                    rb.AddForce(new Vector2(-2, 5), ForceMode2D.Impulse);
                    abletojump = false;
                    timer = 1;

                }

            }
            
        }
        
        

        animator.SetFloat("PlayerPosition", distance);
        animator.SetFloat("Velocity", rb.velocity.y);
    }
    void Update()
    {
        if(!abletojump)
        {
            
            timer -= Time.deltaTime;
            if(timer<0)
            {
                abletojump = true;
            }
            
        }
    }
    void LookAtPlayer()
    {
        spriteRenderer.flipX = (player.transform.position.x > this.transform.position.x);
    }

}
