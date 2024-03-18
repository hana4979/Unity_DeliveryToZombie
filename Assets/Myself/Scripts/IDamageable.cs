using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // hitPoint : 공격당한 위치, hitNormal : 공격당한 표면의 방향
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
