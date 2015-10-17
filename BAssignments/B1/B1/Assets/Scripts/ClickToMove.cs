using UnityEngine;
using System.Collections;

namespace CompleteProject
{

    public class ClickToMove : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private bool walking;
        public bool selected;
        // Use this for initialization
        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            selected = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void moveAgent(RaycastHit hit)
        {
            walking = true;
            navMeshAgent.destination = hit.point;
            navMeshAgent.Resume();
        }
    }
}
