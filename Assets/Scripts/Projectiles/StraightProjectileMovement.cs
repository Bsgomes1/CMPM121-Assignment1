using UnityEngine;

public class StraightProjectileMovement : ProjectileMovement
{
    public StraightProjectileMovement(float speed) : base(speed)
    {

    }

    public override void Movement(Transform transform)
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.Self);
    }
}
