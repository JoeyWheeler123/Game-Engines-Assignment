using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScale : AudioVisual {

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (beat)
        {
            return;
        }

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
        Vector3 pos = transform.localScale;
        Vector3 startPos = pos;
        float timer = 0;

        while(pos != target)
        {
            pos = Vector3.Lerp(startPos, target, timer / time);
            timer += Time.deltaTime;
            transform.localScale = pos;
            yield return null;
        }

        beat = false;
    }

    public Vector3 maxScale;
    public Vector3 minScale;

}
