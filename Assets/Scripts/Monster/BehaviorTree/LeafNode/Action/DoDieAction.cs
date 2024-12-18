using UnityEngine;

public class DoDieAction : Node
{
    Blackboard _blackboard;

    Animator animator;
    public DoDieAction(string name, Blackboard blackboard) : base(name)
    {
        _blackboard = blackboard;
        animator = _blackboard.GetValue<Animator>("Animator");
    }

    public override NodeState Evaluate()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.GetComponent<MonsterAI>().DestroyMonsterAI();
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
