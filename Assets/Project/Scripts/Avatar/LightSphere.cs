﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public Light avatarLight;

    public Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    private float frequencyColor = 0;
    public AvatarLighting avatar;

    public Color lowPitch;
    public Color highPitch;

    public float seuilPitch;
    private void Start()
    {
        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = lowPitch;
        colorKey[0].time = 0.0f;
        colorKey[1].color = highPitch;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SphereCollider>().radius = avatarLight.range;
        if (avatar.PitchValue < seuilPitch)
            frequencyColor = avatar.PitchValue / seuilPitch;
        else
            frequencyColor = 1;
        avatarLight.color = gradient.Evaluate(frequencyColor);
    }
}
