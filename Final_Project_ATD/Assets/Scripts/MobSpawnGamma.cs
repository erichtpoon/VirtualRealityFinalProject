using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MobSpawnGamma : MonoBehaviour {

    private const float timeLeftFirstWave = 4;
    private const float timeLeftSecondWave = 2;
    private const float timeLeftThirdWave = 1;
    private const float intermission = 8;


    public GameObject mobAlphaTypeOne;
    public GameObject mobAlphaTypeTwo;
    public GameObject mobAlphaTypeThree;
    
    public Transform startOLocation;
    public Transform startPLocation;
    private Transform startLocation;

    public static int score;
    public static int lives;
    public static int coins;

    public static bool intermissionon;

    //conditions
    private bool gameFinished;


    //mobsPerWave
    public static int waveNumber;
    private int waveOneMobs;
    private int waveTwoMobs;
    private int waveThreeMobs;
    private int numberofMobs;

    private GameObject spawnedMod;
    private float timeLeft;

    //This is used for randomization
    private float whatIsEnd;
    private int integerwhatIsEnd;

    // Use this for initialization
    void Start () {
        //initializaions
        waveOneMobs = 10;
        waveTwoMobs = 15;
        waveThreeMobs = 30;
        waveNumber = 1;
        timeLeft = intermission;
        intermissionon = true;
        gameFinished = false;
        score = 0;
        lives = 15;
        coins = 5;

    }
	
	// Update is called once per frame
	void Update () {


        float dud = 1;
        dud -= Time.deltaTime;
        timeLeft = timeLeft - Time.deltaTime;

        if (lives <= 0) {
            gameFinished = true;
        }

        if (intermissionon == true) {
            if (timeLeft < 0) {
                intermissionon = false;
            }
        }
        

        if (timeLeft < 0 && waveNumber != 4 && gameFinished == false && lives > 0 && intermissionon == false)
        {
            
            monsterSpawner();
        }
       if(gameFinished == true) {
            SceneManager.LoadScene("gameOver", LoadSceneMode.Single);
        }

    }


    /*******************************
    *  Timer for Monster Spawning
    ********************************/
    private void monsterSpawner()
    {

        float whatIsSpawn = Random.Range(1.0f, 3.9f);
        int integerwhatIsSpawn = (int)whatIsSpawn;

        switch (integerwhatIsSpawn)
        {
            case 1:
                spawnedMod = Instantiate(mobAlphaTypeOne);
                break;

            case 2:
                spawnedMod = Instantiate(mobAlphaTypeTwo);
                break;

            case 3:
                spawnedMod = Instantiate(mobAlphaTypeThree);
                break;
            default:
                break;
        }

        whatIsEnd = Random.Range(1.0f, 2.99f);
        integerwhatIsEnd = (int)whatIsEnd;

        if (integerwhatIsEnd == 1) {
            startLocation = startOLocation;
        }
        else if (integerwhatIsEnd == 2) {
            startLocation = startPLocation;
        }

        timeLeft = waveCalculator(waveNumber);
        waveNumber = mobbernumber(waveNumber);
        spawnedMod.transform.position = startLocation.position;
    }

    /*******************************
    *  Depending of the wave, return mobspawnRate
    ********************************/
    private float waveCalculator(int waveNumber)
    {
        float waveReturn = -1;
        switch (waveNumber)
        {
            case 1:
                waveReturn = timeLeftFirstWave;
                break;

            case 2:
                waveReturn = timeLeftSecondWave;
                break;

            case 3:
                waveReturn = timeLeftThirdWave;
                break;
            default:
                break;
        }
        return waveReturn;
    }
    /*******************************
    *  Depending of the wave, return mobspawnRate
    ********************************/
    private int mobbernumber(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                waveOneMobs = waveOneMobs - 1;
                if (waveOneMobs > 0)
                {
                    return waveNumber;
                }
                else
                {
                    waveNumber = waveNumber + 1;
                    timeLeft = intermission;
                    intermissionon = true;
                    return waveNumber;
                }
            case 2:
                waveTwoMobs = waveTwoMobs - 1;
                if (waveTwoMobs > 0)
                {
                    return waveNumber;
                }
                else
                {
                    waveNumber = waveNumber + 1;
                    timeLeft = intermission;
                    return waveNumber;
                }
            case 3:
                waveThreeMobs = waveThreeMobs - 1;
                if (waveThreeMobs > 0)
                {
                    return waveNumber;
                }
                else
                {
                    waveNumber = waveNumber + 1;
                    timeLeft = intermission;
                    return waveNumber;
                }
            case 4:
                gameFinished = true;
                print("Game is over");
                return 3;
            default:
                print("this should not be happening");
                break;
        }
        return 4;
    }
}
