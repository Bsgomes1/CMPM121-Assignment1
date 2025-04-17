using UnityEngine;

public class ProjectileMovement
{
    public float speed;

    public ProjectileMovement(float speed)
    {
        this.speed = speed;
    }

    public virtual void Movement(Transform transform)
    {
        
    }
}
