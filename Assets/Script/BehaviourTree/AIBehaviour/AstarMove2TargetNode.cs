using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AstarMove2TargetNode : ActionNode {
    AStarAgent aStarAgent;
    AgentBT btAgent;
    Transform currentTarget;
    private float timer = 0f;
    private const float clearPathInterval = 0.4f;

    public AstarMove2TargetNode(AgentBT btAgent) {
        this.btAgent = btAgent;
        aStarAgent = btAgent.gameObject.GetComponent<AStarAgent>();

        currentTarget = btAgent.target;
        aStarAgent.Pathfinding(currentTarget.position);
    }

    public override NodeStatus Execute() {
        MoveToTarget();
        return NodeStatus.SUCCESS;
    }

    private void MoveToTarget() {
        timer += Time.fixedDeltaTime;
        if (timer > clearPathInterval) {
            if(currentTarget.transform.position != btAgent.target.transform.position) {
                currentTarget = btAgent.target;
                aStarAgent.Pathfinding(currentTarget.position);
            }
            timer = 0f;
        }
        Debug.Log(aStarAgent.Status);
        if (aStarAgent.Status == AStarAgentStatus.Finished) {
            currentTarget = btAgent.target;
            aStarAgent.Pathfinding(currentTarget.position);
        }
        if (aStarAgent.Status == AStarAgentStatus.Invalid) {
            currentTarget = btAgent.target;
            aStarAgent.Pathfinding(currentTarget.position);
        }
    }
}
