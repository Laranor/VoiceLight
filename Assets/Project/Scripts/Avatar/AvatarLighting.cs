using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AvatarLighting : MonoBehaviour
{
    public Decibel db;
    [FMODUnity.EventRef]
    public AudioSource audioSource;
    public Light avatarLight;
    private float RmsValue;
    public float DbValue;
    public float PitchValue;

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

    FMOD.Studio.EventInstance lightSound;
    [SerializeField] private float soundIntensity;

    public Renderer handCrystal;

    public bool intro = true;
    public Transform introCrystal;
    void Start()
    {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;

        //Initialisation du son d'ambiance
        lightSound = FMODUnity.RuntimeManager.CreateInstance("event:/Cave/SonPourLeProto");
        lightSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        lightSound.start();
    }

    public void EndIntro()
    {
        intro = false;
        handCrystal.gameObject.SetActive(true);
        avatarLight.gameObject.transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        if(intro)
        {
            handCrystal.gameObject.SetActive(false);
            avatarLight.gameObject.transform.position = introCrystal.position;
        }

        lightSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        AnalyzeSound();
        db.decibel = DbValue;
        multiplier = ((DbValue + 80)/diviseur);
        avatarLight.range = avatarLight.intensity * 5;
        Color finalValue = avatarLight.color * avatarLight.intensity/2;
        handCrystal.material.SetColor("_EmissionColor", finalValue);
        // Lumière en temps réel
        /*if (DbValue > seuilSound && avatarLight.intensity < maxIntensity)
        {
            //Lumière en fonction des decibels
            avatarLight.intensity = multiplier;
            //Paramètre du son à changer
            soundIntensity = multiplier;
        }
        if (avatarLight.intensity > minIntensity && DbValue < seuilSound)
        {
            //Degressif quand pas de lumière 
            avatarLight.intensity -= degressivIntensity * Time.deltaTime;
            soundIntensity -= degressivIntensity * Time.deltaTime;
        }*/

        // Lumière progressive
        if (DbValue > seuilSound && avatarLight.intensity < maxIntensity)
        {
            //Lumière en fonction des decibels
            avatarLight.intensity += multiplier;
            //Paramètre du son à changer
            soundIntensity += multiplier;
        }
        if (avatarLight.intensity > minIntensity /*&& DbValue < seuilSound*/)
        {
            //Degressif quand pas de lumière 
            avatarLight.intensity -= degressivIntensity * Time.deltaTime;
            soundIntensity -= degressivIntensity * Time.deltaTime;
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
        //Debug.Log(DbValue);
        audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);// get sound spectrum
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency
    }
}
