using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteViewer : MonoBehaviour
{
    public enum Mode { SPELLICONS, ENEMIES, PROJECTILES, CLASSES }
    public GameObject spriteView;
    int per_row;
    List<GameObject> views;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        per_row = (Screen.width - 40) / 80;
        views = new List<GameObject>();
        StartCoroutine(ShowView());
    }

    IEnumerator ShowView()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeMode("enemies");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMode(string m)
    {
        foreach (var go in views)
        {
            Destroy(go);
        }
        views.Clear();
        if (m == "spellicons")
        {
            for (int i = 0; i < GameManager.Instance.spellIconManager.GetCount(); ++i)
            {
                var new_sv = Instantiate(spriteView, transform);
                int x = i % per_row;
                int y = i / per_row;
                
                new_sv.transform.Translate(x * 80, -y * 100, 0, Space.Self);
                new_sv.GetComponent<SpriteView>().Apply(i.ToString(), GameManager.Instance.spellIconManager.Get(i));
                views.Add(new_sv);
            }
        }
        if (m == "enemies")
        {
            for (int i = 0; i < GameManager.Instance.enemySpriteManager.GetCount(); ++i)
            {
                var new_sv = Instantiate(spriteView, transform);
                int x = i % per_row;
                int y = i / per_row;
                new_sv.transform.Translate(x * 80, -y * 100, 0, Space.Self);
                new_sv.GetComponent<SpriteView>().Apply(i.ToString(), GameManager.Instance.enemySpriteManager.Get(i));
                views.Add(new_sv);
            }
        }
        if (m == "relics")
        {
            for (int i = 0; i < GameManager.Instance.relicIconManager.GetCount(); ++i)
            {
                var new_sv = Instantiate(spriteView, transform);
                int x = i % per_row;
                int y = i / per_row;
                new_sv.transform.Translate(x * 80, -y * 100, 0, Space.Self);
                new_sv.GetComponent<SpriteView>().Apply(i.ToString(), GameManager.Instance.relicIconManager.Get(i));
                views.Add(new_sv);
            }
        }
        if (m == "player")
        {
            for (int i = 0; i < GameManager.Instance.playerSpriteManager.GetCount(); ++i)
            {
                var new_sv = Instantiate(spriteView, transform);
                int x = i % per_row;
                int y = i / per_row;
                new_sv.transform.Translate(x * 80, -y * 100, 0, Space.Self);
                new_sv.GetComponent<SpriteView>().Apply(i.ToString(), GameManager.Instance.playerSpriteManager.Get(i));
                views.Add(new_sv);
            }
        }
    }

    
}
