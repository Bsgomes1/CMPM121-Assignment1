using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    public GameObject DamageNumber;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Instance.OnDamage += OnDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDamage(Vector3 where, Damage dmg, Hittable target)
    {
        var new_dmg_nr = Instantiate(DamageNumber, where, Quaternion.identity);
        Vector3 dmg_pos = where + new Vector3(0, 0, -2);
        new_dmg_nr.GetComponent<AnimateDamage>().Setup(dmg.amount.ToString(), dmg_pos, dmg_pos + new Vector3(0, 3, 0), 10, 2, Color.magenta, Color.white, 1.5f);
    }
}
