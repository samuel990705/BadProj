using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CHANGE: Introduced 
public class BlobStateBlinking : BlobState
{
    private const float blinkTime = 0.5f;//how often the blob will blink on and off
    private float elapsedTime;//How much time has passed since the blob last blinked
    private Renderer blobRenderer;
    public BlobStateBlinking(Blob theBlob): base(theBlob) {

    }

    public override void Run()
    {
        elapsedTime += Time.deltaTime;
        //If the time has reached the blink time, it blinks on/off
        if(elapsedTime >= blinkTime) {
            blobRenderer.enabled = !blobRenderer.enabled;
            elapsedTime = 0;
        }
    }

    public override void Enter()
    {
        base.Enter();
        //Gets the renderer of the blob, so that it can blink on/off
        blobRenderer = blob.GetComponent<Renderer>();
    }
}
