using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variables
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public int lives = 3;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI liveText;
    public GameObject pausedMenu;
    public bool isPaused = false;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0 || score < 0) // If lives are less than or equal to 0 or score is less than 0, then it's game over
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Pauses the game when escape key is pressed
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
            pausedMenu.SetActive(!isPaused);
            isPaused = !isPaused;
        }

    }

    IEnumerator SpawnTarget() //Spawns in targets
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            
        }
    }

    public void UpdateScore(int scoreToAdd) //Updates the score when the player hits one of the targets
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLive() // Updates the lives whenever the player misses one of the targets and falls below
    {
        lives -= 1;
        liveText.text = "Lives: " + lives;
    }

    public void GameOver() // Sets the game over UI
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() // Restarts game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty) // Starts the game functions
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        spawnRate /= difficulty;
    }
}
