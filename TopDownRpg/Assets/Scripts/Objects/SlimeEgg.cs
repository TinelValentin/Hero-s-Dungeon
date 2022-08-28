using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEgg : Humanoid
{
    [SerializeField] private Transform slimeTransform;
    private Animator animator;
    protected override void Start()
    {
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        Invoke(nameof(Evolve), 5f);
    }

    private void Evolve()
    {
        animator.SetTrigger("Evolve");
        Invoke(nameof(SpawnSlime), 3.3f);
    }

    private void SpawnSlime()
    {
        Destroy(gameObject);
        Instantiate(slimeTransform, transform.position, Quaternion.identity);
    }
}
