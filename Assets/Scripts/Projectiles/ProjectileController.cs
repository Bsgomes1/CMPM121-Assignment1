using UnityEngine;
using System;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
    public float lifetime;
    public event Action<Hittable,Vector3> OnHit;
    public ProjectileMovement movement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.Movement(transform);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile")) return;
        if (collision.gameObject.CompareTag("unit"))
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec != null)
            {
                OnHit(ec.hp, transform.position);
            }
            else
            {
                var pc = collision.gameObject.GetComponent<PlayerController>();
                if (pc != null)
                {
                    OnHit(pc.hp, transform.position);
                }
            }

        }
        Destroy(gameObject);
    }

    public void SetLifetime(float lifetime)
    {
        StartCoroutine(Expire(lifetime));
    }

    IEnumerator Expire(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
