using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AvatarLighting : MonoBehaviour
{
    public AudioSource audioSource;
    public Light avatarLight;

    

    public float RmsValue;
    public float DbValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    [SerializeField] private float multiplier;
    [SerializeField] private float seuilSound = -20f;
    [SerializeField] private float maxIntensity = 2f;
    [SerializeField] private float minIntensity = 1f;
    [SerializeField] private float degressivIntensity = 0.5f;
    [SerializeField] private float diviseur = 4f;

    void Start()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
    }

    void Update()
    {
        AnalyzeSound();
        multiplier = ((DbValue + 80)/diviseur);
        avatarLight.range = avatarLight.intensity * 5;

        if (DbValue > seuilSound && avatarLight.intensity < maxIntensity)
        {
            avatarLight.intensity += multiplier * Time.deltaTime;
        }
        if (avatarLight.intensity > minIntensity /*&& DbValue < seuilSound*/)
        {
            avatarLight.intensity -= degressivIntensity * Time.deltaTime;
        }
    }

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
