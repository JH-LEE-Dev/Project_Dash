using System.Security.Principal;
using UnityEngine;
using static UnityEngine.LightAnchor;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace NS_NormalAttack
{
    [CreateAssetMenu(menuName = "Combat/Attack/Normal")]
    public class NormalAttack : AttackStrategy
    {
        [SerializeField] public float force = 2f;
        [SerializeField] public float radius = 2f;

        public override void ApplyAttack(Entity entity, Vector2 origin, LayerMask layerMask)
        {
            Collider2D[] unitBuffer = new Collider2D[50];
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = true;
            filter.SetLayerMask(layerMask);

            unitBuffer = Physics2D.OverlapCircleAll(origin, radius, layerMask);

            for (int i = 0; i < unitBuffer.Length; i++)
            {
                Entity u = unitBuffer[i].GetComponent<Entity>();

                if (u != null)
                {
                    u.ApplyKnockBack(origin, force);
                }
            }
        }
    }
}

namespace NS_SwordAttack
{
    [CreateAssetMenu(menuName = "Combat/Attack/Swordman")]
    public class SwordmanAttack : AttackStrategy
    {
        [SerializeField] public float force = 2f;
        [SerializeField] public float radius = 1f;
        [SerializeField] public float angle = 60f;

        private Entity CalcAttackAngle(Vector2 origin, Vector2 dashVector, LayerMask layerMask)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, layerMask);

            Vector2 forward = -dashVector.normalized;
            Entity closest = null;
            float minDist = float.MaxValue;

            foreach (var hit in hits)
            {
                Vector2 dir = ((Vector2)hit.transform.position - origin).normalized;

                // 부채꼴 내 여부 체크
                float dot = Vector2.Dot(forward, dir);
                float rad = Mathf.Acos(dot);
                float deg = rad * Mathf.Rad2Deg;

                if (deg > angle * 0.5f)
                    continue; // 부채꼴 밖이면 건너뜀

                // 거리 계산
                float dist = Vector2.Distance(origin, hit.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    Entity hitEntity = hit.GetComponent<Entity>();

                    if (hitEntity != null)
                        closest = hitEntity;
                }
            }

            return closest;
        }

        public override void ApplyAttack(Entity entity, Vector2 origin, LayerMask layerMask)
        {
            Entity hitEntity = CalcAttackAngle(origin, entity.GetDashVector(), layerMask);

            if (hitEntity != null)
            {
                hitEntity.ApplyKnockBack(origin, force);
                entity.fsm.ChangeState<ConflictKnockBackState>();
            }
        }
    }
}

