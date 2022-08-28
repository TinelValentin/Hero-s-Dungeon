using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mob
{
    public Vector3 startingPosition;

    protected Transform player;
    protected bool chase;
    protected bool collidingPlayer;

    protected float chaseSpeed = 2f;
    private readonly int damageOnHit = 4;
    private readonly float pushForceOnHit = 50.0f;


    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("player").GetComponent<Transform>();
        xSpeed = 0.9f;
        ySpeed = 0.7f;
        collidingPlayer = false;
    }

    protected override void reciveDamage(Damage damage)
    {
        base.reciveDamage(damage);
    }

    protected virtual void FixedUpdate()
    {
        if ((player.position - transform.position).sqrMagnitude < 10.0f)
            chase = true;
        if (chase && !collidingPlayer)
        {
            if ((player.position - transform.position).normalized.x > 0)
                transform.localScale = Vector3.one;
            else if ((player.position - transform.position).normalized.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            if ((player.position - transform.position).normalized.y < 0)
                changeSprite.sprite = frontSprite;
            else if ((player.position - transform.position).normalized.y > 0)
                changeSprite.sprite = BackSprite;
            Vector3 movePosition = Vector3.Slerp(transform.position, player.position, chaseSpeed * Time.fixedDeltaTime);
            body.MovePosition(movePosition);
        }

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

        Vector3 direction = player.position - transform.position;
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

    private void DealDamage()
    {
        Damage damage = new Damage(damageOnHit, pushForceOnHit, transform.position);
        player.SendMessage("reciveDamage", damage);


    }

    protected override void Death()
    {
        base.Death();
        GameObject coinsObject = GameObject.Find("Coins");
        Money coins = coinsObject.GetComponent<Money>();
        coins.SendMessage("AddCurrency", 10);

    }
}
