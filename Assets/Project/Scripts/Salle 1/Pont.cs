using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pont : MonoBehaviour
{
    public bool on = false;
    [SerializeField] private float speed = 2;
    private void Update()
    {
        if (on)
        {
            if (transform.position.x <= -1.7851)
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            if (transform.position.x >= -6.15)
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
    }
}
