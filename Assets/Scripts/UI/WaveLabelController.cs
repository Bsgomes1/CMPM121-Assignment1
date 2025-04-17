using UnityEngine;
using TMPro;

public class WaveLabelController : MonoBehaviour
{
    TextMeshProUGUI tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.GameState.INWAVE)
        {
            tmp.text = "Enemies left: " + GameManager.Instance.enemy_count;
        }
        if (GameManager.Instance.state == GameManager.GameState.COUNTDOWN)
        {
            tmp.text = "Starting in " + GameManager.Instance.countdown;
        }
    }
}
