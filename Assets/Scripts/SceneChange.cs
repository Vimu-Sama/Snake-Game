using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = 2.5f;
    }
}
