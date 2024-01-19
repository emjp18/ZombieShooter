using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class zombie_1_behavior_tree : MonoBehaviour
{
    Root root_AI_Node;
    List<Node> children_AI_Nodes;
    Idle idle_AI_Node;
    Chase chase_AI_Node;
    public float chaseRange = 5;
    public Transform player;
    Vector3 direction;
    public float speed = 3;

    private bool WithinChaseRangeCheck()
    {
        if ((transform.position - player.position).magnitude < chaseRange)
        {
            return true;
        }
        else
        {
          return false;
        }
            
    }

    public void Start()
    {

        idle_AI_Node = new Idle();
        chase_AI_Node = new Chase();

        children_AI_Nodes = new List<Node>
        {
            idle_AI_Node,
            chase_AI_Node
        };

        root_AI_Node = new Root(children_AI_Nodes);

        root_AI_Node.SetData("withinChaseRange", WithinChaseRangeCheck());
        root_AI_Node.SetData("movementDirection", Vector2.zero);
        root_AI_Node.SetData("chasing", false);
    }

    public void Update()
    {

        var withinRange = WithinChaseRangeCheck();
        direction = -(transform.position - player.position).normalized;

        root_AI_Node.SetData("withinChaseRange", withinRange);
        if(withinRange)
            root_AI_Node.SetData("movementDirection", direction);

        root_AI_Node.Evaluate();


        if((bool)root_AI_Node.GetData("chasing"))
        {

            transform.position += (direction * speed*Time.deltaTime);
        }


    }

}
namespace Behavior_Tree
{
    public class Idle : Node
    {
        public Idle() : base() { }
        public override NodeState Evaluate()
        {
            if ((bool)GetData("withinChaseRange"))
            {
             
                return NodeState.FAILURE;
            }
            SetData("movementDirection", Vector2.zero);
            SetData("chasing", false);

            return NodeState.RUNNING;
        }
    }
    public class Chase : Node
    {
        public Chase() : base() { }
       
        public override NodeState Evaluate()
        {
            if (!(bool)GetData("withinChaseRange"))
            {
                return NodeState.FAILURE;
            }

            SetData("chasing", true);


            return NodeState.RUNNING;
        }
}
}