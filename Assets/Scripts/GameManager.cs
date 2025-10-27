using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveManager.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveManager.LoadData();
        }
    }

    [SerializeField] TextMeshProUGUI UIText;

    public int CurrentScore;

    public void UpdateScore(int score)
    {
        CurrentScore += score;
        UIText.text = (CurrentScore.ToString());
    }
}
