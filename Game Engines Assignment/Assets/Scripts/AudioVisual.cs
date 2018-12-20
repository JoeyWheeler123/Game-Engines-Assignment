using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioVisual : MonoBehaviour {

    public float range;
    public float time;
    public float speed;
    public float rest;
    private float lastValue;
    private float value;
    private float timer;
    protected bool beat;

    //Virtual so that it can be overwritten
    public virtual void OnUpdate()
    {
        lastValue = value;
        value = AudioPlayer.specValue;

        if(lastValue > range && value <= range)
        {
            if(timer > time)
            {
                OnBeat();
            }
        }

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
