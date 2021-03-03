using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This state class is derived from BlobState.
 * In this state, the blob shrinks to zero size and is destroyed.*/
public class BlobStateBlinking : BlobState
{
    private float timer;
    // Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;


    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {
        

    }

    public override void Run() // Overriden from base class.
    {
        

            timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            if (blob.GetComponent<Renderer>().enabled == false)
            {
                blob.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                blob.GetComponent<Renderer>().enabled = false;
            }
            timer = 0f;
        }


        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }




    }

    public override void Enter() // Overriden from base class.
    {
        base.Enter();
        endTime = Random.Range(minTime, maxTime);


    }

    public override void Leave()
    {

        GameController control1 = blob.GetComponentInParent<GameController>();
        control1.Score = 1;
        
    }

   
}