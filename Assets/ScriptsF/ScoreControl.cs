using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "PuntajeSO", menuName = "Puntajes/Nuevo Adm. Puntajes", order = 1)]
public class ScoreControl : ScriptableObject
{
    [SerializeField] public int[] topScore;
    int k = 0;
    public void OnEnable()
    {
        if (topScore == null)
        {
            topScore = new int[5];
        }
    }
    public void RegistryNewScore(int newScore)
    {
        if (k < 10)
        {
            topScore[k] = newScore;
            k++;
        }
        else
        {
            if (newScore > topScore[0])
            {
                topScore[0] = newScore;
            }
        }
        SelectionSortOrden();
    }
    public void SelectionSortOrden()
    {
        for (int i = 0; i < topScore.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < topScore.Length; j++)
            {
                if (topScore[j] < topScore[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int tmp = topScore[i];
                topScore[i] = topScore[minIndex];
                topScore[minIndex] = tmp;
            }
        }
    }
}