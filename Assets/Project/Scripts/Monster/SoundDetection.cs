using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetection : MonoBehaviour
{
    public List<GameObject> targets;
    public float intensityGoal = 1;
    public void Update()
    {
        //Debug.Log(GetLoudestObject(targets));
        //GetLoudestObject(targets);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Light>() && other.GetComponent<Decibel>() && !targets.Contains(other.gameObject) || !targets.Contains(other.gameObject) && other.GetComponentInChildren<Light>() && other.GetComponent<Decibel>())
        {
            float intensity = other.GetComponentInChildren<Light>().intensity - (other.GetComponentInChildren<Light>().intensity * ((other.transform.position - transform.position).magnitude / other.GetComponentInChildren<Light>().range));
            if (intensity >= intensityGoal)
                targets.Add(other.gameObject);
        }
        if (other.GetComponent<Light>() && other.GetComponent<Decibel>() && targets.Contains(other.gameObject) || targets.Contains(other.gameObject) && other.GetComponentInChildren<Light>() && other.GetComponent<Decibel>())
        {
            float intensity = other.GetComponentInChildren<Light>().intensity - (other.GetComponentInChildren<Light>().intensity * ((other.transform.position - transform.position).magnitude / other.GetComponentInChildren<Light>().range));
            if (intensity <= intensityGoal)
                targets.Remove(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Light>() && other.GetComponent<Decibel>() && targets.Contains(other.gameObject) || other.GetComponentInChildren<Light>() && other.GetComponent<Decibel>() && targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

    public GameObject GetLoudestObject(List<GameObject> enemies)
    {
        GameObject bestTarget = null;
        foreach (GameObject potentialTarget in enemies)
        {
            float intensityHigh = -Mathf.Infinity;
            float intensity = potentialTarget.GetComponentInChildren<Light>().intensity - (potentialTarget.GetComponentInChildren<Light>().intensity * ((potentialTarget.transform.position - transform.position).magnitude / potentialTarget.GetComponentInChildren<Light>().range));
            if (intensity > intensityHigh)
            {
                intensityHigh = intensity;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}
