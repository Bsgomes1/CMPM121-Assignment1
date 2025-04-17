using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpriteView : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply(string label, Sprite sprite)
    {
        this.label.text = label;
        this.image.sprite = sprite;
    }
}
