using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour {

    TextMeshPro textMesh;


    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();

        if(textMesh == null)
        {
            Debug.LogError("Textmesh component not found");
        }
    }

    public void SetPoints(int score)
    {
        textMesh.SetText(score.ToString());
    
    }

}
