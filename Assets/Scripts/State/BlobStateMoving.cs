using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob moves in a sine wave pattern.*/
public class BlobStateMoving : BlobState
{
    // Sine wave definitions.
    public const float speed = 0.01f;
    public const float amplitude = 0.5f;
    public const float sinSpeed = 2.0f;
    public const float minTime = 1.0f;
    public const float maxTime = 5.0f;

    // Movement over time.
    private Vector3 curPos;
    private float elapsedTime;
    private float endTime;
    private Vector3 direction;

    public BlobStateMoving(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        // Regular movement
        curPos += Time.deltaTime * direction; 

        // Sine movement
        elapsedTime += Time.deltaTime;
        Vector3 offset = new Vector3(0.0f, amplitude * Mathf.Sin(elapsedTime * sinSpeed), 0.0f);
        blob.transform.position = curPos + offset;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStatePulsing(blob)); // Change to color pulsing state.
        }
    }
    
    
    /*
     * The Enter() in this function didn't override the BlobState.Enter().
     * So when in script Blob, on line 30, currentState.Enter(), if the state is moving.
     * It won't actually call the Enter() in this script, but calling BlobState.Enter(), which has nothing.
     * So the blob won't move at all.
     */
    public override void Enter() // Overriden from base class.
    {
        base.Enter(); // Call base class.

        curPos = blob.transform.position;
        endTime = Random.Range(minTime, maxTime);
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f)); // Move in random direction.
    }
}
