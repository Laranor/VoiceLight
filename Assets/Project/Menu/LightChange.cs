using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public float min;
    public float max;
    Light selfLight;
    public float value;
    private float timer;
    private float timerReset = 0.3f;
    private float timer2;
    private void Start()
    {
        selfLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer2 > 1)
        {
            timerReset = Random.Range(0.1f, 0.5f);
            timer2 = 0;
        }
        if (timer > timerReset)
        {
            value = Random.Range(-0.5f, 0.5f);
            timer = 0;
        }
        
        if (transform.position.z >= min && value < 0)
        {
            transform.position += new Vector3(0, 0, value) * Time.deltaTime;
        }
        if (transform.position.z <= max && value > 0)
        {
            transform.position += new Vector3(0, 0, value) * Time.deltaTime;
        }
        if (transform.position.z < min)
        {
            value = 0.5f;
        }
        if (transform.position.z > max)
        {
            value = -0.5f;
        }
    }
}
