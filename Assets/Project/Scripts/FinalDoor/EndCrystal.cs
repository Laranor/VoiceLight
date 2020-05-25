using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCrystal : MonoBehaviour
{
    public bool enable;
    public GameObject avatar;

    public GameObject text;

    public float distance;

    public GameObject textScale;
    public Vector3 scale;

    bool yes;
    public bool baseOn;
    public Animator crystalUp;
    public GameObject crystal;

    public AnimPorteFinale cinematicPorte;
    public bool intro;

    public bool pilier;

    public int distanceToEnable;
    void Update()
    {
        if(intro)
        {
            Color finalValue = avatar.GetComponentInChildren<Light>().color * avatar.GetComponentInChildren<Light>().intensity / 2;
            crystal.GetComponent<Renderer>().material.SetColor("_EmissionColor", finalValue);
            crystalUp.speed = 10;
        }
        if ((avatar.transform.position - transform.position).magnitude < distanceToEnable && !yes && baseOn)
        {
            crystalUp.SetBool("Up", true);
            yes = true;
        }
        //Debug.Log((avatar.transform.position - transform.position).magnitude);
        if ((avatar.transform.position - transform.position).magnitude < distance && !enable && baseOn)
        {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!intro)
                {
                    cinematicPorte.cinematic = true;
                    if (pilier)
                        cinematicPorte.pitch = true;
                    else
                        cinematicPorte.hero = true;
                }
                enable = true;
                if(intro)
                {
                    avatar.GetComponentInChildren<AvatarLighting>().EndIntro();
                    crystal.SetActive(false);
                }
            }
        }
        if ((avatar.transform.position - transform.position).magnitude >= distance && text != null)
        {
            text.SetActive(false);
        }

        if (enable)
        {
            Destroy(text);
        }
        if(text != null && Camera.main != null)
        {
            text.transform.LookAt(Camera.main.transform);
            textScale.transform.localScale = scale * ((avatar.transform.position - transform.position).magnitude) / 2f;
            //textScale.transform.localScale
        }
    }
}
