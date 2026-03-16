using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    private Player player;
    private Spawner spawner;
    [SerializeField] private ShakeData deathShakeData;
    [SerializeField] private AudioClip deathClip;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public Button retryButton;

    private float score;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        } else 
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }
    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        CameraShakerHandler.Shake(deathShakeData);

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        AudioManager.instance.PlaySoundFXClip(deathClip, transform, 1f);
    }
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        UpdateBestScore();
    }
    private void UpdateBestScore()
    {
        float hiScore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiScore)
        {
            hiScore = score;
            PlayerPrefs.SetFloat("hiscore", hiScore);
        }
        bestScoreText.text = Mathf.FloorToInt(hiScore).ToString("D5");
    }
}
