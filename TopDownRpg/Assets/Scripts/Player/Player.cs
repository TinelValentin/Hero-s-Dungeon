using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mob
{

    [SerializeField] private Transform projectile;
    [SerializeField] private Transform teleportAura;
    private float cooldownProjectile;
    private float lastProjectileTime;
    private float cooldownTeleport;
    private float lastTeleportTime;
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
        cooldownTeleport = 0.5f;
        lastTeleportTime = 0.0f;
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

        if (canMove)
            Movement(new Vector3(x, y, 0));

        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastProjectileTime > cooldownProjectile)
            {
                lastProjectileTime = Time.time;
                Shoot(mouseCoordinates);
            }

        }
        else

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Time.time - lastTeleportTime > cooldownTeleport)
            {
                canMove = false;
                lastTeleportTime = Time.time;
                Transform auraInstantiated = Instantiate(teleportAura, transform.position, Quaternion.identity);
                auraInstantiated.SetParent(this.transform);
                Destroy(auraInstantiated.gameObject, 0.5f);
                Invoke(nameof(ReEnableMovement), 0.2f);
                StartCoroutine(Blink(mouseCoordinates));

            }

        }
    }

    public void ReEnableMovement()
    {
        canMove = true;
    }

    protected override void ReciveDamage(Damage damage)
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

    private IEnumerator Blink(Vector3 mouseCoordinates)
    {
        yield return new WaitForSeconds(0.15f);
        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, -10);
        Vector3 direction = (mouseCoordinates - currentPosition).normalized;
        Vector3 newPosition = transform.position + 2.5f * new Vector3(direction.x, direction.y);
        //
        RaycastHit2D teleportHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, newPosition.y * Time.deltaTime), Mathf.Abs(newPosition.y * Time.deltaTime), LayerMask.GetMask("Human", "Blocking"));
        if (teleportHit.collider == null)
        {
            teleportHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(newPosition.x * Time.deltaTime, 0), Mathf.Abs(newPosition.x * Time.deltaTime), LayerMask.GetMask("Human", "Blocking"));
            if (teleportHit.collider == null)
            {
                transform.Translate(newPosition);

            }

        }

    }
}
