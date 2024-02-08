using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanum
{
    public class Humanoid : MonoBehaviour
    {
        [SerializeField]
        private GameObject humanoidPrefab;
        private float humanoidID;
        private float humanoidSize;
        private int antiVirusesTaken;
        private Vector3 moveDirection;


        [SerializeField] private Color explosionColor = Color.green; // Color to change to on explosion
        private MeshRenderer meshRenderer;
        private Rigidbody rb;
        private bool isGrounded = false;

        void Start()
        {
            // Get the Mesh Renderer component attached to the GameObject
            meshRenderer = GetComponent<MeshRenderer>();
            rb = GetComponent<Rigidbody>();
        }
        
        void Awake()
        {
            this.antiVirusesTaken = 0;
            this.humanoidSize = 1;
        }

        private void Update()
        {
            transform.up = Vector3.up;
            transform.forward = moveDirection;
            
            if (isGrounded)
            {
                if (Random.Range(0, 100) < 5) // Adjust the probability (5%) based on how often you want them to change direction
                {
                    transform.up = Vector3.up;
                    WalkRandomly();
                }
            }
        }

        void WalkRandomly()
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            moveDirection = new Vector3(randomX, 0f, randomZ).normalized;
            
            // transform.forward = randomDirection;
            // Debug.Log($"Transform.forward: {transform.forward}");
            // Debug.Log($"Random Direction: {randomDirection}");
            // Debug.Log($"Local Rotation: {transform.localRotation.eulerAngles}");
            // Debug.Log($"Global Rotation: {transform.rotation.eulerAngles}");

            rb.AddForce(moveDirection * 2f, ForceMode.Impulse); // Adjust force as needed
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isGrounded)
            {
                Humanoid otherHumanoid = collision.gameObject.GetComponent<Humanoid>();
                if (otherHumanoid != null)
                {
                    if (this.humanoidSize == otherHumanoid.getSize())
                    {
                        // Debug.Log($"Humanoid {otherHumanoid.getID()}, touched Humanoid {humanoidID}!");
                        destroyAndUpdate(otherHumanoid);
                    }
                }
            }
            if (collision.gameObject.name == "Ground")
            {
                // Debug.Log("ON GROUND");
                isGrounded = true;
            }
        }

        private void destroyAndUpdate(Humanoid otherHumanoid)
        {
            if (this.humanoidID > otherHumanoid.getID())
            {
                setSize(humanoidSize * 1.25f);
                if(antiVirusesTaken > 0)
                    ChangeColor(Color.blue);
                // Debug.Log($"Destroying humanoid {otherHumanoid.getID()}");
                otherHumanoid.destoryHumanoid();
            }
            else
            {
                otherHumanoid.setSize(otherHumanoid.getSize() * 1.35f);
                if(otherHumanoid.getAntivirusesTaken() > 0)
                    otherHumanoid.ChangeColor(Color.blue);
                // Debug.Log($"Destroying humanoid {humanoidID}");
                Destroy(gameObject);
            }
        }

        /* This is the explosion when interacting with the virus */
        public void TriggerExplosion() 
        {
            // This human is safe from the virus so halt the funciton
            if (this.antiVirusesTaken > 0)
                return;

            // Debug.Log($"Humanoid {humanoidID} exploded!");
            // Implement explosion effects or destruction logic here
            
            ChangeColor(explosionColor);
            StartCoroutine(DestroyAfterDelay(4.0f)); // Change 3f to the desired delay in seconds
        }

        public void InjestAntivirus()
        {
            this.antiVirusesTaken++;

            // change color to show that we've injested the antivirus
            if (this.antiVirusesTaken < 5)
            {
                ChangeColor(Color.blue);
            }
            // Overdose! More than 4 antiviruses taken
            else
            {
                ChangeColor(Color.red); // change color to red signifying OD
                StartCoroutine(DestroyAfterDelay(4.0f));
            }
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(delay);

            // Destroy the GameObject after the delay
            Destroy(gameObject);
        }

        private void ChangeColor(Color newColor)
        {
            if (meshRenderer != null)
            {
                // Change the color of the Mesh Renderer's material
                meshRenderer.material.color = newColor;
            }
            else
            {
                Debug.LogWarning("Mesh Renderer component not found. Color cannot be changed.");
            }
        }


        public int getAntivirusesTaken() 
        {
            return antiVirusesTaken;
        }
        public void destoryHumanoid()
        {
            Destroy(gameObject);
        }
        public float getSize()
        {
            return humanoidSize;
        }
        public void setSize(float newSize)
        {
            humanoidSize = newSize;
            transform.localScale = new Vector3(humanoidSize, humanoidSize, humanoidSize);
        }
        public float getID()
        {
            return humanoidID;
        }
        public void setID(float iD)
        {
            humanoidID = iD;
        }
    }
}

    