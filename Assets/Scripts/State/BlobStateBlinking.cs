using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{

    
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run()
    {

    }

    public override void Enter()
    {
        base.Enter();
        blob.StartCoroutine(Thelink());
    }

    public override void Leave()
    {
        base.Leave();
        blob.Controller.Score = 1;
    }

    IEnumerator Thelink()
    {
        yield return new WaitForSeconds(.5f);

        blob.GetComponent<MeshRenderer>().enabled = !blob.GetComponent<MeshRenderer>().enabled;

        blob.StartCoroutine(Thelink());
        //yield return null;
    }
}
