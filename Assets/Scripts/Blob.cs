﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)
    private BlobStateShrinking StateShrinking;
    private GameController controller;  // Cached connection to game controller component
    public GameController _controller
    {
        get
        {
            return controller;
        }
        set
        {
            controller = value;
        }
    }

    void Start()
    {
        StateShrinking = new BlobStateShrinking(this);
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();

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
        if (currentState != StateShrinking)
        {
            ChangeState(StateShrinking);
        }
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        controller._score = 10; //change the original method to new setter of score
    }
}
