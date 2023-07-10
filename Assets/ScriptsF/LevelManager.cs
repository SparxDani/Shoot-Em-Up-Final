using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    int numDestructables = 0;
    public bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Tutorial", "Level 1", "Level 2", "Level 3" };
    int currentLevel = 0;

    int score = 0;
    public Text scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Iniciar el primer nivel
        //LoadLevel(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar si se debe cargar el siguiente nivel
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (currentLevel < levels.Length)
                {
                    LoadLevel(currentLevel);
                }
                else
                {
                    Debug.Log("¡Has completado todos los niveles!");
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }
    public void LoadNextLevel()
    {
        currentLevel++;

        if (currentLevel < levels.Length)
        {
            string sceneName = levels[currentLevel];
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            // Has alcanzado el último nivel, puedes realizar alguna acción aquí
            Debug.Log("¡Has completado todos los niveles!");
        }
    }

    void LoadLevel(int levelIndex)
    {
        string sceneName = levels[levelIndex];
        SceneManager.LoadScene(sceneName);
    }

    public void ResetLevel()
    {
        //SaveScore(score);
        score = 0;
        AddScore(score);
        LoadLevel(currentLevel);
    }

    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();
    }

    public void AddDestructable()
    {
        numDestructables++;
    }

    public void RemoveDestructable()
    {
        numDestructables--;

        if (numDestructables == 0)
        {
            startNextLevel = true;
        }
    }

    
}
