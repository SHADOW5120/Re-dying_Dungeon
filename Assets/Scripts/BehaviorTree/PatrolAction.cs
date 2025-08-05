using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Npc] patrols around", category: "Action", id: "c4b6596873a2ad218a89e5aa3c9ef8a5")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Npc;
    private List<Vector2> waypoints = new List<Vector2>();
    private int currentWaypointIndex = 0;
    private float patrolSpeed = 0.5f;
    private float reachThreshold = 0.2f;
    private Animator anim;
    private float idleDuration = 2;
    private float idleTimer;
    private Vector3 originalScale;
    private Rigidbody2D body;

    protected override Status OnStart()
    {
        if (Npc?.Value != null)
        {
            anim = Npc.Value.GetComponent<Animator>();

            originalScale = Npc.Value.transform.localScale;

            Vector2 npcPos = Npc.Value.transform.position;
            int numberOfPoints = Random.Range(1, 5);
            waypoints.Clear();
            waypoints.Add(npcPos);

            for (int i = 0; i < numberOfPoints; i++)
            {
                waypoints.Add(new Vector2(npcPos.x + Random.Range(-1f, 1f), npcPos.y + Random.Range(-1f, 1f)));
            }

            body = Npc.Value.GetComponent<Rigidbody2D>();
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        GameObject npc = Npc?.Value;
        if (npc == null || waypoints == null || waypoints.Count == 0)
            return Status.Failure;

        if (currentWaypointIndex >= waypoints.Count)
            currentWaypointIndex = 0;

        Vector2 npcPos = npc.transform.position;
        Vector2 targetPos = waypoints[currentWaypointIndex];
        float distance = Vector2.Distance(npcPos, targetPos);

        if (distance <= reachThreshold)
        {
            idleTimer += Time.deltaTime;
            anim.SetBool("run", false);

            if (idleTimer >= idleDuration)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                idleTimer = 0f;
            }

            return Status.Running;
        }

        Vector2 direction = (targetPos - npcPos).normalized;
        //npc.transform.position = Vector2.MoveTowards(npcPos, targetPos, patrolSpeed * Time.deltaTime);
        body.linearVelocity = direction * patrolSpeed;

        if (direction.x != 0)
        {
            Vector3 scale = originalScale;
            scale.x = Mathf.Sign(direction.x) * Mathf.Abs(originalScale.x);
            npc.transform.localScale = scale;
        }

        anim.SetBool("run", true);
        return Status.Running;
    }


    protected override void OnEnd()
    {
    }
}
