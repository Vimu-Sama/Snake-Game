using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] ManageBody manageBody;
    bool onlyexecuteonce = false;

    private void Update()
    {
        if(manageBody.isDestroying)
            gameOverPanel.SetActive(true);
    }

}
