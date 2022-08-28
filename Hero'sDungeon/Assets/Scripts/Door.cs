using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Collidable
{

    private Transform player;
    public Vector3 positionToNextRoom;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("player").GetComponent<Transform>();
        
       
    }


    protected override void OnCollide(BoxCollider2D collide)
    {
       
       
        if (collide.gameObject.tag == "Player" || collide.gameObject.tag=="Weapon")
        {
            player.position = transform.position + positionToNextRoom;
        }
    }
    
}
