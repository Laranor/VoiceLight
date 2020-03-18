using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porte : MonoBehaviour
{
    [SerializeField] private float timeWait = 0;
    [SerializeField] private bool sound = false;
    public Lock lock1;
    public Lock lock2;
    public Lock lock3;
    public Lock lock4;
    public Lock lock5;

    // Update is called once per frame
    void Update()
    {
        if(!lock1.locked && !lock2.locked && !lock3.locked && !lock4.locked && !lock5.locked)
        {
            if(!sound)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/01_A_IMPLEMENTER/Door_Open_01", transform.position);
                sound = true;
            }
            timeWait += Time.deltaTime;
            if (timeWait > 2.5f)
            {
                var y = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y;
                gameObject.transform.Rotate(new Vector3(0, 0.17f, 0), Space.World);
                if (y >= 140)
                    enabled = false;
            }
        }

    }
}
