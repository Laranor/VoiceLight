using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingColor : MonoBehaviour
{
    public Renderer doorCircle;

    private float f1 = 1.498039f;
    private float f2 = 1.498039f;
    private float f3 = 0;

    public float max = 1.498039f;

    private int state = 1;
    public float timeToChange;

    private void Start()
    {
        f1 = max;
        f2 = max;
        f3 = 0;
    }
    void Update()
    {
        if(state == 1)
        {
            f2 -= max / timeToChange * Time.deltaTime;
            if (f2 <= 0)
                state = 2;
        }
        if (state == 2)
        {
            f3 += max / timeToChange * Time.deltaTime;
            if (f3 >= max)
                state = 3;
        }
        if (state == 3)
        {
            f1 -= max / timeToChange * Time.deltaTime;
            if (f1 <= 0)
                state = 4;
        }
        if (state == 4)
        {
            f2 += max / timeToChange * Time.deltaTime;
            if (f2 >= max)
                state = 5;
        }
        if (state == 5)
        {
            f3 -= max / timeToChange * Time.deltaTime;
            if (f3 <= 0)
                state = 6;
        }
        if (state == 6)
        {
            f1 += max / timeToChange * Time.deltaTime;
            if (f1 >= max)
                state = 1;
        }
        Color finalValue = new Vector4(f1, f2, f3, 1);
        doorCircle.material.SetColor("_EmissionColor", finalValue);
    }
}
