using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private int score;

    public Player player;

    public Text scoreText;

    public GameObject playButton;

    public GameObject PauseButton;

    public GameObject ContinueButton;

    public GameObject QuitButton;

    public GameObject gameOver;

    public GameObject GetReady;

    private int difficultyLevel = 1;

    private int maxDifficultyLevel = 5;

    private int scoreToNextLevel = 20;

    [SerializeField]
    Transform ScoreSound;

    [SerializeField]
    Transform GameOverSound;


    private void Awake()
    {
        //Application.targetFrameRate = 60;   
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        GetReady.SetActive(true);

        Time.timeScale = 1f;
        player.enabled = true;

        GetReady.SetActive(false);
        PauseButton.SetActive(true);

        Spikes[] spikes = FindObjectsOfType<Spikes>();

        for(int i = 0 ; i < spikes.Length;i++)
        {
            Destroy(spikes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        GetReady.SetActive(false);
        ContinueButton.SetActive(false);
        PauseButton.SetActive(true);
        QuitButton.SetActive(false);
    }

    public void PauseMenu()
    {
        Pause();      
        GetReady.SetActive(true);
        ContinueButton.SetActive(true);
        PauseButton.SetActive(false);
        QuitButton.SetActive(true);
    }

    public void GameOver()
    {
        PauseButton.SetActive(false);
        gameOver.SetActive(true);

        Transform obj1 = Instantiate(GameOverSound, GameOverSound.transform.position, new Quaternion());
        obj1.gameObject.SetActive(true);
        Destroy(obj1.gameObject, obj1.GetComponent<AudioSource>().clip.length);

        playButton.SetActive(true);

        Spikes.speed = 5;
        scoreToNextLevel = 20;
        difficultyLevel = 1;

        Pause();
        
    }

    public void IncreaseScore()
    {
        score++;

        Transform obj1 = Instantiate(ScoreSound, ScoreSound.transform.position, new Quaternion() );
        obj1.gameObject.SetActive(true);
        Destroy(obj1.gameObject, obj1.GetComponent<AudioSource>().clip.length);

        scoreText.text = score.ToString();

        if (score >= scoreToNextLevel)
            LevelUp();
        
    }

    public void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel += 20;
        difficultyLevel++;

        Spikes.speed = 4 + difficultyLevel;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

}
