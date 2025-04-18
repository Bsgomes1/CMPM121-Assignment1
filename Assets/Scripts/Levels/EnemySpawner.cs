using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public Image level_selector;
    public GameObject button;
    public GameObject enemy;
    public SpawnPoint[] SpawnPoints;
    private Dictionary<string, Enemy> enemyData;
    private List<Level> levels;

    private int currentWave = 0; // Tracks the current wave number
    private Level currentLevel;  // Tracks the current level

    void Start()
    {
        int x = -200;
        LoadEnemyData("Assets/Resources/enemies.json");
        LoadLevelData("Assets/Resources/levels.json");
        foreach (Level level in levels)
        {
            if (level.Name.ToLower() == "start") continue;

            GameObject selector = Instantiate(button, level_selector.transform);
            selector.transform.localPosition = new Vector3(x, 130); // You can remove this if you're using a Layout Group

            MenuSelectorController controller = selector.GetComponent<MenuSelectorController>();
            controller.spawner = this;
            controller.SetLevel(level);
            x = x + 200;
        }
        //GameObject selector = Instantiate(button, level_selector.transform);
        //selector.transform.localPosition = new Vector3(0, 130);
        //selector.GetComponent<MenuSelectorController>().spawner = this;
        //selector.GetComponent<MenuSelectorController>().SetLevel(levels[0]);
    }

    private void LoadEnemyData(string filePath)
    {
        string json = File.ReadAllText(filePath);
        var enemies = JsonConvert.DeserializeObject<List<Enemy>>(json);
        enemyData = new Dictionary<string, Enemy>();

        foreach (var enemy in enemies)
        {
            enemyData[enemy.Name.ToLower()] = enemy;
        }
    }

    private void LoadLevelData(string filePath)
    {
        string json = File.ReadAllText(filePath);
        levels = JsonConvert.DeserializeObject<List<Level>>(json);
    }

    public void StartLevel(Level level)
    {
        level_selector.gameObject.SetActive(false);

        // Find the level by name
        currentLevel = level;

        if (currentLevel == null)
        {
            Debug.LogError("Level is null!");
            return;
        }

        Debug.Log("Starting level: " + currentLevel.Name);
        currentWave = 1; // Start at wave 1
        GameManager.Instance.player.GetComponent<PlayerController>().StartLevel();
        StartCoroutine(SpawnWave(currentLevel, currentWave));
    }


    public void NextWave()
    {
        if (currentWave < currentLevel.Waves || currentLevel.Waves == 0) // Endless mode has 0 waves
        {
            currentWave++; // Increment the wave number
            StartCoroutine(SpawnWave(currentLevel, currentWave)); // Pass the current level and wave number
        }
        else
        {
            // Player wins if all waves are completed
            Debug.Log("You win!");
        }
    }

    private SpawnPoint GetSpawnPoint(string location)
    {
        if (string.IsNullOrEmpty(location) || location.ToLower() == "random")
        {
            // Return a random spawn point
            return SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        }

        // Filter spawn points by type
        var filteredPoints = SpawnPoints.Where(sp => sp.kind.ToString().ToLower() == location.ToLower()).ToArray();
        if (filteredPoints.Length > 0)
        {
            return filteredPoints[UnityEngine.Random.Range(0, filteredPoints.Length)];
        }

        // Default to a random spawn point if no match is found
        return SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
    }

    // IEnumerator SpawnWave(Level level, int waveNumber)
    // {
    //     GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
    //     GameManager.Instance.countdown = 3;

    //     for (int i = 3; i > 0; i--)
    //     {
    //         yield return new WaitForSeconds(1);
    //         GameManager.Instance.countdown--;
    //     }

    //     GameManager.Instance.state = GameManager.GameState.INWAVE;

    //     foreach (var spawn in level.Spawns)
    //     {
    //         int count = (int)RPNCalculator.Evaluate(spawn.Count, new Dictionary<string, float>
    //         {
    //             { "base", enemyData[spawn.Enemy.ToLower()].HP },
    //             { "wave", waveNumber }
    //         });

    //         float delay = spawn.Delay != null ? float.Parse(spawn.Delay) : 2f;
    //         List<int> sequence = spawn.Sequence ?? new List<int> { 1 };

    //         int spawned = 0;
    //         int sequenceIndex = 0;

    //         while (spawned < count)
    //         {
    //             int groupSize = Math.Min(sequence[sequenceIndex], count - spawned);
    //             sequenceIndex = (sequenceIndex + 1) % sequence.Count;

    //             for (int i = 0; i < groupSize; i++)
    //             {
    //                 SpawnEnemy(spawn, waveNumber);
    //             }

    //             spawned += groupSize;
    //             yield return new WaitForSeconds(delay);
    //         }
    //     }

    //     yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
    //     GameManager.Instance.state = GameManager.GameState.WAVEEND;
    // }
    IEnumerator SpawnWave(Level level, int waveNumber)
    {
        Debug.Log($"Starting wave {waveNumber} for level {level.Name}");

        GameManager.Instance.state = GameManager.GameState.COUNTDOWN;
        GameManager.Instance.countdown = 3;

        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.countdown--;
        }

        GameManager.Instance.state = GameManager.GameState.INWAVE;

        foreach (var spawn in level.Spawns)
        {
            int count = (int)RPNCalculator.Evaluate(spawn.Count, new Dictionary<string, float>
            {
                { "base", enemyData[spawn.Enemy.ToLower()].HP },
                { "wave", waveNumber }
            });
            Debug.Log($"Spawning {count} {spawn.Enemy}(s) for wave {waveNumber}");

            float delay = spawn.Delay != null ? float.Parse(spawn.Delay) : 2f;
            List<int> sequence = spawn.Sequence ?? new List<int> { 1 };

            int spawned = 0;
            int sequenceIndex = 0;

            while (spawned < count)
            {
                int groupSize = Math.Min(sequence[sequenceIndex], count - spawned);
                sequenceIndex = (sequenceIndex + 1) % sequence.Count;

                for (int i = 0; i < groupSize; i++)
                {
                    SpawnEnemy(spawn, waveNumber);
                }

                spawned += groupSize;
                yield return new WaitForSeconds(delay);
            }
        }

        yield return new WaitWhile(() => GameManager.Instance.enemy_count > 0);
        GameManager.Instance.state = GameManager.GameState.WAVEEND;
        Debug.Log($"Wave {waveNumber} completed!");
    }
    private void SpawnEnemy(Spawn spawn, int waveNumber)
    {
        SpawnPoint spawnPoint = GetSpawnPoint(spawn.Location);
        
        // Vector2 offset = UnityEngine.Random.insideUnitCircle * 1.8f;
        // Vector3 position = spawnPoint.transform.position + new Vector3(offset.x, offset.y, 0);

        // GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
        // Enemy enemyData = this.enemyData[spawn.Enemy.ToLower()];
        Vector3 position;
        int maxAttempts = 10; // Maximum attempts to find a non-overlapping position
        int attempts = 0;

        do
        {
            Vector2 offset = UnityEngine.Random.insideUnitCircle * 1.8f;
            position = spawnPoint.transform.position + new Vector3(offset.x, offset.y, 0);
            attempts++;
        }
        while (Physics2D.OverlapCircle(position, 0.5f) != null && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Debug.LogWarning("Could not find a non-overlapping position for enemy spawn.");
            return; // Skip spawning this enemy if no valid position is found
        }

        GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
        Enemy enemyData = this.enemyData[spawn.Enemy.ToLower()];


        float hp = RPNCalculator.Evaluate(spawn.HP ?? "base", new Dictionary<string, float>
        {
            { "base", enemyData.HP },
            { "wave", waveNumber }
        });

        float speed = RPNCalculator.Evaluate(spawn.Speed ?? "base", new Dictionary<string, float>
        {
            { "base", enemyData.Speed },
            { "wave", waveNumber }
        });

        float damage = RPNCalculator.Evaluate(spawn.Damage ?? "base", new Dictionary<string, float>
        {
            { "base", enemyData.Damage },
            { "wave", waveNumber }
        });

        newEnemy.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.enemySpriteManager.Get(enemyData.Sprite);
        EnemyController controller = newEnemy.GetComponent<EnemyController>();
        controller.hp = new Hittable((int)hp, Hittable.Team.MONSTERS, newEnemy);
        controller.speed = (int)speed;
        GameManager.Instance.AddEnemy(newEnemy);
    }
}
