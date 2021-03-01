using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    private Renderer r;
    private float timer;
    private GameController controller;

    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run() // Overriden from base class.
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            timer = 0.0f;
            r.enabled = !r.enabled;
        }
    }

    public override void Enter() // Overriden from base class.
    {
        timer = 0.0f;
        r = blob.GetComponent<Renderer>();
        controller = GameObject.Find("Ground").GetComponent<GameController>();
    }

    public override void Leave()
    {
        controller.score = 1;

    }

}
