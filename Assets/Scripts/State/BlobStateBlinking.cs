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

        blob.StartCoroutine(Blinking());
    }

    public override void Leave()
    {
        blob.StopAllCoroutines();
        blob.GetComponent<Renderer>().enabled = !blob.GetComponent<Renderer>().enabled;
        blob._controller._score = 1;
      
    }

    public IEnumerator Blinking()
    {
        yield return new WaitForSeconds(0.5f);
        blob.GetComponent<Renderer>().enabled = !blob.GetComponent<Renderer>().enabled;
        blob.StartCoroutine(Blinking());
    }

}
