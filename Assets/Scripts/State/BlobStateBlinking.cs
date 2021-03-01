using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob remains motionless and pulses between two colors.*/
public class BlobStateBlinking : BlobState
{
    // Cached
    private Renderer renderer;

    // Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;
    private float blinkTime;


    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        if (blinkTime >= 0.5f)
        {
            Blink();
        }

        // Return to Moving state after time has elapsed.
        elapsedTime += Time.deltaTime;
        blinkTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }

    }

    public override void Enter() // Overriden from base class.
    {
        base.Enter(); // Call base class.

        renderer = blob.GetComponent<Renderer>(); // Cache the renderer.
        endTime = Random.Range(minTime, maxTime);
        blinkTime = 0f;
    }

    public override void Leave()
    {
        base.Leave();
        renderer.enabled = true;
        blob.controller.Score = 1;
    }

    public void Blink()
    {
        if (renderer.enabled)
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.enabled = true;
        }
        blinkTime = 0f;
    }
}
