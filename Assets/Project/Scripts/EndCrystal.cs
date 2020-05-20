using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCrystal : MonoBehaviour
{
    public bool enable;
    public GameObject avatar;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && (avatar.transform.position - transform.position).magnitude < 2)
        {
            enable = true;
        }
    }
}
