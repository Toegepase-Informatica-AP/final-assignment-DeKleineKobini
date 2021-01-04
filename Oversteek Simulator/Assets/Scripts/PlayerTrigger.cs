using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerTrigger : MonoBehaviour
    {
        private Player player;

        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        void OnTriggerEnter(Collider collision)
        {
            player.OnChildTriggerEnter(collision);
        }

        void OnTriggerStay(Collider collision)
        {
            player.OnChildTriggerStay(collision);
        }

        void OnCollisionEnter(Collision collision)
        {
            player.OnChildCollisionEnter(collision);
        }
    }
}
