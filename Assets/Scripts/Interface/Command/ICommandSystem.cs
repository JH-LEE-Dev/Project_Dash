using UnityEditor.Search;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public interface ICommandSystem 
{
    public void ApplyKnockBackCommand(Entity entity, Vector2 attackPos, float power)
    {

    }
    public void ApplyAttackCommand(Entity entity)
    {
       
    }

    public void ApplyConflictKnockBackCommand(Entity entity)
    {

    }
}
