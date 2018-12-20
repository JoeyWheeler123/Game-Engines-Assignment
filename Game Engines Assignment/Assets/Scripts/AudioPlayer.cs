using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public static float specValue { get; private set; }

    private float[] audioSpectrum;

    private void Start()
    {
        audioSpectrum = new float[128];
    }

    private void Update()
    {
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);

        if(audioSpectrum != null && audioSpectrum.Length > 0)
        {
            specValue = audioSpectrum[0] * 100;
        }
    }
}
