using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derive audioVisual
public class AudioScale : AudioVisual {

    public override void OnUpdate()
    {
        base.OnUpdate();
        //Checking is we are in a beat step
        if (beat)
        {
            return;
        }
        //If not in a beat, lerp towards minScale
        transform.localScale = Vector3.Lerp(transform.localScale, minScale, rest * Time.deltaTime);

    }

    public override void OnBeat()
    {
        base.OnBeat();
        StopCoroutine("ScaleUp");
        StartCoroutine("ScaleUp", maxScale);
    }

    private IEnumerator ScaleUp(Vector3 target)
    {
        //Set current transform of the objects scale
        Vector3 pos = transform.localScale;
        //Starting vector is set as the current position
        Vector3 startPos = pos;
        //Timer to track how far away from each other they are
        float timer = 0;
        //While the scale of the object is not the target scale, it lerps towards it
        while(pos != target)
        {
            pos = Vector3.Lerp(startPos, target, timer / time);
            //Increment timer
            timer += Time.deltaTime;
            //Set the scale of the object to the current scale after lerp
            transform.localScale = pos;
            yield return null;
        }

        beat = false;
    }

    //When a beat occurs it will go to mas scale
    public Vector3 maxScale;
    //When it is finished it will return to minScale
    public Vector3 minScale;

}
