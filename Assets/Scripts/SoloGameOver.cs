using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SoloGameOver : MonoBehaviour
{
    [SerializeField] ScoreKeeper scoreKeeper;

    private void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Your Score: " + scoreKeeper.score;
    }

}
