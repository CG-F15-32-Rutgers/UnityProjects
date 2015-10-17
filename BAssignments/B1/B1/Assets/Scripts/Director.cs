using UnityEngine;
using System.Collections;

namespace CompleteProject
{


    public class Director : MonoBehaviour
    {

        private bool agentClicked = false;
        private bool obstacleClicked = false;
        private float speed = 2.0f;
        private GameObject obstacle;

        private ClickToMove script;
        private ClickToMove script1;
        private ClickToMove script2;
        private ClickToMove script3;
        private ClickToMove script4;
        private ClickToMove script5;
        private ClickToMove script6;
        private ClickToMove script7;
        private ClickToMove script8;
        private ClickToMove script9;
        private ClickToMove script10;
        private ClickToMove script11;

        // Use this for initialization
        void Start()
        {
            script = GameObject.Find("Agent").GetComponent<ClickToMove>();
            script1 = GameObject.Find("Agent (1)").GetComponent<ClickToMove>();
            script2 = GameObject.Find("Agent (2)").GetComponent<ClickToMove>();
            script3 = GameObject.Find("Agent (3)").GetComponent<ClickToMove>();
            script4 = GameObject.Find("Agent (4)").GetComponent<ClickToMove>();
            script5 = GameObject.Find("Agent (5)").GetComponent<ClickToMove>();
            script6 = GameObject.Find("Agent (6)").GetComponent<ClickToMove>();
            script7 = GameObject.Find("Agent (7)").GetComponent<ClickToMove>();
            script8 = GameObject.Find("Agent (8)").GetComponent<ClickToMove>();
            script9 = GameObject.Find("Agent (9)").GetComponent<ClickToMove>();
            script10 = GameObject.Find("Agent (10)").GetComponent<ClickToMove>();
            script11 = GameObject.Find("Agent (11)").GetComponent<ClickToMove>();

        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("Agent"))
                    {
                        agentClicked = true;
                        obstacleClicked = false;
                        SpecifyAgent(hit);
                    }
                    else if (hit.collider.CompareTag("Obstacle"))
                    {
                        obstacleClicked = true;
                        obstacle = GameObject.Find(hit.collider.name);
                    }
                    else
                    {
                        obstacleClicked = false;
                        if (agentClicked)
                        {
                            agentClicked = false;
                            moveSelectedAgents(hit);
                        }

                    }
                }
            }

            if (obstacleClicked)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    obstacle.transform.position += Vector3.right * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    obstacle.transform.position += Vector3.left * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    obstacle.transform.position += Vector3.forward * speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    obstacle.transform.position += Vector3.back * speed * Time.deltaTime;
                }
            }

        }

        void SpecifyAgent(RaycastHit hit)
        {
            if(hit.collider.name == "Agent")
            {
                script.selected = true;
            }
            if (hit.collider.name == "Agent (1)")
            {
                script1.selected = true;
            }
            if (hit.collider.name == "Agent (2)")
            {
                script2.selected = true;
            }
            if (hit.collider.name == "Agent (3)")
            {
                script3.selected = true;
            }
            if (hit.collider.name == "Agent (4)")
            {
                script4.selected = true;
            }
            if (hit.collider.name == "Agent (5)")
            {
                script5.selected = true;
            }
            if (hit.collider.name == "Agent (6)")
            {
                script6.selected = true;
            }
            if (hit.collider.name == "Agent (7)")
            {
                script7.selected = true;
            }
            if (hit.collider.name == "Agent (8)")
            {
                script8.selected = true;
            }
            if (hit.collider.name == "Agent (9)")
            {
                script9.selected = true;
            }
            if (hit.collider.name == "Agent (10)")
            {
                script10.selected = true;
            }
            if (hit.collider.name == "Agent (11)")
            {
                script11.selected = true;
            }

            return;
        }

        void moveSelectedAgents(RaycastHit hit)
        {
            if (script.selected)
            {
                script.selected = false;
                script.moveAgent(hit);
            }
            if (script1.selected)
            {
                script1.selected = false;
                script1.moveAgent(hit);
            }
            if (script2.selected)
            {
                script2.selected = false;
                script2.moveAgent(hit);
            }
            if (script3.selected)
            {
                script3.selected = false;
                script3.moveAgent(hit);
            }
            if (script4.selected)
            {
                script4.selected = false;
                script4.moveAgent(hit);
            }
            if (script5.selected)
            {
                script5.selected = false;
                script5.moveAgent(hit);
            }
            if (script6.selected)
            {
                script6.selected = false;
                script6.moveAgent(hit);
            }
            if (script7.selected)
            {
                script7.selected = false;
                script7.moveAgent(hit);
            }
            if (script8.selected)
            {
                script8.selected = false;
                script8.moveAgent(hit);
            }
            if (script9.selected)
            {
                script9.selected = false;
                script9.moveAgent(hit);
            }
            if (script10.selected)
            {
                script10.selected = false;
                script10.moveAgent(hit);
            }
            if (script11.selected)
            {
                script11.selected = false;
                script11.moveAgent(hit);
            }

            return;
        }
    }
}
