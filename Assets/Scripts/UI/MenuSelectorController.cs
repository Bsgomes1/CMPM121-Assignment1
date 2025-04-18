using UnityEngine;
using TMPro;

public class MenuSelectorController : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Level level;
    public EnemySpawner spawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(Level newLevel)
    {
        level = newLevel;
        label.text = newLevel.Name;  
    }

    public void StartLevel()
    {
        spawner.StartLevel(level);
    }
}
