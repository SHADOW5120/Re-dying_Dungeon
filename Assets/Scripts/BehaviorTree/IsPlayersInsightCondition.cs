using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsPlayersInsight", story: "Does [npc] see players", category: "Conditions", id: "740c6e38f4c50f8d339abcd09cf36fee")]
public partial class IsPlayersInsightCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Npc;
    [SerializeReference] public BlackboardVariable<Transform> TargetPlayer;

    private float visionRange = 3f;
    float fieldOfView = 90f;

    public override bool IsTrue()
    {
        if (Npc?.Value == null)
            return false;

        Vector2 npcPos = Npc.Value.transform.position;
        Vector2 npcForward = Npc.Value.transform.right.normalized;

        int playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        Collider2D[] hits = Physics2D.OverlapCircleAll(npcPos, visionRange, playerLayerMask);

        foreach (var hit in hits)
        {
            if (hit != null && hit.gameObject != null)
            {
                Vector2 toPlayer = ((Vector2)hit.transform.position - npcPos).normalized;
                float angleToPlayer = Vector2.Angle(npcForward, toPlayer);

                if (angleToPlayer <= fieldOfView / 2f)
                {
                    RaycastHit2D rayHit = Physics2D.Raycast(npcPos, toPlayer, visionRange, ~playerLayerMask);
                    if (rayHit.collider == null || rayHit.collider.gameObject == hit.gameObject)
                    {
                        TargetPlayer.Value = hit.transform;
                        return true;
                    }
                }
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
