using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Torch : MonoBehaviour
{

    Transform mainLight;
    Transform flickerLight;
    UnityEngine.Rendering.Universal.Light2D mainLightComponent;
    UnityEngine.Rendering.Universal.Light2D flickerLightComponent;


    void Start()
    {

            mainLight = this.transform.GetChild(0);
            flickerLight = this.transform.GetChild(1);
            mainLightComponent = mainLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            flickerLightComponent = flickerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

            StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (; ; ) //this is while(true)
        {
            float randomIntensity = Random.Range(0.5f, 2.5f);
            flickerLightComponent.intensity = randomIntensity;


            float randomTime = Random.Range(0.05f, 0.15f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
