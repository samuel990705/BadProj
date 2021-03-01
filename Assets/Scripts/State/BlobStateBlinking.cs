using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    
    Color curr = new Color(0,0,0,1); // starts off as black
    bool swap = false;

    // Cached
    private Renderer renderer;
    private GameController controller;

    // Time until state change
    private float elapsedTime;
    private float endTime = .5f;
    private float totalTime = 0f;
    private float closingTime = 5f;


    public override void Run() // Overriden from base class.
    {
        // Switch between blinking colors.
        if(elapsedTime > endTime)
        {
            switchColors();
        }


        renderer.material.SetColor("_Color", curr);

        
        elapsedTime += Time.deltaTime;
        // since it did not say how long we're in blinking, i set it to five seconds
        totalTime += Time.deltaTime;

        if(totalTime > closingTime)
        {
            blob.ChangeState(new BlobStateMoving(blob));
            Debug.Log("Here");
        }

    }

    public override void Enter() // Overriden from base class.
    {
        base.Enter(); // Call base class.
        renderer = blob.GetComponent<Renderer>(); // Cache the renderer.
        controller = blob.GetComponent<GameController>();
        elapsedTime = 0f; // reset timer for blinking
        
     }

    private void switchColors()
    {
        if(swap){
            swap = false;
            curr = new Color(1, 1, 1, 1); // swap to white
        } else
        {
            swap = true;
            curr = new Color(0, 0, 0, 1); //swap to black
        }
        elapsedTime = 0f;
    }

    override public void Leave()
    {
        controller = blob.GetComponentInParent<GameController>();
        controller.Score = 1;
    }


}
