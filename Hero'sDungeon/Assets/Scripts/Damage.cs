using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage 
{
    public int damageNumber;
    public float pushForce;
    public Vector3 damageOrigin;

    public Damage(int damage, float push, Vector3 origin)
    {
        this.damageNumber = damage;
        this.pushForce = push;
        this.damageOrigin = origin;
    }
  
}
