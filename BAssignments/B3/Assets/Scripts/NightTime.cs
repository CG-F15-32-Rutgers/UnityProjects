using UnityEngine;
using System.Collections;

public class NightTime : MonoBehaviour {

    //public Material Dusk;
    public Material Night;
    public Light sun;
    public ParticleSystem smoke;
    public Light[] lanterns;
    public Light summon_light;
    public GameObject[] zombies;

	public void changeTime()
    {
        sun.enabled = false;
        RenderSettings.skybox = Night;
        RenderSettings.ambientLight = Color.black;
        RenderSettings.fogColor = Color.black;
        RenderSettings.reflectionIntensity = 0;
        smoke.enableEmission = true;

        for(int i = 0; i < lanterns.Length; ++i)
        {
            lanterns[i].enabled = true;
        }

        summon_light.enabled = true;

        for (int i = 0; i < zombies.Length; ++i)
        {
            zombies[i].SetActive(true);
        }
    }

    public void lightsOff()
    {
        for (int i = 0; i < lanterns.Length; ++i)
        {
            lanterns[i].enabled = false;
        }
    }
}
