using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player1, player2;
    public float playerSpeed;

    public GameObject timer;
    private float timeToMove = 1;
    public float moveTimer = 0.1f;

    private int player1Score, player2Score;
    public Text player1ScoreText, player2ScoreText;

    private bool hasStarted;
    public Text startText;

    public float timeToSpawnParticle = 0.5f;
    private float waitToSpawnParticle;
    public GameObject particle;

    public GameObject UIother, RestartText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        waitToSpawnParticle = timeToSpawnParticle;
        hasStarted = false;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStarted)
        {
            PlayerOneController();
            PlayerTwoController();
            Timer();
        }

        SpawnParticles();
        UpdateScore();
        StartGame();
        RestartLevel();
    }

    private void SpawnParticles()
    {
        Vector2 randomRight = new Vector2(9, Random.Range(4.9f, -3));
        Vector2 randomLeft = new Vector2(-9, Random.Range(4.9f, -3));

        waitToSpawnParticle -= Time.deltaTime;
        if(waitToSpawnParticle <= 0)
        {
            waitToSpawnParticle = timeToSpawnParticle;
            Instantiate(particle, randomRight, Quaternion.identity);
            Instantiate(particle, randomLeft, Quaternion.identity);
        }
    }

    private void PlayerOneController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            player1.transform.Translate(new Vector3(0, playerSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player1.transform.Translate(new Vector3(0, -playerSpeed * Time.deltaTime));
        }

        if (player1.transform.position.y >= 5.44f)
        {
            player1.transform.position = new Vector3(player1.transform.position.x, -5.44f, transform.position.z);
            AudioManager.instance.PlaySFX(0);
            player1Score++;
        }
        else if (player1.transform.position.y <= -5.44f)
        {
            player1.transform.position = new Vector3(player1.transform.position.x, -5.44f, transform.position.z);
        }
    }

    private void PlayerTwoController()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player2.transform.Translate(new Vector3(0, playerSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player2.transform.Translate(new Vector3(0, -playerSpeed * Time.deltaTime));
        }

        if (player2.transform.position.y >= 5.44f)
        {
            player2.transform.position = new Vector3(player2.transform.position.x, -5.44f, transform.position.z);
            AudioManager.instance.PlaySFX(0);
            player2Score++;
        }
        else if (player2.transform.position.y <= -5.44f)
        {
            player2.transform.position = new Vector3(player2.transform.position.x, -5.44f, transform.position.z);
        }
    }

    private void Timer()
    {
        timeToMove -= Time.deltaTime;
        if(timeToMove <= 0)
        {
            timer.transform.position = new Vector2(timer.transform.position.x, timer.transform.position.y - moveTimer);
            timeToMove = 1;
        }

        if (timer.transform.position.y <= -10)
        {
            Time.timeScale = 0;
            RestartText.gameObject.SetActive(true);
        }
    }

    private void UpdateScore()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    private void StartGame()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !hasStarted)
        {
            UIother.gameObject.SetActive(false);
            StartCoroutine(StartingRoutine());
        }
    }

    private IEnumerator StartingRoutine()
    {
        Time.timeScale = 1;
        startText.text = "3";
        AudioManager.instance.PlaySFX(0);

        yield return new WaitForSeconds(1);
        startText.text = "2";
        AudioManager.instance.PlaySFX(0);

        yield return new WaitForSeconds(1);
        startText.text = "1";
        AudioManager.instance.PlaySFX(0);

        yield return new WaitForSeconds(1);
        startText.text = "GO!";
        AudioManager.instance.PlaySFX(1);
        hasStarted = true;

        yield return new WaitForSeconds(1);
        startText.gameObject.SetActive(false);
    }

    private void RestartLevel()
    {
        if(hasStarted && Time.timeScale == 0)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.instance.PlaySFX(1);
                SceneManager.LoadScene(0);
            }
        }
    }
}
