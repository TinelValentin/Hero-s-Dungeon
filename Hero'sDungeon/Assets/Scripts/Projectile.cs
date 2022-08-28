using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Projectile : Collidable
{
    public string enemyTag;
    GameObject projectile;


    protected float projectileSpeed;
    protected int damage;
    protected float pushForce;
    private string blockingProjectileTag = "blockingProjectile";
    private Vector3 origin;
    private Vector3 directionTarget;

    public void shootDirection(Vector3 direction)
    {
        directionTarget = direction;
    }

    protected override void Start()
    {
        base.Start();
        projectileSpeed = 10.0f;
        damage = 3;
        pushForce = 0.1f;
        origin = transform.position;
        CalculateAngle();

    }

    protected void CalculateAngle()
    {


        float angle = Mathf.Atan2(directionTarget.y, directionTarget.x) * Mathf.Rad2Deg - 90;


        transform.eulerAngles = Vector3.forward * angle;
    }


    protected override void Update()
    {

        base.Update();
        transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position + directionTarget, projectileSpeed * Time.deltaTime);
        Destroy(gameObject, 3.0f);
    }
    protected override void OnCollide(BoxCollider2D collide)
    {
        if (collide.CompareTag(enemyTag))
        {
            Damage projectileDamage = new Damage(damage, pushForce, origin);
            collide.SendMessage("reciveDamage", projectileDamage);
            Destroy(gameObject);
        }
        if (collide.CompareTag(blockingProjectileTag))
        {
            
            Destroy(gameObject);
        }

    }


}
