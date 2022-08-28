using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    class Grave : Humanoid
    {
        [SerializeField] private Transform skeletonTransform;
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
            Invoke(nameof(Spawn), 3.3f);
        }

        private void Spawn()
        {
            Destroy(gameObject);
            Instantiate(skeletonTransform, transform.position, Quaternion.identity);
        }
    }
}
