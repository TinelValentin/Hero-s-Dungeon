using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : Collidable
{
    //weapon attributes
    private readonly int damage = 10;
    private readonly float pushForce = 2.0f;
    private  int currentWeapon = 0;
    private SpriteRenderer curentWeaponAppearance;


    //weapon swing attributes
    private float cooldown = 0.4f;
    private float lastSwingTime = 0.0f;
    private Animator animatorControler;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        curentWeaponAppearance = GetComponent<SpriteRenderer>();
        animatorControler = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        changeDirection(mouseCoordinates);
        if (Input.GetMouseButtonDown(0))
        if (Time.time - lastSwingTime > cooldown)
        {
            lastSwingTime = Time.time;
            Swing();
        }
    }
    protected override void OnCollide(BoxCollider2D collide)
    {
        if(collide.tag == "enemy")
        {
            Damage dealDamage = new Damage(damage, pushForce, transform.position);

            collide.SendMessage("ReciveDamage", dealDamage);
        }
    }

    private void Swing()
    {
        animatorControler.SetTrigger("Swing");
    }

    private void changeDirection(Vector3 direction)
    {

        if (transform.position.x - direction.x > 0)
            transform.localScale = Vector3.one;
        else if (transform.position.x - direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
