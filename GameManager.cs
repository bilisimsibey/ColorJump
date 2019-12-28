using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject touchToStartObj;
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public void GameOver()
    {
        StartCoroutine(gameOverCoroutine());
       
    }
    IEnumerator gameOverCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(0.1f);
        GetComponent<scoreManager>().currentScoreText.color = Color.white;
        GetComponent<scoreManager>().bestScoreText.color = Color.white;
        GetComponent<scoreManager>().best.color = Color.white;
        gameOverPanel.SetActive(true);
        yield break;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        touchToStartObj.SetActive(false);
    }
    
}
