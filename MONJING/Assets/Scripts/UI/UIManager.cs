using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private Button restartButton;
    [Header("MainGame")]
    [SerializeField] private TMP_Text main_scoreText;
    [Header("Player")]
    [SerializeField] private GameObject sitPlayer;
    [SerializeField] private GameObject standPlayer;
    [Header("Prologue")]
    [SerializeField] private GameObject dragText;
    [SerializeField] private GameObject finger;
    public static int score;
    public GameObject arrow;
    [Header("Sound")]
    [SerializeField] private GameObject onMusic;
    [SerializeField] private GameObject offMusic;
    private void Start()
    {
        score = 0;
        UpdateScoreText(); // 시작할 때 점수 텍스트 업데이트

        // 기존 하이스코어 불러오기
        int savedHighscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = savedHighscore.ToString();
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        // 하이스코어 업데이트
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = score.ToString();
        }
    }
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
        main_scoreText.text = score.ToString();
    }

    public void forRestartButton()
    {
        gameOverPanel.gameObject.SetActive(false);
        EnemyManager.isGameOver = false;
        // 인터넷 연결이 안되어있는 경우
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
        else // 인터넷 연결이 되어있는 경우
        {
            admob.Instance.ShowAd();
        }
    }

    public void setStandPlayer()
    {
        sitPlayer.SetActive(false);
        standPlayer.SetActive(true);
    }

    public void setSitPlayer()
    {
        sitPlayer.SetActive(true);
        standPlayer.SetActive(false);
    }

    public void offPrologue()
    {
        finger.SetActive(false);
        dragText.SetActive(false);
    }

    public void onPrologue()
    {
        finger.SetActive(true);
        dragText.SetActive(true);
        arrow.SetActive(true);
    }

    public void onMusicobject()
    {
        onMusic.SetActive(true);
        offMusic.SetActive(false);
    }

    public void offMusicobject()
    {
        onMusic.SetActive(false);
        offMusic.SetActive(true);
    }
}
