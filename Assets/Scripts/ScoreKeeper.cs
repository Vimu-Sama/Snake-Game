using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [HideInInspector] public int score = 0;
    [SerializeField] TextMeshProUGUI scoreText; 
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
