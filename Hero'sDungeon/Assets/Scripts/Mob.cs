using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Mob : Fighter
{
    protected Rigidbody2D body;
    public Sprite frontSprite;
    public Sprite BackSprite;
   
   
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float xSpeed = 1.0f;
    protected float ySpeed = 0.6f;
    

    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
        changeSprite = gameObject.GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
       
    }


    

    protected virtual void Movement(Vector3 input)
    {
        moveDelta = new Vector3(input.x*xSpeed* 2.5f, input.y*ySpeed* 2.5f, 0);

       
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * 3.5f), LayerMask.GetMask("Human", "Blocking"));
       if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
           
        }


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime *3.5f ), LayerMask.GetMask("Human", "Blocking"));
        if (hit.collider == null)
        {
           transform.Translate(moveDelta.x * Time.deltaTime , 0, 0);
        }





    }
}

