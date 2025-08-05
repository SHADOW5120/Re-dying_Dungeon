using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Npc] attacks players when in range", category: "Action", id: "635a4275b84b36e1028353f61c182cad")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Npc;
    [SerializeReference] public BlackboardVariable<Transform> TargetPlayer;

    //private float attackRange = 0.75f;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;
    private Animator anim;

    protected override Status OnStart()
    {
        if (Npc?.Value != null)
        {
            anim = Npc.Value.GetComponent<Animator>();
            if (anim == null)
                Debug.LogWarning($"{Npc.Value.name} has no Animator component!");
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GameObject npc = Npc?.Value;
        Transform target = TargetPlayer?.Value;

        if (npc == null)
            return Status.Failure;

        Vector2 npcPos = npc.transform.position;
        Vector2 targetPos = target.position;

        Vector2 direction = (targetPos - npcPos).normalized;
        float faceDirection = targetPos.x - npcPos.x;

        Rigidbody2D body = npc.GetComponent<Rigidbody2D>();

        if (faceDirection > 0)
            body.transform.localScale = Vector3.one;
        else if (faceDirection < 0)
            body.transform.localScale = new Vector3(-1, 1, 1);

        if (body != null)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                anim?.SetTrigger("attack");
            }
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

