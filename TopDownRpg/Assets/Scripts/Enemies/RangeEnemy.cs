using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{

    [SerializeField] protected Transform arrow;
    float elapsed = 0f;
    private readonly float distanceTraveledFromPlayer = 0.1f;

    protected override void Start()
    {
        base.Start();


    }

    protected virtual void Shoot()
    {
        Transform arrowInstantiated = Instantiate(arrow, transform.position, Quaternion.identity);
        Vector3 shootDirection = (player.position - transform.position).normalized;
        arrowInstantiated.GetComponent<Projectile>().shootDirection(shootDirection);
    }

    protected override void MoveToPlayer()
    {
        Vector3 direction = player.position - transform.position;
        if (direction.x < 0.0f)
        {
            if (direction.y < 0.0f)
            {
                Movement(new Vector3(distanceTraveledFromPlayer, distanceTraveledFromPlayer, 0));
            }
            else
                if (direction.y > 0.0f)
            {
                Movement(new Vector3(distanceTraveledFromPlayer, -distanceTraveledFromPlayer, 0));
            }
            else
                Movement(new Vector3(distanceTraveledFromPlayer, 0, 0));
        }
        else
        if (direction.x > 0.0f)
        {
            if (direction.y < 0.0f)
            {
                Movement(new Vector3(-distanceTraveledFromPlayer, distanceTraveledFromPlayer, 0));
            }
            else
                if (direction.y > 0.0f)
            {
                Movement(new Vector3(-distanceTraveledFromPlayer, -distanceTraveledFromPlayer, 0));
            }
            else
                Movement(new Vector3(-distanceTraveledFromPlayer, 0, 0));
        }
        else
            Movement(new Vector3(0, -distanceTraveledFromPlayer, 0));

    }
    protected new void FixedUpdate()
    {

        if ((player.position - transform.position).sqrMagnitude < 30.0f)
            chase = true;
        else
        {
            chase = false;
        }
        if (chase && !collidingPlayer)
        {
            elapsed += Time.deltaTime;
            if ((player.position - transform.position).normalized.x > 0)
                transform.localScale = Vector3.one;
            else if ((player.position - transform.position).normalized.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            if ((player.position - transform.position).normalized.y < 0)
                changeSprite.sprite = frontSprite;
            else if ((player.position - transform.position).normalized.y > 0)
                changeSprite.sprite = BackSprite;
            MoveToPlayer();
        }

        if (elapsed >= 1f)
        {
            elapsed %= 1f;
            Shoot();
        }

    }

}
