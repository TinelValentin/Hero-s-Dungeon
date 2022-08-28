using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{

    [SerializeField] private Transform projectile;
    private float cooldownProjectile;
    private float lastProjectileTime;
    private PlayerHealthDisplay healthDisplay;

    private bool canMove = true;
    private void ChangeDirection(Vector3 direction)
    {

        if (transform.position.x - direction.x > 0)
            transform.localScale = Vector3.one;
        else if (transform.position.x - direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (transform.position.y - direction.y > 0)
            changeSprite.sprite = frontSprite;
        else if (transform.position.y - direction.y < 0)
            changeSprite.sprite = BackSprite;
    }


    protected override void Start()
    {
        base.Start();
        xSpeed = 1.2f;
        ySpeed = 0.9f;
        cooldownProjectile = 0.5f;
        lastProjectileTime = 0.0f;
        GameObject playerObject = GameObject.Find("UI");
        healthDisplay = playerObject.GetComponent<PlayerHealthDisplay>();
    }
    protected virtual void Shoot(Vector3 mouseCoordinates)
    {
        Transform arrowInstantiated = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector3 shootDirection = (mouseCoordinates - transform.position).normalized;
        arrowInstantiated.GetComponent<Projectile>().shootDirection(shootDirection);
    }

    private void FixedUpdate()
    {
        
        Vector3 mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ChangeDirection(mouseCoordinates);
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(canMove)
        Movement(new Vector3(x, y, 0));

        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastProjectileTime > cooldownProjectile)
            {
                lastProjectileTime = Time.time;
                Shoot(mouseCoordinates);
            }

        }

        if (Input.GetKey(KeyCode.E))
        {
            if (Time.time - lastProjectileTime > cooldownProjectile)
            {
                canMove = false;
                lastProjectileTime = Time.time;
                Blink(mouseCoordinates);
                Invoke(nameof(reEnableMovement), 0.2f);
            }

        }
    }

    public void reEnableMovement()
    {
        canMove = true;
    }

    protected override void reciveDamage(Damage damage)
    {
        if (Time.time - lastImunity > imunity)
        {

            currentHealth -= damage.damageNumber;
            healthDisplay.SendMessage("SetCurrentHealth", currentHealth);
            lastImunity = Time.time;
            pushDirection = (transform.position - damage.damageOrigin).normalized;
            changeSprite.material.color = getHitColor;
            Invoke(nameof(ChangeColor), 0.5f);
            Vector2 pushVector = damage.pushForce * pushRecovery * Time.deltaTime * pushDirection;
            GetPushed(pushVector);

            if (currentHealth <= 0.0f)
                Death();
        }
    }

    private void Blink(Vector3 mouseCoordinates)
    {
        Vector3 direction = (mouseCoordinates - transform.position).normalized;
        transform.position = new Vector2(mouseCoordinates.x,mouseCoordinates.y);
    }
}
