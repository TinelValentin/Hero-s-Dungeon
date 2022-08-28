using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Collidable
{
    public int damage;
    public int cooldown;
    public Transform target;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, 10f);
    }

    protected override void OnCollide(BoxCollider2D collide)
    {
        if (collide.CompareTag("Player"))
        {
            Damage sendDamage = new Damage(damage, 0, transform.position);
            target.SendMessage("ReciveDamage", sendDamage);
        }
    }
}
