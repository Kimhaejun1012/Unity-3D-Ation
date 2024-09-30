using UnityEngine;

public interface IProjectile
{
    void SetAttacker(Transform transform);
    Transform GetAttacker();

    void ReturnObject();
}
