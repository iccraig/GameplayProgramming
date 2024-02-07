using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanum
{
    public class AntiVirus : MonoBehaviour
    {
        private Rigidbody rb;

        void Start () 
        {
            // force which we toss the antivirus with
            int launchForce = 50;

            // random direction to toss antivirus in
            Vector3 randomDirection = Random.insideUnitSphere;
            // apply force in the random direction to toss the pill in
            rb = this.GetComponent<Rigidbody>();
            rb.AddForce(randomDirection * launchForce);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Humanoid otherHumanoid = collision.gameObject.GetComponent<Humanoid>();
            if (otherHumanoid != null)
            {
                Destroy(this.gameObject);
                otherHumanoid.InjestAntivirus();
            }
        }
    }
}
