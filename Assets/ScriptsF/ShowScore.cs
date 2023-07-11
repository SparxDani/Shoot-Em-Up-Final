using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public Text scoreListText;
    public ScoreControl scoreControl; // Asigna una instancia de ScoreControl en el Inspector de Unity o mediante código

    void Start()
    {
        UpdateScoreList();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreList()
    {

        string scoreList = "Top Score:\n";

        for (int i = 0; i < scoreControl.maxScore.Length; i++)
        {
            scoreList += (i + 1) + ". " + scoreControl.maxScore[i] + "\n";
        }

        scoreListText.text = scoreList;
    }

}
