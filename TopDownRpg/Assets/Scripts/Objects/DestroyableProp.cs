using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableProp : Collidable
{
    public uint objectHealth = 10;
    private readonly float imunity = 0.5f;
    private float lastTime = 0.0f;


    override protected void OnCollide(BoxCollider2D collide)
    {
       if(Time.time-lastTime>imunity)
        {
            lastTime = Time.time;
            if (collide.CompareTag("weapon"))
            {
                if (objectHealth > 0)
                    objectHealth--;
                else
                    Destroy(gameObject);

            }
        }
        

    }
}
