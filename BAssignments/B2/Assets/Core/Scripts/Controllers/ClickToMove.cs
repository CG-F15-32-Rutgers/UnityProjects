using UnityEngine;
using System.Collections;

namespace CompleteProject
{

    public class ClickToMove : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private bool running;
        private float speedBoost = 1.5f;
        private float initialSpeed;
        public bool selected;
        // Use this for initialization
        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            initialSpeed = navMeshAgent.speed;
            selected = false;
            running = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void moveAgent(RaycastHit hit, bool run)
        {
            running = run;
            navMeshAgent.destination = hit.point;
            navMeshAgent.Resume();
        }
    }
}
