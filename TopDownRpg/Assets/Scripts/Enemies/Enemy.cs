using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mob
{
    public Vector3 startingPosition;

    protected Transform player;
    protected Vector3 destination;
    protected bool chase;
    protected bool collidingPlayer;

    protected float chaseSpeed = 2f;
    protected readonly int damageOnHit = 4;
    protected readonly float pushForceOnHit = 50.0f;


    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("player").GetComponent<Transform>();
        destination = player.position;
        xSpeed = 1.1f;
        ySpeed = 0.9f;
        collidingPlayer = false;
    }

    protected virtual bool ChaseCondition()
    {
        return (destination - transform.position).sqrMagnitude < 10.0f;
            
    }

    protected void MovePattern()
    {
        if (ChaseCondition())
            chase = true;
        if (chase && !collidingPlayer)
        {
            if ((destination - transform.position).normalized.x > 0.01)
                transform.localScale = Vector3.one;
            else if ((destination - transform.position).normalized.x <= 0.01)
                transform.localScale = new Vector3(-1, 1, 1);
            if ((destination - transform.position).normalized.y <= 0)
                changeSprite.sprite = frontSprite;
            else if ((destination - transform.position).normalized.y > 0)
                changeSprite.sprite = BackSprite;
            //Vector3 movePosition = Vector3.Slerp(transform.position, player.position, chaseSpeed * Time.fixedDeltaTime);
            MoveToPlayer();
        }
    }

    protected virtual void FixedUpdate()
    {
        MovePattern();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collidingPlayer = true;
            DealDamage();
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) collidingPlayer = false;
    }

    protected virtual void MoveToPlayer()
    {

        Vector3 direction = destination - transform.position;
        if (direction.x < 0.0f)
        {
            if (direction.y < 0.0f)
            {
                Movement(new Vector3(-0.5f, -0.5f, 0));
            }
            else
                if (direction.y > 0.0f)
            {
                Movement(new Vector3(-0.5f, 0.5f, 0));
            }
            else
                Movement(new Vector3(-0.5f, 0.0f, 0));
        }
        else
        if (direction.x > 0.0f)
        {
            if (direction.y < 0.0f)
            {
                Movement(new Vector3(0.5f, -0.5f, 0));
            }
            else
                if (direction.y > 0.0f)
            {
                Movement(new Vector3(0.5f, 0.5f, 0));
            }
            else
                Movement(new Vector3(0.5f, 0.0f, 0));
        }
        else
            Movement(new Vector3(0.0f, 0.5f, 0));
    }

    protected void DealDamage()
    {
        Damage damage = new Damage(damageOnHit, pushForceOnHit, transform.position);
        player.SendMessage("ReciveDamage", damage);


    }

    protected override void Death()
    {
        base.Death();
        GameObject coinsObject = GameObject.Find("Coins");
        Money coins = coinsObject.GetComponent<Money>();
        coins.SendMessage("AddCurrency", 10);

    }
}
