using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "DoesNpcSeeAnyPlayer", story: "Does [Npc] see any [Player]", category: "Conditions", id: "c41fcff704c2caa9d5a3f1d6615ebdda")]
public partial class DoesNpcSeeAnyPlayerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Npc;
    [SerializeReference] public BlackboardVariable<Transform> Player;

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
                    int obstacleMask = LayerMask.GetMask("Wall");

                    RaycastHit2D rayHit = Physics2D.Raycast(npcPos, toPlayer, visionRange, obstacleMask);
                    if (rayHit.collider == null || rayHit.collider.gameObject == hit.gameObject)
                    {
                        Player.Value = hit.transform;
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
