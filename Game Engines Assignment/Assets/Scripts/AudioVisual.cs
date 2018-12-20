using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioVisual : MonoBehaviour {
    //Spectrum range for visual to go off
    public float range;
    //Minimum time bewteen beat
    public float time;
    //Speed of the visual to complete
    public float speed;
    //Time between beats
    public float rest;
    //Determine is value is gone above or below the range
    private float lastValue;
    private float value;
    //For time step
    private float timer;
    //Bool keeping track if currently beating
    protected bool beat;

    //Virtual so that it can be overwritten
    public virtual void OnUpdate()
    {
        //Assign previus and current audio values
        lastValue = value;
        value = AudioPlayer.specValue;

        //Check is the audio value went below the range and if a beat can be played based on the time interval
        if(lastValue > range && value <= range)
        {
            if(timer > time)
            {
                OnBeat();
            }
        }
        //Same again if it went below range
        if(lastValue <= range && value > range)
        {
            if(timer > time)
            {
                OnBeat();
            }
        }
        
        timer += Time.deltaTime;
    }

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnBeat()
    {
        timer = 0;
        beat = true;
    }
}
