using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* This state class is derived from BlobState.
 * the blob should blink on and off with a delay of half a second between blinks. */
public class BlobStateBlinking : BlobState
{

    // Cached
    private Renderer renderer;

    // Blink variables
    private float blinkBetween=0.5f;
    private float blinkDuration=0.5f;
    private float blinkCounter;
    //Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        blinkCounter += Time.deltaTime;
        //if not in off state
        if (renderer.enabled)
        {
            if (blinkCounter >= blinkBetween)
            {
                renderer.enabled = false;
                blinkCounter = 0;
            }
        }
        else
        {
            if (blinkCounter >= blinkDuration)
            {
                renderer.enabled = true;
                blinkCounter = 0;
            }
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            //changed to blinking state
            blob.ChangeState(new BlobStateMoving(blob));
        }
    }

    public override void Enter() // Overriden from base class.
    {
        base.Enter(); // Call base class.

        renderer = blob.GetComponent<Renderer>(); // Cache the renderer.
        renderer.enabled = false;
        endTime = Random.Range(minTime, maxTime);
    }

    public override void Leave()
    {
        base.Leave();
        GameController controller = blob.GetComponentInParent<GameController>();
        controller.Score = 1;//add score for leaving blink
        renderer.enabled = true;
    }
}

