using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    private float time = 0f;

    private Renderer renderer;//cache meshRenderer of blob
    private GameController controller;  // Cache connection to game controller component

    // Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float elapsedTime;
    private float endTime;



    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Enter()// Overriden from base class.
    {
        renderer = blob.GetComponent<Renderer>();
        renderer.enabled = true;
        controller = blob.GetComponentInParent<GameController>();

        endTime = Random.Range(minTime, maxTime);

    }

    public override void Run()
    {

        //change to back to moving state after a while
        elapsedTime += Time.deltaTime;

        if (elapsedTime > endTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
        }



        //make object blink
        time += Time.deltaTime;

        if (time > 0.5f)//invisible
        {
            renderer.enabled = false;
        }

        if(time>1.0f)//visible
        {
            renderer.enabled = true;
            time = 0;//reset time
        }


    }

    public override void Leave() // Overriden from base class.
    {
        renderer.enabled = true;//set renderer.enabled to true when state ends
        controller.Score = 1;//adds 1 to the score using setter of Score

    }
}
