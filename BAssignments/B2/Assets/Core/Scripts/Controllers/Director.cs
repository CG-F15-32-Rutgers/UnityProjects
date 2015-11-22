using UnityEngine;
using System.Collections;

namespace CompleteProject
{


    public class Director : MonoBehaviour
    {

       // private bool agentClicked;
        private bool obstacleClicked;
        private float speed = 2.0f;
        private GameObject obstacle;

        //private ClickToMove script;
        //private GameObject[] agents;

        void start()
        {
            //agentClicked = false;
            obstacleClicked = false;
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
                    /*if (hit.collider.CompareTag("Agent"))
                    {
                        agentClicked = true;
                        obstacleClicked = false;
                        script = GameObject.Find(hit.collider.name).GetComponent<ClickToMove>();
                        script.selected = true;
                    }*/
                    //else if
                    if (hit.collider.CompareTag("Obstacle"))
                    {
                        obstacleClicked = true;
                        obstacle = GameObject.Find(hit.collider.name);
                    }
                    else
                    {
                        obstacleClicked = false;
                        /*if (agentClicked)
                        {
                            agentClicked = false;
                            agents = GameObject.FindGameObjectsWithTag("Agent");
                            foreach (GameObject go in agents)
                            {
                                script = go.GetComponent<ClickToMove>();
                                if(script.selected == true)
                                {
                                    script.selected = false;
                                    script.moveAgent(hit, true);
                                }

                            }
                        }*/

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
    }
}
