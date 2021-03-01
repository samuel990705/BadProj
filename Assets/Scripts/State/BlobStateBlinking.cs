using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    private MeshRenderer meshRen;
    private float timeElapsed;
    public override void Run()
    {
        timeElapsed = Time.time;
        if (timeElapsed>=0.5f)
        {
            //I give the below line credit to an anonymous friend of mine, when I was self talking about the homework in our Space Citizens discord channel
            //he told me there is this easier way to write. This is actually mind blowing for me.
            meshRen.enabled = !meshRen.enabled;
            timeElapsed = 0;
            //Below is my own way of writing it.
            // if (meshRen.enabled == true)
            // {
            //     meshRen.enabled = false;
            // }
            // else
            // {
            //     meshRen.enabled = true;
            // }
        }
    }

    public override void Enter()
    {
        meshRen = blob.GetComponent<MeshRenderer>();
    }
    
    public override void Leave()
    {
        blob.Controller.Score = 1;
    }
}
