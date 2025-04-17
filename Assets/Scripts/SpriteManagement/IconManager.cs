using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceSprite(int which, Image target)
    {
        target.sprite = sprites[which];
    }

    public Sprite Get(int index)
    {
        return sprites[index];
    }

    public int GetCount()
    {
        return sprites.Length;
    }


}
