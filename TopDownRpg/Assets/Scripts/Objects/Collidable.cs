using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    protected BoxCollider2D colliderBase;
    protected BoxCollider2D[] hit = new BoxCollider2D[10];

    protected virtual void Start()
    {
        colliderBase = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        colliderBase.OverlapCollider(filter, hit);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i] != null)
                OnCollide(hit[i]);

            hit[i] = null;
        }
    }

    protected abstract void OnCollide(BoxCollider2D collide);
    
}