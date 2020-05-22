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
    void Update()
    {
        if ((avatar.transform.position - transform.position).magnitude < distance && !enable)
        {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                enable = true;
            }
        }
        if ((avatar.transform.position - transform.position).magnitude >= distance)
        {
            text.SetActive(false);
        }

        if (enable)
        {
            Destroy(text);
        }
        if(text != null)
        {
            text.transform.LookAt(Camera.main.transform);
            textScale.transform.localScale = scale * ((avatar.transform.position - transform.position).magnitude) / 2f;
            //textScale.transform.localScale
        }
    }
}
