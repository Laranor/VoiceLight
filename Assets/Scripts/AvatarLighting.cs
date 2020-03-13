using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AvatarLighting : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Light avatarLight;

    private float RmsValue;
    public float DbValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    float[] _samples;

    [SerializeField] private float multiplier;
    [SerializeField] private float seuilSound = -20f;
    [SerializeField] private float maxIntensity = 2f;
    [SerializeField] private float minIntensity = 1f;
    [SerializeField] private float degressivIntensity = 0.5f;
    [SerializeField] private float diviseur = 4f;

    FMOD.Studio.EventInstance lightSound;
    private float soundIntensity;

    void Start()
    {
        _samples = new float[QSamples];

        //Initialisation du son d'ambiance
        lightSound = FMODUnity.RuntimeManager.CreateInstance("event:/SonPourLeProto");
        lightSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        lightSound.start();
    }

    void Update()
    {
        AnalyzeSound();
        multiplier = ((DbValue + 80)/diviseur);
        avatarLight.range = avatarLight.intensity * 5;
        if (DbValue > seuilSound && avatarLight.intensity < maxIntensity)
        {
            //Lumière en fonction des decibels
            avatarLight.intensity = multiplier;
            //Paramètre du son à changer
            soundIntensity = multiplier / 3.75f;
        }
        if (avatarLight.intensity > minIntensity && DbValue < seuilSound)
        {
            //Degressif quand pas de lumière
            avatarLight.intensity -= degressivIntensity * Time.deltaTime;
            soundIntensity -= degressivIntensity/3.75f * Time.deltaTime;
        }

        //Son en fonction de la lumière
        lightSound.setParameterByName("Intensity", soundIntensity);
    }

    //Récupération des décibels du micro
    void AnalyzeSound()
    {
        audioSource.GetOutputData(_samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        
        if (DbValue < -80) DbValue = -80; // clamp it to -80dB min
        Debug.Log(DbValue);                        // get sound spectrum
    }
}
