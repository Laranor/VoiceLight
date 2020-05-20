using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchPlatform : MonoBehaviour
{
    public AvatarLighting avatar;

    [SerializeField] private float pitchValue;
    [SerializeField] private float dbValue;

    public GameObject endCrystal;
    public float maxHeight;
    public float minHeight;
    public float seuilPitch;
    public float speed;
    public float disableHeight;
    private bool enable;

    public void Update()
    {
        if (endCrystal.GetComponent<EndCrystal>().enable == true)
        {
            if(transform.position.y != disableHeight)
            {
                if(transform.position.y > disableHeight)
                {
                    transform.position -= new Vector3(0, 3, 0) * Time.deltaTime;
                }
                if (transform.position.y < disableHeight)
                {
                    transform.position += new Vector3(0, 3, 0) * Time.deltaTime;
                }
            }
            if (transform.position.y < disableHeight + 0.01f && transform.position.y > disableHeight - 0.01f)
                Destroy(GetComponent<PitchPlatform>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            pitchValue = avatar.PitchValue;
            dbValue = avatar.DbValue;
            speed = (dbValue + 80) / 40;
            if (pitchValue > seuilPitch)
            {
                if (transform.position.y < maxHeight)
                {
                    transform.position += new Vector3(0, 1+speed, 0) * Time.deltaTime;
                }
            }
            if (pitchValue <= seuilPitch && dbValue > -50)
            {
                if (transform.position.y > minHeight )
                {
                    transform.position -= new Vector3(0, 1+speed, 0) * Time.deltaTime;
                }
            }
        }
    }
}
