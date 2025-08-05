using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Boss attack", story: "[Boss] attacks players", category: "Action", id: "8e654f301c5ef08c8bff73c62b04656c")]
public partial class BossAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Boss;
    [SerializeReference] public BlackboardVariable<Transform> TargetPlayer;

    private float chaseSpeed = 1.75f;
    private float attackRange = 0.75f;
    private Animator anim;
    private Vector3 originalScale;

    protected override Status OnStart()
    {
        if (Boss?.Value != null)
        {
            originalScale = Boss.Value.transform.localScale;

            anim = Boss.Value.GetComponent<Animator>();
            if (anim == null)
                Debug.LogWarning($"{Boss.Value.name} has no Animator component!");
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GameObject npc = Boss?.Value;
        Transform target = TargetPlayer?.Value;

        if (npc == null || target == null)
            return Status.Failure;

        Vector2 npcPos = npc.transform.position;
        Vector2 targetPos = target.position;
        float distance = Vector2.Distance(npcPos, targetPos);

        if (distance <= attackRange)
        {
            anim?.SetTrigger("attack");
            return Status.Success;
        }

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
            npc.transform.position = Vector2.MoveTowards(npcPos, targetPos, chaseSpeed * Time.deltaTime);
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        GameObject npc = Boss?.Value;
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

