using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)    
    
    //CHANGE:
    private BlobStateShrinking stateShrinking;//A private instance of this object's shrinking state, to check whether it's already in it
    private GameController controller;  // Cached connection to game controller component

    void Start()
    {
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();
        //CHANGE:
        stateShrinking = new BlobStateShrinking(this);//Initialize's a blob state shrinking at start, for when it needs to check if its already moving
    }

    void Update()
    {
        currentState.Run(); // Run state update.
    }

    // Change state.
    public void ChangeState(BlobState newState)
    {
        
        if (currentState != null) currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    // Change blobs to shrinking state when clicked.
    void OnMouseDown()
    {
        //CHANGE: Now, the blob only changes state if they aren't already in a shrinking state
        if(currentState != stateShrinking) {
            //Also means it doesn't have to initialize a new state everytime it calls this
            ChangeState(stateShrinking); 
        }
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        //CHANGE: Because of how we are now using a getter/setter to set score, I changed up how you get the score a little bit
        controller.Score = controller.Score + 10;
    }
}
