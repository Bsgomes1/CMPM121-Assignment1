using UnityEngine;
using TMPro;

public class AnimateDamage : MonoBehaviour
{
    public Color color_i, color_f;
    public Vector3 initialPosition, finalPosition; //position to drift to, relative to the gameObject's local origin
    public float fadeDuration;
    private float fadeStartTime;
    public int font_i;
    public int font_f;
    public string dmg;
    float timeoffset;

    public void Setup(string dmg, Vector3 initialPosition, Vector3 finalPosition, int font_i, int font_f, Color color_i, Color color_f, float duration)
    {
        this.dmg = dmg;
        this.font_i = font_i;
        this.font_f = font_f;
        this.initialPosition = initialPosition;
        this.finalPosition = finalPosition;
        this.color_i = color_i;
        this.color_f = color_f;
        this.fadeDuration = duration;
    }

    void Start()
    {
        fadeStartTime = Time.time;
        GetComponent<TMPro.TMP_Text>().text = dmg;
        timeoffset = Random.value;
    }


    void Update()
    {
        float progress = (Time.time - fadeStartTime) / fadeDuration;
        if (progress <= 1)
        {
           
            transform.position = Vector3.Lerp(initialPosition, finalPosition, progress) + new Vector3(Mathf.Sin((Time.time+timeoffset)*10)/3, 0, 0);
            GetComponent<TMP_Text>().fontSize = Mathf.RoundToInt(progress * font_f + (1 - progress) * font_i);
            GetComponent<TMP_Text>().fontMaterial.color = Color.Lerp(color_i, color_f, progress);
        }
        else Destroy(gameObject);
    }
}
