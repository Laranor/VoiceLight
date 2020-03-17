using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    public Light avatarLight;
    [SerializeField] private float intensity;
    [SerializeField] private float time = 0;
    [SerializeField] private bool opening = false;

    [SerializeField] private float timeWait = 0;

    [SerializeField] private float timeTarget = 2;
    [SerializeField] private float intensityTarget = 2;

    // Update is called once per frame
    void Update()
    {
        if(opening)
        {
            timeWait += Time.deltaTime;
            if(timeWait > 2.5f)
            {
                var z = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z;
                gameObject.transform.Rotate(new Vector3(0, 0.17f, 0), Space.World);
                if (z >= 130)
                    enabled = false;
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!opening)
        {
            if (other.gameObject.name == "Light")
            {
                intensity = avatarLight.intensity - (avatarLight.intensity * ((other.transform.position - transform.position).magnitude / avatarLight.range));
                if (intensity > intensityTarget)
                {
                    time += Time.deltaTime;
                    if (time > timeTarget)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/01_A_IMPLEMENTER/Door_Open_01", transform.position);
                        opening = true;
                    }
                }
                else
                    time = 0;
            }
        }

    }
}
