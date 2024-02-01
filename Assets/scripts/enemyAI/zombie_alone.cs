using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_alone : MonoBehaviour
{
    Root root_AI_Node;
    List<Node> children_AI_Nodes;
    Idle idle_AI_Node;
    Chase chase_AI_Node;
    public float chaseRange = 5;
    Vector2 dir;

    public float speed = 3;
    AStar2D navigation;

    int pathIndex = 0;

    Vector2 moveVector;
    public Vector2 MoveVector { get { return moveVector; } set { moveVector = value; } }
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

   
    bool[] behaviourOrderBooleans = new bool[3];

    public bool[] BehaviourOrderBooleans { get { return behaviourOrderBooleans; } }
    public enum behavior_order { align = 0, avoid = 1, cohesion = 2 };

    public Transform player;
    public AiGrid grid;


    void Start()
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
        root_AI_Node.SetData("cellExtent", grid.GetCellSize() * 0.5f);

    }
    bool WithinChaseRangeCheck()
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
    void Update()
    {
        var withinRange = WithinChaseRangeCheck();

        root_AI_Node.SetData("withinChaseRange", withinRange);



        root_AI_Node.Evaluate();


        if ((bool)root_AI_Node.GetData("chasing"))
        {




            if (navigation.GetPathFound())
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

                    dir = -((Vector2)transform.position - goal).normalized;
                    root_AI_Node.SetData("movementDirection", dir);

                    transform.position += ((Vector3)(dir)) * speed * Time.deltaTime;

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
