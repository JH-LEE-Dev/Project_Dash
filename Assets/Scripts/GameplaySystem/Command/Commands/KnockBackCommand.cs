using UnityEngine;

public class KnockBackCommand : ICommand
{
    private float power;
    private Vector2 attackPos;

    public void Execute(Entity entity)
    {
        entity.ApplyKnockBack(attackPos, power);
    }

    public void Initialize(Vector2 attackPos, float power)
    {
        this.attackPos = attackPos;
        this.power = power;
    }
}
