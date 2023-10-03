using System.Collections;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private int highScore;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private bool isEscapePressed = false;
    private float escapePressedTime = 0f;
    private float doubleEscapeInterval = 1f; // Waktu dalam detik antara dua penekanan tombol Escape.

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
    }

    void Update()
    {
        // Check apakah tombol Escape ditekan.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Jika tombol Escape ditekan dalam waktu yang singkat, keluar dari permainan.
            if (isEscapePressed && (Time.time - escapePressedTime) < doubleEscapeInterval)
            {
                Application.Quit();
            }
            else
            {
                // Jika ini adalah penekanan pertama, catat waktunya.
                isEscapePressed = true;
                escapePressedTime = Time.time;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}
