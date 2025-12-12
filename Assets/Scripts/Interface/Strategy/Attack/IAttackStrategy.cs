using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    public abstract void ApplyAttack(Entity entity, Vector2 origin,LayerMask layerMask);
}