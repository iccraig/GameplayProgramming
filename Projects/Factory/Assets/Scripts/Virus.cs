using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanum
{
    public class Virus : MonoBehaviour
    { 
        [SerializeField] private float moveSpeed = 20.0f; // Speed of movement
        private Rigidbody rb;

        void Start()
        {
            // Get the Rigidbody component attached to the GameObject
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.freezeRotation = true;
        }

        // void FixedUpdate()
        // {
        //     // Calculate a random direction for movement
        //     Vector3 randomDirection = Random.insideUnitSphere;
        //     // Apply force in the random direction to make the black hole float around
        //     rb.AddForce(randomDirection * moveSpeed);
        // }

        void FixedUpdate()
        {
            // Calculate a random direction for movement
            Vector3 randomDirection = Random.insideUnitSphere;

            // Adjust the probability of moving downward (e.g., 80% chance)
            if (Random.Range(0, 100) < 10)
            {
                // Move downward by setting the y-component of randomDirection to a negative value
                randomDirection.y = -Mathf.Abs(randomDirection.y);
            }

            // Apply force in the modified random direction to make the virus float around
            rb.AddForce(randomDirection * moveSpeed);

            // Limit the height of the virus by clamping its position on the y-axis
            float maxHeight = 20.0f; // Adjust this value as needed
            rb.position = new Vector3(rb.position.x, Mathf.Clamp(rb.position.y, -maxHeight, maxHeight), rb.position.z);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Humanoid otherHumanoid = collision.gameObject.GetComponent<Humanoid>();
            if (otherHumanoid != null)
            {
                Destroy(gameObject);
                otherHumanoid.TriggerExplosion();
            }
        }
    }
}

    