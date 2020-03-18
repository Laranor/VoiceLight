using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock2 : MonoBehaviour
{
    public bool locked = true;
    private void Update()
    {
        if (transform.position.x <= -1.3f && !locked)
            transform.position += new Vector3(0.01f, 0, 0);
    }
    public void Unlocking()
    {
        locked = false;
    }        
}