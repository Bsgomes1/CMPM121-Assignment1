using UnityEngine;
using System.Collections.Generic;
using System;

public class Unit : MonoBehaviour
{
    
    public Vector2 movement;
    public float distance;
    public event Action<float> OnMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(new Vector2(movement.x, 0) * Time.fixedDeltaTime);
        Move(new Vector2(0, movement.y) * Time.fixedDeltaTime);
        distance += movement.magnitude*Time.fixedDeltaTime;
        if (distance > 0.5f)
        {
            OnMove?.Invoke(distance);
            distance = 0;
        }
    }

    public void Move(Vector2 ds)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        int n = GetComponent<Rigidbody2D>().Cast(ds, hits, ds.magnitude * 2);
        if (n == 0)
        {
            transform.Translate(ds);
        }
    }


}
