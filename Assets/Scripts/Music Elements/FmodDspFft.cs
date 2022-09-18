//--------------------------------------------------------------------
//
// This is a Unity behaviour script that demonstrates how to access
// the Core API channel group of a bus, and how to create and
// add an FFT DSP. 
//
// This document assumes familiarity with Unity scripting. See
// https://unity3d.com/learn/tutorials/topics/scripting for resources
// on learning Unity scripting. 
//
//--------------------------------------------------------------------

using System;
using UnityEngine;
using System.Runtime.InteropServices;

class FmodDspFft : MonoBehaviour
{
    private FMOD.DSP mFFT;
    private LineRenderer mLineRenderer;
    private float[] mFFTSpectrum;
    const int WindowSize = 126;
    private FMODUnity.EventReference eventName;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;

#if UNITY_EDITOR
    void Reset()
    {
        eventName = FMODUnity.EventReference.Find("event:/BackgroundTrack");
    }
#endif

    void Start()
    {
        mLineRenderer = gameObject.AddComponent<LineRenderer>();
        mLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        mLineRenderer.widthMultiplier = 0.2f;
        mLineRenderer.positionCount = WindowSize;
        mLineRenderer.startWidth = mLineRenderer.endWidth = .1f;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        mLineRenderer.colorGradient = gradient;

        // Create a DSP of DSP_TYPE.FFT
        if (FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out mFFT) == FMOD.RESULT.OK)
        {
            mFFT.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.HANNING);
            mFFT.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WindowSize * 2);
            FMODUnity.RuntimeManager.StudioSystem.flushCommands();

            // Get the master bus (or any other bus for that matter)
            FMOD.Studio.Bus selectedBus = FMODUnity.RuntimeManager.GetBus("bus:/");
            if (selectedBus.hasHandle())
            {
                // Get the channel group
                FMOD.ChannelGroup channelGroup;
                if (selectedBus.getChannelGroup(out channelGroup) == FMOD.RESULT.OK)
                {
                    // Add fft to the channel group
                    if (channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.HEAD, mFFT) != FMOD.RESULT.OK)
                    {
                        Debug.LogWarningFormat("FMOD: Unable to add mFFT to the master channel group");
                    }
                }
                else
                {
                    Debug.LogWarningFormat("FMOD: Unable to get Channel Group from the selected bus");
                }
            }
            else
            {
                Debug.LogWarningFormat("FMOD: Unable to get the selected bus");
            }
        }
        else
        {
            Debug.LogWarningFormat("FMOD: Unable to create FMOD.DSP_TYPE.FFT");
        }
    }

    void OnDestroy()
    {
        FMOD.Studio.Bus selectedBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        if (selectedBus.hasHandle())
        {
            FMOD.ChannelGroup channelGroup;
            if (selectedBus.getChannelGroup(out channelGroup) == FMOD.RESULT.OK)
            {
                if(mFFT.hasHandle())
                {
                    channelGroup.removeDSP(mFFT);
                }
            }
        }
    }

    const float WIDTH = 10.0f;
    const float HEIGHT = 0.1f;

    void Update()
    {
        if (mFFT.hasHandle())
        {
            IntPtr unmanagedData;
            uint length;
            if (mFFT.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length) == FMOD.RESULT.OK)
            {
                FMOD.DSP_PARAMETER_FFT fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
                if (fftData.numchannels > 0)
                {
                    if (mFFTSpectrum == null)
                    {
                        // Allocate the fft spectrum buffer once
                        for (int i = 0; i < fftData.numchannels; ++i)
                        {
                            mFFTSpectrum = new float[fftData.length];
                        }
                    }
                    fftData.getSpectrum(0, ref mFFTSpectrum);

                    var pos = Vector3.zero;
                    pos.x = WIDTH * -0.5f;

                    for (int i = 0; i < WindowSize; ++i)
                    {
                        pos.x += (WIDTH / WindowSize);

                        float level = lin2dB(mFFTSpectrum[i]);
                        pos.y = (80 + level) * HEIGHT;

                        mLineRenderer.SetPosition(i, pos);
                    }
                }
            }
        }
    }

    private float lin2dB(float linear)
    {
        return Mathf.Clamp(Mathf.Log10(linear) * 20.0f, -80.0f, 0.0f);
    }
}