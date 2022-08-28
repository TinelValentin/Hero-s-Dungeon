using UnityEngine;

public class Humanoid : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth = 20;
    public float pushRecovery = 0.2f;

    protected float imunity = 0.2f; //after being hit 
    protected float lastImunity;
    protected SpriteRenderer changeSprite;
    protected Color getHitColor;
    protected Color originalColor;
    protected Vector3 pushDirection;

    protected BoxCollider2D boxCollider;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        changeSprite = GetComponent<SpriteRenderer>();
        getHitColor = Color.red;
        originalColor = changeSprite.color;
    }

    protected virtual void ChangeColor()
    {
        changeSprite.material.color = originalColor;

    }

    protected void GetPushed(Vector2 pushVector)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, pushVector.y), Mathf.Abs(pushVector.y * Time.deltaTime * 3.5f), LayerMask.GetMask("Human", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, pushVector.y * Time.deltaTime, 0);

        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, pushVector.x), Mathf.Abs(pushVector.x * Time.deltaTime * 3.5f), LayerMask.GetMask("Human", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, pushVector.x * Time.deltaTime, 0);

        }
    }

    protected virtual void ReciveDamage(Damage damage)
    {
        if (Time.time - lastImunity > imunity)
        {

            currentHealth -= damage.damageNumber;
            lastImunity = Time.time;
            pushDirection = (transform.position - damage.damageOrigin).normalized;
            changeSprite.material.color = getHitColor;
            Invoke(nameof(ChangeColor), 0.3f);
            Vector2 pushVector = damage.pushForce * pushRecovery * Time.deltaTime * pushDirection;

            GetPushed(pushVector);
            if (currentHealth <= 0.0f)
                Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
