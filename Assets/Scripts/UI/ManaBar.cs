using UnityEngine;

public class ManaBar : MonoBehaviour
{
    public GameObject slider;

    public SpellCaster sc;
    float old_perc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sc == null) return;
        float perc = sc.mana * 1.0f / sc.max_mana;
        if (Mathf.Abs(old_perc - perc) > 0.01f)
        {
            slider.transform.localScale = new Vector3(perc, 1, 1);
            slider.transform.localPosition = new Vector3(-(1 - perc) / 2, 0, 0);
            old_perc = perc;
        }
    }

    public void SetSpellCaster(SpellCaster sc)
    {
        this.sc = sc;
        old_perc = 0;
    }
}
