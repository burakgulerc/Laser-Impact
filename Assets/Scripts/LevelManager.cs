using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] float sceneLoadDelay = 2;

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }
    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");         
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOverMenu", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quittin");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }
}
