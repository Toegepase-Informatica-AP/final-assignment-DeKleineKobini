using UnityEngine;

namespace Assets.Scripts
{
    public class SimplePlayer : MonoBehaviour
    {
        public float movementSpeed = 1;
        public float rotationSpeed = 300;

        private Environment environment;
        private Rigidbody body;

        private void Start()
        {
            environment = GetComponentInParent<Environment>();
            body = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            // Check for forward input.
            if (Input.GetKey(KeyCode.W))
                transform.position += transform.forward * movementSpeed * Time.deltaTime * 2;

            // Check for rotation input.
            if (Input.GetKey(KeyCode.LeftArrow))
                transform.Rotate(0, rotationSpeed * Time.deltaTime * -1, 0);
            else if (Input.GetKey(KeyCode.RightArrow))
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Finish"))
            {
                EndCycle();
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Car"))
            {
                EndCycle();
            }
        }

        public void EndCycle()
        {
            body.velocity = new Vector3(0, 0, 0);
            environment.ResetEnvironment();
        }
    }
}
