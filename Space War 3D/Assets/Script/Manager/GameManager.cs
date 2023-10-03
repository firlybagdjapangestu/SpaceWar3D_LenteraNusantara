using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Enemy and Player")]
    [SerializeField] private EnemyManager enemy = null;
    [SerializeField] private PlayerManager player = null;

    [Header("Game Variables")]
    public int score;
    public int highScore;
    public int enemyDestroyed;
    public int currentEnemy;
    public int levelGame;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefabs;
    public Transform[] spawnPoint;
    public int pickSpawnPoint;
    public float timeBetweenWaves;
    public float spawnInterval;
    public float enemyPerWave;
    public Transform[] stayPosition;
    [SerializeField] private int[] originalArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    [SerializeField] private int[] shuffledArray;
    public int pickRandomEnemy;
    public int howMuchTypeEnemy;

    [Header("Player Leveling")]
    public int expPlayer;
    public int targetExp;
    public int levelPlayer;
    public int maxLevel;

    [Header("Enemy Leveling")]
    public float increaseHealth;
    public float increaseSpeed;

    [Header("Game Manager")]
    private bool isPaused = false;
    public AudioSource audioSource;
    public AudioSource backgroundMusic;
    public AudioClip[] audioClips;
    public Camera mainCamera; // Referensi ke komponen Camera
    private Vector3 originalPosition;


    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreResultText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image levelFill;
    [SerializeField] private GameObject bannerExit;
    [SerializeField] private GameObject bannerScore;

    private void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            bannerExit.SetActive(true);
        }
    }

    #region Start and Enable/Disable
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        StartCoroutine(StartWaves());
        shuffledArray = Shuffle(originalArray);
        mainCamera = Camera.main; 
        originalPosition = mainCamera.transform.localPosition;
    }

    private void OnEnable()
    {
        player.OnPlayerDeath += OnDeathEffect;
        player.OnPlayerGetExp += OnGetExp;
    }

    private void OnDisable()
    {
        player.OnPlayerDeath -= OnDeathEffect;
        player.OnPlayerGetExp -= OnGetExp;
    }
    #endregion

    #region Event
    private void OnHitEffect()
    {
        // effect jika terkena hit
    }

    private void OnDeathEffect()
    {
        bannerScore.SetActive(true);
        backgroundMusic.mute = true;
        scoreResultText.text = "Score : " + score.ToString();
        highScoreText.text = "High Score : " + highScore.ToString();
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        Time.timeScale = 0;
    }

    public void OnEnemyDestroyed(Transform enemyPosition)
    {
        enemyDestroyed++;
        score += 500;
        scoreText.text = score.ToString();
        GameObject weaponItemPool = PoolManager.Instance.GetWeaponPooled();
        Shake(1f);
        if (weaponItemPool != null)
        {
            weaponItemPool.transform.position = enemyPosition.transform.position;
            weaponItemPool.transform.rotation = enemyPosition.transform.rotation;
            weaponItemPool.SetActive(true);
        }
        
        for (int i = 0; i < 3; i++)
        {
            GameObject expItemPool = PoolManager.Instance.GetExpPooled();           
            if (expItemPool != null)
            {
                expItemPool.transform.position = enemyPosition.transform.position;
                expItemPool.transform.rotation = enemyPosition.transform.rotation;
                expItemPool.SetActive(true);
            }
            expItemPool.GetComponent<ExpBehavior>().Active();
        }
    }

    public void OnGetExp()
    {
        expPlayer += 10;
        levelFill.fillAmount = (float)expPlayer / (float)targetExp;
        audioSource.PlayOneShot(audioClips[2]);
        if (expPlayer > targetExp && levelPlayer < maxLevel)
        {
            StartCoroutine(TextInfo("Level Up"));
            levelPlayer++;
            targetExp += targetExp;
            expPlayer = 0;
            levelText.text = (levelPlayer + 1).ToString();
            player.bulletManager.ChangeWeapon();
            levelFill.fillAmount = (float)expPlayer / (float)targetExp;
        }
        
    }

    public void OnShootEffect()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void OnGetWeapon()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }

    public void Shake(float strength)
    {
        StartCoroutine(ShakeCoroutine(strength));
    }

    private IEnumerator ShakeCoroutine(float strength)
    {
        float shakeDuration = 0.5f;
        float shakeMagnitude = strength;

        while (shakeDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            mainCamera.transform.localPosition = originalPosition + randomOffset;

            shakeDuration -= Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition;
    }

    private IEnumerator TextInfo(string info)
    {
        infoText.text = info;
        infoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        infoText.gameObject.SetActive(false);
    }
    #endregion

    #region Wave Spawning
    IEnumerator StartWaves()
    {
        print("New Wave");
        yield return new WaitForSeconds(timeBetweenWaves);
        while (currentEnemy < enemyPerWave)
        {
            currentEnemy++;
            pickSpawnPoint = Random.Range(0, spawnPoint.Length);
            GameObject enemyPool = PoolManager.Instance.GetEnemyPooled();
            enemyPool.GetComponent<EnemyManager>().ActivateEnemy();
            if (enemyPool != null)
            {  
                enemyPool.transform.position = spawnPoint[pickSpawnPoint].position;
                enemyPool.transform.rotation = spawnPoint[pickSpawnPoint].rotation;
                enemyPool.SetActive(true);
            }
            enemyPool.GetComponent<EnemyStateManager>().ActivateEnemy();           
            yield return new WaitForSeconds(spawnInterval);
        }
        while (enemyDestroyed % enemyPerWave != 0)
        {
            yield return null;
        }
        
        levelUp();
        print("You Finished the wave");
        StartCoroutine(TextInfo("Next Wave"));
        pickRandomEnemy = Random.Range(0, howMuchTypeEnemy);
        currentEnemy = 0;
        shuffledArray = Shuffle(originalArray);
        StartCoroutine(StartWaves());
    }
    void levelUp()
    {
        increaseHealth += increaseHealth;
        increaseSpeed += increaseSpeed;
    }
    #endregion

    #region Array Shuffling
    // Fungsi untuk melakukan pengacakan menggunakan algoritma Fisher-Yates Shuffle.
    private int[] Shuffle(int[] arr)
    {
        int[] shuffled = new int[arr.Length];
        arr.CopyTo(shuffled, 0);

        for (int i = 0; i < shuffled.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Length);
            int temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }

        return shuffled;
    }
    #endregion

    #region Game Managing
    public void PauseGame()
    {
        if (isPaused)
        {
            // Unpause permainan
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            // Pause permainan
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
        
    }
    #endregion


}
