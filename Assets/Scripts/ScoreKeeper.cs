using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [HideInInspector] public int score = 0;
    public bool isGameOver = false;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText.text = "Score: " + score;
    }
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
