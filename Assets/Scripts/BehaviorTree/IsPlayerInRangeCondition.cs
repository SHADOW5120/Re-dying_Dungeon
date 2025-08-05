using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsPlayerInRange", story: "Does [Boss] see players", category: "Conditions", id: "e8c89083d1299e437dc55ef36da20ca8")]
public partial class IsPlayerInRangeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Boss;

    public override bool IsTrue()
    {
        if (Boss?.Value == null)
            return false;

        Vector2 bossPos = Boss.Value.transform.position;

        Vector2 roomSize = new Vector2(10f, 8f);

        int playerLayerMask = 1 << LayerMask.NameToLayer("Player");

        Collider2D[] hits = Physics2D.OverlapBoxAll(bossPos, roomSize, 0f, playerLayerMask);

        foreach (var hit in hits)
        {
            if (hit != null && hit.gameObject != null)
            {
                return true;
            }
        }

        return false;
    }



    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
