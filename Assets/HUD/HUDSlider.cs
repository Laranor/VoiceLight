using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSlider : MonoBehaviour
{
    public Light avatarLighting;
    public AvatarLighting avatar;
    public RectTransform sliderContainer;

    private float minDistance;
    private float maxDistance = 0;
    private float sliderSize;

    public Image blueImage;
    
    public Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    private float frequencyColor = 0;

    //Blue
    public RectTransform blueSliderCharge;
    private float blueCharge;

    private void Start()
    {
        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.yellow;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.red;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        minDistance = sliderContainer.sizeDelta.x - 2;

        sliderSize = maxDistance - minDistance;

        blueSliderCharge.offsetMax = new Vector2(-minDistance, blueSliderCharge.offsetMax.y);
    }
    
    void Update()
    {
        blueCharge = Mathf.Round(avatarLighting.intensity/3.75f * 100);
        float blueChargeValue = minDistance + (blueCharge * sliderSize) / 30;
        blueSliderCharge.offsetMax = new Vector2(- blueChargeValue, blueSliderCharge.offsetMax.y);

        frequencyColor = avatar.PitchValue / 2000;
        blueImage.color = gradient.Evaluate(frequencyColor);
    }
}
