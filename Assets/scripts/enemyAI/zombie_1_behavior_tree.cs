using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class zombie_1_behavior_tree : MonoBehaviour
{
    Root root_AI_Node;
    List<Node> children_AI_Nodes;
    Idle idle_AI_Node;
    Chase chase_AI_Node;
    public float chaseRange = 5;
    public Transform player;
    Vector2 direction;
    public float speed = 3;
    AStar2D navigation;
    public AiGrid grid;
    int pathIndex = 0;
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

        navigation = new AStar2D(grid);

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
        root_AI_Node.SetData("resetPath", false);
        root_AI_Node.SetData("cellExtent", grid.GetCellSize()*0.5f);

    }

    public void Update()
    {

        var withinRange = WithinChaseRangeCheck();
      
        root_AI_Node.SetData("withinChaseRange", withinRange);
      
           

        root_AI_Node.Evaluate();


        if((bool)root_AI_Node.GetData("chasing"))
        {

            
         
            if(navigation.GetPathFound())
            {
                
                var path = navigation.GetPath();

                if (pathIndex >= path.Count || (bool)root_AI_Node.GetData("resetPath"))
                {

                    pathIndex = 0;
                    navigation.ResetPath();

                }
                else
                {
                    Vector2 goal = path[pathIndex].pos;

                    direction = -((Vector2)transform.position - goal).normalized;
                    root_AI_Node.SetData("movementDirection", direction);

                    transform.position += (Vector3)(direction * speed * Time.deltaTime);

                    if (Vector2.Distance(transform.position, goal) <
                        (float)root_AI_Node.GetData("cellExtent"))
                    {
                        pathIndex++;


                    }

                }
            }
            else
            {
                navigation.AStarSearch(transform.position, player.position);
            }
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