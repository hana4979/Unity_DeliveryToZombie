using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // hitPoint : ���ݴ��� ��ġ, hitNormal : ���ݴ��� ǥ���� ����
    void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
}
