using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public static float specValue { get; private set; }
    //Array of floats for beats
    private float[] audioSpectrum;

    private void Start()
    {
        //Initialise array power of 2
        audioSpectrum = new float[128];
    }

    private void Update()
    {
        //Fill array with spectrum data
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);

        //Simple check to see if there is any data in the array
        if(audioSpectrum != null && audioSpectrum.Length > 0)
        {
            //Assign a general value and multiply by 100(doesn't have to be 100, can be anything)
            specValue = audioSpectrum[0] * 100;
        }
    }
}
