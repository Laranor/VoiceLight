using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    Light selfLight;
    public float intensityMax = 5;
    public float diviseur = 10;
    public float decreaseValue = 0.5f;

    GameObject avatarHand;
    public bool holding = false;
    void Start()
    {
        selfLight = GetComponent<Light>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<Light>() && other.gameObject.GetComponentInParent<Decibel>())
        {
            StockLight(other.gameObject.GetComponentInChildren<Light>());
        }
        if (other.gameObject.name == "Avatar")
        {
            if(!holding)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    holding = true;
                    avatarHand = other.transform.GetChild(0).gameObject;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    holding = false;
                    avatarHand = null;
                }
            }

        }
    }

    private void StockLight (Light light)
    {
        if ((light.gameObject.transform.position - transform.position).magnitude <= 2)
        {
            float intensity = light.intensity - (light.intensity * ((light.transform.position - transform.position).magnitude / light.range));
            Debug.Log(intensity);
            if(selfLight.intensity < intensityMax && intensity > 0)
                selfLight.intensity += intensity / diviseur * Time.deltaTime;
        }
    }

    private void Update()
    {
        selfLight.range = selfLight.intensity * 5;
        if(selfLight.intensity > 0)
        {
            selfLight.intensity -= decreaseValue * Time.deltaTime;
        }

        if(holding)
        {
            transform.position = avatarHand.transform.position;
        }
    }
}
