using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText, highScoreText;
    private int score;
    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }
    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Faded(gameOver, 1f, 1f));
    }

    public IEnumerator Faded(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapse = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while(elapse < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapse / duration);
            elapse += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(points + score);
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHighScore();
    }
    private void SaveHighScore()
    {
        int highscore = LoadHighScore();
        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }
}
