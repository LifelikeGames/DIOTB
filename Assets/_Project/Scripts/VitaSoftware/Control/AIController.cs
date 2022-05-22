using UnityEngine;
using UnityEngine.AI;
using VitaSoftware.Shop;

namespace VitaSoftware.Control
{
    public class AIController : Controller
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private QueueManager queueManager;
        [SerializeField] private Animator animator;

        private bool needsGravestone = true;
        private static readonly int Velocity = Animator.StringToHash("Velocity");

        private void OnEnable()
        {
            queueManager.NewSpotAvailable += OnNewSpotAvailable;
        }

        private void OnDisable()
        {
            queueManager.NewSpotAvailable -= OnNewSpotAvailable;
        }

        private void FixedUpdate()
        {
            animator.SetFloat(Velocity, agent.velocity.magnitude);
        }

        private void OnNewSpotAvailable()
        {
            GetNewTargetPosition();
        }

        private void Awake()
        {
            GetNewTargetPosition();
        }

        private void GetNewTargetPosition()
        {
            if (!needsGravestone) return;
            if (queueManager.TryGetSpot(out var position, this))
                agent.SetDestination(position);
        }

        public override void SendHome()
        {
            needsGravestone = false;
            agent.SetDestination(new Vector3(-20, 0, -20));
            Destroy(gameObject, 10);
        }
    }
}