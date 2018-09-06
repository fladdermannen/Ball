using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglerController : MonoBehaviour {

    public List<GameObject> jugglers = new List<GameObject>();
    public List<Transform> positions;

    GameObject left;
    GameObject mid;
    GameObject right;
    int position = 1;
    [HideInInspector]
    public bool inputLock;



    private void OnEnable()
    {
        Input.OnLeftPressed += Input_OnLeftPressed;
        Input.OnRightPressed += Input_OnRightPressed;

        left = jugglers[0].gameObject;
        mid = jugglers[1].gameObject;
        right = jugglers[2].gameObject;

        left.SetActive(false);
        mid.SetActive(true);
        right.SetActive(false);

        inputLock = false;

    }

    private void OnDisable()
    {
        Input.OnLeftPressed -= Input_OnLeftPressed;
        Input.OnRightPressed -= Input_OnRightPressed;
    }



    private void Input_OnRightPressed()
    {
        //Debug.Log("Right pressed");
        if (!inputLock)
        {
            if (StanceDance(1))
                position++;
        }
    }

    private void Input_OnLeftPressed()
    {
        //Debug.Log("Left pressed");
        if (!inputLock)
        {
            if (StanceDance(-1))
                position--;
        }
    }



    bool StanceDance(int direction)
    {
        switch(direction)
        {
            case -1:
                if (position == 2)
                {
                    right.SetActive(false);
                    mid.SetActive(true);
                }
                else if (position == 1)
                {
                    mid.SetActive(false);
                    left.SetActive(true);
                }
                else
                    return false;
                break;
            case 1:
                if (position == 0)
                {
                    left.SetActive(false);
                    mid.SetActive(true);
                }
                else if (position == 1)
                {
                    mid.SetActive(false);
                    right.SetActive(true);
                }
                else
                    return false;
                break;

        }
        return true;
    }

    public void ResetJuggler()
    {
        left.SetActive(false);
        mid.SetActive(true);
        right.SetActive(false);
        inputLock = false;
    }
}
