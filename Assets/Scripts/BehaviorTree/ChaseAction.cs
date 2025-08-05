using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Npc] chases players when insight", category: "Action", id: "d658dea9b19be42d436919ff14bae004")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Npc;
    [SerializeReference] public BlackboardVariable<Transform> TargetPlayer;

    private float visionRange = 3f;
    private float chaseSpeed = 1.75f;
    private float attackRange = 0.5f;
    private Animator anim;
    private Vector3 originalScale;
    private Rigidbody2D body;

    protected override Status OnStart()
    {
        if (Npc?.Value != null)
        {
            originalScale = Npc.Value.transform.localScale;

            anim = Npc.Value.GetComponent<Animator>();
            if (anim == null)
                Debug.LogWarning($"{Npc.Value.name} has no Animator component!");

            body = Npc.Value.GetComponent<Rigidbody2D>();
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GameObject npc = Npc?.Value;
        Transform target = TargetPlayer?.Value;

        if (npc == null || target == null)
            return Status.Failure;

        Vector2 npcPos = npc.transform.position;
        Vector2 targetPos = target.position;
        float distance = Vector2.Distance(npcPos, targetPos);

        if (distance <= attackRange)
            return Status.Success;
        else if (distance > visionRange)
            return Status.Failure;

        Vector2 direction = (targetPos - npcPos).normalized;
        float faceDirection = targetPos.x - npcPos.x;

        Rigidbody2D body = npc.GetComponent<Rigidbody2D>();

        if (direction.x != 0)
        {
            Vector3 scale = originalScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(originalScale.x);
            npc.transform.localScale = scale;
        }

        if (body != null)
        {
            body.linearVelocity = direction * chaseSpeed;
            anim?.SetBool("run", true);
        }
        else
        {
            //npc.transform.position = Vector2.MoveTowards(npcPos, targetPos, chaseSpeed * Time.deltaTime);
            body.linearVelocity = direction * chaseSpeed;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        GameObject npc = Npc?.Value;
        if (npc != null)
        {
            Rigidbody2D body = npc.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.linearVelocity = Vector2.zero;
            }
        }
    }
}

