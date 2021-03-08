using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    private float time = 0f;

    private Renderer renderer;//cache meshRenderer of blob
    private GameController controller;  // Cache connection to game controller component



    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Enter()// Overriden from base class.
    {
        renderer = blob.GetComponent<Renderer>();
        renderer.enabled = true;
        controller = blob.GetComponentInParent<GameController>();

    }

    public override void Run()
    {
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
        controller.Score = 1;//adds 1 to the score using setter of Score

    }
}
