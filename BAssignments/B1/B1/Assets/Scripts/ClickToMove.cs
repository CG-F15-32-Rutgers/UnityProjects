using UnityEngine;
using System.Collections;

namespace CompleteProject
{

    public class ClickToMove : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private bool walking;
        private bool agentClicked;
        private bool obstacleClicked;
        // Use this for initialization
        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButton(0))
            {
                if(Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("Agent"))
                    {
                        agentClicked = true;
                    }
                    else if (hit.collider.CompareTag("Obstacle"))
                    {
                        obstacleClicked = true;
                    }
                    else
                    {
                        if (agentClicked)
                        {
                            walking = true;
                            agentClicked = false;
                            navMeshAgent.destination = hit.point;
                            navMeshAgent.Resume();

                        }
 
                    }
                }
            }

        }
    }
}
