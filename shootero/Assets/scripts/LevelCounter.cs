using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCounter : MonoBehaviour
{
    private EnemyRespawnMenager respMenager;

    public int level;
    public int subLevel;

    public float enemyCount;
    public int deaths;

    public float dificultyScallingSpeed;

    public int dificulty;

    [HideInInspector]
    public int enemyStats;

    [SerializeField]
    private GameObject bossSpawnPlace;
    [Header("BossPrefabs")]
    /*
     Kolejnosc bossow:
    private GameObject bossBlack;
    private GameObject bossBlue;
    private GameObject bossGreen;
    private GameObject bossRed;
    private GameObject finalBoss;
    */
    [SerializeField]
    private GameObject[] bosses;

    private static LevelCounter instance;
    public static LevelCounter Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void PrePrepareStart()
    {
        StartCoroutine(PrepareStart());
    }
    private IEnumerator PrepareStart()
    {
        yield return new WaitForSeconds(5f);
        subLevel = 1;
        enemyStats = 0;
        respMenager = EnemyRespawnMenager.Instance;

        enemyCount = subLevel * (dificultyScallingSpeed);

        DontDestroyOnLoad(this.gameObject);

        if (level == 5 || level == 10 || level == 15 || level == 20 || level == 25)
        {
            boss = true;
        }
        else //(level != 5 && level != 10 && level != 15 && level != 20 && level != 25)
        {
            boss = false;
        }
        StartCoroutine(Respawn());
    }
    public bool boss;
    private void SpawnBoss(int bossNum)
    {
        boss = true;
        Instantiate(bosses[bossNum], bossSpawnPlace.transform.position, Quaternion.identity);
    }

    public void StartSequence()
    {
        for (int i = 0; i < 2; i++)
        {
            if (boss)
            {
                if (deaths == 1)
                {
                    StartCoroutine(BossToMenu());
                }
            }
            else
            {
                if (deaths >= enemyCount)
                {
                    subLevel += 1;

                    enemyCount = dificultyScallingSpeed * (Mathf.Pow(subLevel, 1.5f));
                    deaths = 0;
                    if (subLevel >= 11)
                    {
                        StartCoroutine(ToMenu());
                    }
                    StartCoroutine(Respawn());
                }
            }
        }
    }
    public IEnumerator Respawn()
    {
        if (!boss)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                yield return new WaitForSeconds(0.8f);

                if (level < 6)
                {
                    dificulty = 1;

                    respMenager.RespawnEnemies(1, Random.Range(0, 2));//max 1
                }
                else if (5 < level && level < 11)
                {
                    dificulty = 2;
                    enemyStats = 5;
                    respMenager.RespawnEnemies(1, Random.Range(0, 4));//max 3
                }
                else if (10 < level && level < 16)
                {
                    dificulty = 3;
                    enemyStats = 10;
                    respMenager.RespawnEnemies(1, Random.Range(0, 7));//max 6 ale potem trzeba zmienic na 6 bo ten ostatni to mothership dla testow
                }
                else if (15 < level && level < 20)
                {
                    dificulty = 3;
                    enemyStats = 15;
                    respMenager.RespawnEnemies(1, Random.Range(0, respMenager.allEnemies.Length));
                }
                else if (20 < level)
                {
                    dificulty = 4;
                    enemyStats = 23;
                    respMenager.RespawnEnemies(1, Random.Range(0, respMenager.allEnemies.Length));
                }

            }
        }
        else
        {
            enemyCount = 1;
            int bossNum = 0;
            if (level == 5)
            {
                bossNum = 0;
                SpawnBoss(bossNum);
            }
            else if (level == 10)
            {
                bossNum = 1;
                SpawnBoss(bossNum);
            }
            else if (level == 15)
            {
                bossNum = 2;
                SpawnBoss(bossNum);
            }
            else if (level == 20)
            {
                bossNum = 3;
                SpawnBoss(bossNum);
            }
            else if (level == 25)
            {
                bossNum = 4;
                SpawnBoss(bossNum);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ToMenu());
        }
    }
    public IEnumerator ToMenu()
    {
        if (subLevel >= 11)
        {
            level += 1;
            subLevel = 1;
        }

        SceneManager.LoadScene("MainMenu Test", LoadSceneMode.Single);

        yield return new WaitForEndOfFrame();

       // LevelMenager.Instance.Level = level;
       //LevelMenager.Instance.UpdateSlider();
    }
    public IEnumerator BossToMenu()
    {
        level += 1;
        subLevel = 1;
        boss = false;
        SceneManager.LoadScene("MainMenu Test", LoadSceneMode.Single);

        yield return new WaitForEndOfFrame();

        //LevelMenager.Instance.Level = level;
       // LevelMenager.Instance.UpdateSlider();
    }
}
