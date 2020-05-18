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
        if (endCrystal.GetComponent<EndCrystalPilier>().enable == true)
        {
            transform.position = new Vector3(transform.position.x, disableHeight, transform.position.z);
            Destroy(GetComponent<PitchPlatform>());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        pitchValue = avatar.PitchValue;
        dbValue = avatar.DbValue;
        speed = (dbValue + 80) / 40;
        if (other.gameObject.name == "Avatar")
        {
            if(pitchValue > seuilPitch)
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
