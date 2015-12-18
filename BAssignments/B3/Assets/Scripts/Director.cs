using UnityEngine;
using System.Collections;

namespace CompleteProject
{


    public class Director : MonoBehaviour
    {

       private bool CharacterClicked;
        private bool obstacleClicked;
        private float speed = 2.0f;
        private GameObject obstacle;
        private GameObject MainCamera;
        private GameObject Character;

        private PlayerCamera script;
        //private GameObject[] agents;

        void start()
        {
            CharacterClicked = false;
            obstacleClicked = false;
            //MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        void Awake()
        {
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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
                    if (hit.collider.CompareTag("Character") || hit.collider.CompareTag("mayor") || hit.collider.CompareTag("necromancer"))
                    {
                        CharacterClicked = true;
                        obstacleClicked = false;
                        Character = GameObject.Find(hit.collider.name);
                        MainCamera.GetComponent<CameraController>().enabled = false;
                        MainCamera.GetComponent<PlayerCamera>().enabled = true;
                        MainCamera.GetComponent<PlayerCamera>().target = Character;
                        MainCamera.GetComponent<PlayerCamera>().offset = new Vector3 (0, -3, 6);

                    }
                    //else if
                    /*if (hit.collider.CompareTag("Obstacle"))
                    {
                        if(obstacle != null)
                        {
                            obstacle.GetComponent<Renderer>().material.color = Color.black;
                        }
  
                        obstacleClicked = true;
                        obstacle = GameObject.Find(hit.collider.name);
                        obstacle.GetComponent<Renderer>().material.color = Color.red;
                    }
                    else
                    {
                        obstacleClicked = false;
                        obstacle.GetComponent<Renderer>().material.color = Color.black;
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

                    //}
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

            if (Input.GetKey(KeyCode.Escape))
            {
                CharacterClicked = false;

                MainCamera.GetComponent<PlayerCamera>().enabled = false;
                MainCamera.GetComponent<CameraController>().enabled = true;
            }
        }
    
    }
}
