using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public Light avatarLight;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SphereCollider>().radius = avatarLight.range;
    }
}
