using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private Transform egg;
    Vector3 anticipationMovementRight = new Vector3(10, 0, 0);
    Vector3 anticipationMovementLeft = new Vector3(-10, 0, 0);
    Vector3 anticipationMovementUp = new Vector3(0, 10, 0);
    Vector3 anticipationMovementDown = new Vector3(0, -10, 0);

    /// <summary>
    /// 1-Right
    /// 2-Down
    /// 3-Left
    /// 4-Up
    /// </summary>
    private int direction = 1;
    float layEggCooldown = 8.0f;
    float lastEggTime = 5.0f;

    private void RandomNumber()
    {
        direction = Random.Range(1, 5);
    }

    protected bool CanMove(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed * 2.5f, input.y * ySpeed * 2.5f, 0);


        RaycastHit2D hitApprove = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * 3.5f), LayerMask.GetMask("Human", "Blocking"));
        if (hitApprove.collider == null)
        {
            hitApprove = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime * 3.5f), LayerMask.GetMask("Human", "Blocking"));
            if (hitApprove.collider == null)
            {
                return true;
            }

        }
        return false;

    }

    private void LayEgg()
    {
        Instantiate(egg, transform.position, Quaternion.identity);
   
    }

    protected override void FixedUpdate()
    {
        switch (direction)
        {
            case 1:
                Vector3 moveRight = new Vector3(1.4f, 0, 0);
                if (CanMove(moveRight + anticipationMovementRight))
                {
                    transform.localScale = Vector3.one;
                    Movement(moveRight);
                }
                else
                {
                    RandomNumber();
                }
                break;

            case 2:
                Vector3 moveDown = new Vector3(0, -1.4f, 0);
                if (CanMove(moveDown + anticipationMovementDown))
                {
                    Movement(moveDown);
                }
                else
                {
                    RandomNumber();
                }
                break;

            case 3:
                Vector3 moveLeft = new Vector3(-1.4f, 0, 0);
                if (CanMove(moveLeft + anticipationMovementLeft))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Movement(moveLeft);
                }
                else
                {
                    RandomNumber();
                }
                break;

            case 4:
                Vector3 moveUp = new Vector3(0, 1.4f, 0);
                if (CanMove(moveUp + anticipationMovementUp))
                {
                    Movement(moveUp);
                }
                else
                {
                    RandomNumber();
                }
                break;
        }

        if (Time.time - lastEggTime > layEggCooldown)
        {
            lastEggTime = Time.time;
            LayEgg();
        }
    }


}
