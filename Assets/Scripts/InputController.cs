using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;


public class InputController : MonoBehaviour {

    private TapGestureRecognizer tapGesture;
    private PanGestureRecognizer panGesture;
    private bool dragging = false;
    private Vector3 offset;

    public bool left;

    public delegate void ButtonPressed();
    public static event ButtonPressed OnLeftPressed;
    public static event ButtonPressed OnRightPressed;

    private void Start()
    {
        tapGesture = new TapGestureRecognizer();
        tapGesture.ThresholdSeconds = 0.3f;
        tapGesture.PlatformSpecificView = gameObject;
        tapGesture.StateUpdated += TapGesture_StateUpdated;
        FingersScript.Instance.AddGesture(tapGesture);

        panGesture = new PanGestureRecognizer();
        panGesture.ThresholdUnits = 0.5f;
        panGesture.PlatformSpecificView = gameObject;
        //panGesture.StateUpdated += PanGesture_StateUpdated;
        FingersScript.Instance.AddGesture(panGesture);


    }

    void TapGesture_StateUpdated(GestureRecognizer gesture)
    {

        if (gesture.State == GestureRecognizerState.Ended)
        {
            Vector2 pos = new Vector2(gesture.FocusX, gesture.FocusY);
            pos = Camera.main.ScreenToWorldPoint(pos);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (OnLeftPressed != null && left)
                    OnLeftPressed();
                else if (OnRightPressed != null)
                    OnRightPressed();
            }

        }
    }


    /*
    void PanGesture_StateUpdated(GestureRecognizer gesture)
    {

       switch(gesture.State)
       {
           case GestureRecognizerState.Began:
                Vector2 pos = new Vector2(gesture.FocusX, gesture.FocusY);
                pos = Camera.main.ScreenToWorldPoint(pos);

                offset = transform.position - (Vector3)pos;

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Dragging");
                    dragging = true;
                }
                break;
           case GestureRecognizerState.Executing:
                if(dragging)
                {
                    Vector2 newpos = new Vector2(gesture.FocusX, gesture.FocusY);
                    newpos = Camera.main.ScreenToWorldPoint(newpos);

                    transform.position = (Vector3) newpos + offset;
                }
               break;
           case GestureRecognizerState.Ended:
                if (dragging)
                    dragging = false;
               break;
            
       }

    }
    */



}
