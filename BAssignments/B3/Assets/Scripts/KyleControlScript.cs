using UnityEngine;
using System.Collections;

public class KyleControlScript : MonoBehaviour
{

    public float animSpeed = 1.5f;
    public float lookSmoother = 3f;
    public float tick;
    public UnityEngine.UI.Text hint;

    private Animator anim; // a reference to the animator on the character
    private GameObject dismissableNPC;

    void Start()
    {
        // initialising reference variables
        anim = GetComponent<Animator>();
        if (anim.layerCount == 2)
        {
            anim.SetLayerWeight(1, 1);
        }
        tick = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Script from our B1 modified VERY slightly for this assignment (no jump / fall)
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
            anim.SetBool("H_PistolAim", true);
        }

    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shopper") || other.gameObject.CompareTag("Thief"))
        {
            dismissableNPC = other.gameObject;
            hint.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(dismissableNPC))
        {
            dismissableNPC = null;
            hint.gameObject.SetActive(false);
        }
    }*/


}
