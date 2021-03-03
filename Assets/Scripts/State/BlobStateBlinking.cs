using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobStateBlinking : BlobState
{
    //Time until state change
    private const float minTime = 1.0f;
    private const float maxTime = 5.0f;
    private float endTime;
    private float blinkTime;
    //Renderer of the blob
    private Renderer renderer;
    public BlobStateBlinking(Blob theBlob) : base(theBlob) // Derived class constructor calls base class constructor.
    {

    }

    public override void Run()
    {
        blinkTime += Time.deltaTime;
        if (blinkTime >= endTime)
        {
            //Stop the coroutine, otherwise it will blink on and off forever.
            blob.StopAllCoroutines();
            blob.ChangeState(new BlobStateMoving(blob));
        }
    }

    public override void Enter()
    {
        base.Enter(); // Call base class.

        //Set end time to a random number between min and max time.
        endTime = Random.Range(minTime, maxTime);
        //Get renderer of blob
        renderer = blob.GetComponent<Renderer>();

        blob.StartCoroutine(BlinkOnOff());//Start the coroutine here, otherwise it will start multiple times and create chaos.
    }

    public override void Leave()
    {
        base.Leave();//Call base class
        //Set the transparent alpha back to 1, just in case it leaves this state while transparent.
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);

        //Add 1 point when leaving blinking state
        GameObject.Find("Ground").GetComponent<GameController>().Score = 1;
    }
    //The IEnumerator is used to calculate time 0.5 seconds.
    IEnumerator BlinkOnOff()
    {
        /*I tried to use setActive, but realized that the state is controlled by these prefabs. If they are setAcitve to false, the state just freeze.
        * Therefore, I set the color to transparent, but you can still see the structure of the prefabs since transparent works like that.
        */
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0f);
        Debug.Log("Fade out");
        yield return new WaitForSeconds(0.5f);
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
        Debug.Log("Fade in");
        yield return new WaitForSeconds(0.5f);
        blob.StartCoroutine(BlinkOnOff());
    }
}
