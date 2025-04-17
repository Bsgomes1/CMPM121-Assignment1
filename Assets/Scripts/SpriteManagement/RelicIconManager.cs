using UnityEngine;

public class RelicIconManager : IconManager
{
    void Start()
    {
        GameManager.Instance.relicIconManager = this;
    }
}
