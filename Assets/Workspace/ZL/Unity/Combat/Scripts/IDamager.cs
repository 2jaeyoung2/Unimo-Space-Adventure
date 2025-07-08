using UnityEngine;

namespace ZL.Unity.Combat
{
    public interface IDamager
    {
        public void GiveDamage(IDamageable damageable, Vector3 contact);
    }
}