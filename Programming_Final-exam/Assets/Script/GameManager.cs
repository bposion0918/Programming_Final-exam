using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public Transform deadZoneY; // 낙사 기준 높이 역할을 할 오브젝트나 Y값 대신 사용

    public float timeLimit = 60f; // 제한 시간 1분
    private int currentScore = 0;
    private float timeLeft;
    private bool isGameOver = false;

    private Transform playerTransform;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timeLeft = timeLimit;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            // 게임오버 상태에서 R키를 누르면 재시작
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }

        // 1. 타이머 감소 처리
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            TriggerGameOver();
        }

        // 2. Y축 추락(낙사) 감지 (예: 플레이어 Y값이 -10 이하로 떨어지면)
        if (playerTransform != null && playerTransform.position.y < -10f)
        {
            TriggerGameOver();
        }

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        currentScore += amount;
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft).ToString() + "s";
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // 게임오버 화면 켜기
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void WinGame()
    {
        isGameOver = true;
        // 과제 필수 요구사항에는 승리 연출이 없으므로, 간단히 멈추거나 게임오버 텍스트만 "Clear!"로 변경해 재활용 가능합니다.
        scoreText.text = "STAGE CLEAR! Final Score: " + currentScore;
        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        // 씬 재시작 시 시간 흐름 복구
        Time.timeScale = 1f;
    }
}