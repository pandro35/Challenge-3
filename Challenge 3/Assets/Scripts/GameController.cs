using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public BGScroller bgscroller;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float speedWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text winText;
    public int score;

    private bool gameOver;
    private bool restart;

    public AudioSource BGM;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    public GameObject slowStars;
    public GameObject fastStars;
    public GameObject bigStars;

    void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }
        }

        if (Input.GetKey(KeyCode.H))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'Space' for Restart";
                restart = true;
                break;
            }
        }
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You win!";
            gameOver = true;
            restart = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        if (score >= 100)
        {
            GameOverText.text = "Game Created by Alejandro Porras. Win Music by Patrick de Arteaga";
            bgscroller.scrollSpeed = -50;
            slowStars.SetActive(false);
            fastStars.SetActive(true);
            BGM.Stop();
            BGM.clip = winMusic;
            BGM.Play();
        }
        else
        {
            GameOverText.text = "Game Over! Created by Alejandro Porras. Lose Music by Patrick de Arteaga";
            bgscroller.scrollSpeed = 0;
            slowStars.SetActive(false);
            bigStars.SetActive(true);
            BGM.Stop();
            BGM.clip = loseMusic;
            BGM.Play();
        }
        gameOver = true;
    }
}
