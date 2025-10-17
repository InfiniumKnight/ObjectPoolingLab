using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI UIText;
    int CurrentScore;

    public void UpdateScore(int score)
    {
        CurrentScore += score;
        UIText.text = (CurrentScore.ToString());
    }
}
