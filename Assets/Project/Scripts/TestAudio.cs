using System;
using UnityEngine;
using System.Runtime.InteropServices;

class TestAudio : MonoBehaviour
{
    public Decibel db;
    public FMOD.Studio.EventInstance musicInstance;
    FMOD.DSP fft;
    const int WindowSize = 1024;

    Light selfLight;
    float level = -80;
    public float diviseur = 50;
    public float degressif = 2;
    public float maxIntensity = 5;
    
    void Start()
    {
        selfLight = GetComponent<Light>();
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/01_A_IMPLEMENTER/Walk_01");
        musicInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out fft);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.HANNING);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WindowSize * 2);

        FMOD.ChannelGroup channelGroup;
        FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out channelGroup);
        channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.HEAD, fft);

    }

    const float WIDTH = 10.0f;
    const float HEIGHT = 0.1f;
    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    void Update()
    {
        if(IsPlaying(musicInstance))
        {
            IntPtr unmanagedData;
            uint length;
            fft.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
            FMOD.DSP_PARAMETER_FFT fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
            var spectrum = fftData.spectrum;

            if (fftData.numchannels > 0)
            {
                for (int i = 0; i < WindowSize; ++i)
                {
                    level = lin2dB(spectrum[0][i]);
                    if (selfLight.intensity < maxIntensity)
                        selfLight.intensity += (level + 80) / diviseur;
                    
                }
            }
           
        }
        db.decibel = level;
        selfLight.range = selfLight.intensity * 5;
        gameObject.GetComponent<SphereCollider>().radius = selfLight.range;
        if (selfLight.intensity > 0)
            selfLight.intensity -= degressif * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        musicInstance.start();
    }
    float lin2dB(float linear)
    {
        return Mathf.Clamp(Mathf.Log10(linear) * 20.0f, -80.0f, 20.0f);
    }
}