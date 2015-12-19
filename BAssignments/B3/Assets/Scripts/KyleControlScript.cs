using UnityEngine;
using System.Collections;

public class KyleControlScript : MonoBehaviour
{

    public float animSpeed = 1.5f;
    public float lookSmoother = 3f;
    public float tick;
    public UnityEngine.UI.Text hint;

    private Animator anim; 
    private GameObject dismissableNPC;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        if (anim.layerCount == 3)
        {
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 1);
        }
        tick = 0;
    }
    
    void Update()
    {
       
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            v = v * 6.5f;
        }
        else if (v < 0)
        {
            v = v * 0.5f;
            h = h * -1;
        }

        if (v != 0)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, h * 2, 0));
            transform.rotation = transform.rotation * rotation;
        }
        else if (v == 0)
        {
            h = h * 20;
        }

        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", h);
        anim.speed = animSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //anim.laye(1, 1);
            anim.Play("PistolAim");
            //anim.SetLayerWeight(1, 1);
        }

    }

}
