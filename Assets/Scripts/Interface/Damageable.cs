using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable {
    public void TakeDamage(int damage);

    public void TakeDamageWithKnockBack(int damage, float knockBack) {}
}
