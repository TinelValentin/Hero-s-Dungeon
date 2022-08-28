using UnityEngine;

public class Necromancer : RangeEnemy
{
    private float specialAttackTime = 0f;
    private float summonAttackTime = 0f;
    float shootTime = 0f;
    private bool stillCastingSpecial = false;
    
    
    Vector3 scale = new Vector3(2.2f, 2.2f, 2.2f); 

    private Animator animatorControler;
    GameObject staff;

    [SerializeField]
    public GameObject grave;

    protected override void Start()
    {
        base.Start();
        this.xSpeed = 0.2f;
        this.ySpeed = 0.2f;
        this.staff = GameObject.Find("necromancerStaff");
        animatorControler = GetComponent<Animator>();
        
    }

    private void ChangeDirection()
    {
        if ((destination - transform.position).normalized.x > 0.01)
        {
            transform.localScale = scale;
        }

        else if ((destination - transform.position).normalized.x <= 0.01)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }

        if ((destination - transform.position).normalized.y < 0.5)
        {
            staff.SetActive(true);
            changeSprite.sprite = frontSprite;
        }

        else if ((destination - transform.position).normalized.y > 0)
        {
            staff.SetActive(false);
            changeSprite.sprite = BackSprite;
        }

    }

    protected void ChooseRandomSpecialAttack()
    {
        int randomNumber = Random.Range(0, 2);
        int numberOfTimes = Random.Range(1, 5);
        switch (randomNumber)
        {
            case 0:
                for (int i = 0; i < numberOfTimes; i++)
                    Invoke(nameof(ShootProjectilesInCircle),i);
                stillCastingSpecial = false;
                break;
            case 1:
                for (int i = 0; i < numberOfTimes; i++)
                    Invoke(nameof(SendProjectilesFolowing), i);
                stillCastingSpecial = false;
                break;
        }
    }

    protected new void FixedUpdate()
    {
        shootTime += Time.deltaTime;
        specialAttackTime += Time.deltaTime;
        summonAttackTime += Time.deltaTime;
        destination = player.position;
        ChangeDirection();

        if (shootTime >= 1f)
        {
            shootTime %= 1f;
            Shoot();
        }
        if (specialAttackTime > 3f && !stillCastingSpecial)   //If the number of executions of the last attack didn't finish
        {
            ChooseRandomSpecialAttack();
            specialAttackTime %= 3f;
        }
        else
        if (specialAttackTime == 3f)
        {
            specialAttackTime = 3f;
        }


        if (summonAttackTime > 15f)
        {
            summonAttackTime %= 15f;
            Invoke(nameof(SummonMinions), 3.5f);
        }
    }

    protected void SummonMinions()
    {
        float radius = 4f;
        int numObjects = 3;
        for (int i = 0; i < numObjects; i++)
        {
            float theta = i * 2 * Mathf.PI / numObjects;
            float x = Mathf.Sin(theta) * radius;
            float y = Mathf.Cos(theta) * radius;


            GameObject ob = new GameObject();
            ob.transform.parent = transform;
            ob.transform.position = new Vector3(x, y, 0);
            Instantiate(grave, transform.position + ob.transform.position, Quaternion.identity);
        }
    }


    protected void ShootProjectilesInCircle()
    {
        animatorControler.SetTrigger("Attack1");
        float radius = 1.5f;
        for (int numObjects = 5; numObjects < 10; numObjects++)
            for (int i = 0; i < numObjects; i++)
            {
                float theta = i * 2 * Mathf.PI / numObjects;
                float x = Mathf.Sin(theta) * radius;
                float y = Mathf.Cos(theta) * radius;


                GameObject ob = new GameObject();
                ob.transform.parent = transform;
                ob.transform.position = new Vector3(x, y, 0);
                Transform projectile = Instantiate(arrow, transform.position + ob.transform.position, Quaternion.identity);
                Vector3 shootDirection = (projectile.position - transform.position).normalized;
                projectile.GetComponent<Projectile>().shootDirection(shootDirection);
            }
    }


    protected void SendProjectilesFolowing()
    {
        animatorControler.SetTrigger("Attack2");
        int numObjects = 7;
        float radius = 1.5f;
        for (int i = 0; i < numObjects; i++)
        {
            float theta = i * 2 * Mathf.PI / numObjects;
            float x = Mathf.Sin(theta) * radius;
            float y = Mathf.Cos(theta) * radius;


            GameObject ob = new GameObject();
            ob.transform.parent = transform;
            ob.transform.position = new Vector3(x, y, 0);
            Transform projectile = Instantiate(arrow, transform.position + ob.transform.position, Quaternion.identity);
            Vector3 shootDirection = (player.position - projectile.position).normalized;
            projectile.GetComponent<Projectile>().shootDirection(shootDirection);
        }
    }

    protected override void ReciveDamage(Damage damage)
    {
        base.ReciveDamage(damage);
        BossHealthBar.UpdateHealth(this.currentHealth);
    }

    protected override void Death()
    {
        base.Death();
        BossHealthBar.HideHealthBar();
    }

    private void OnEnable()
    {
        BossHealthBar.ShowHealthBar(this.maxHealth, currentHealth);
    }
}
