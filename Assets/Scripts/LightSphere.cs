using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    [SerializeField] private Light avatarLight;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SphereCollider>().radius = avatarLight.range;
    }
}
