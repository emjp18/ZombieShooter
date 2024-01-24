using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class flock_group : MonoBehaviour
{

    public flock_agent agentPrefab;
    public int agentInstances = 5;
    List<flock_agent>  agents = new List<flock_agent>();
    Collider2D[] nearbyCollider;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float flockRadius = 2.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = flockRadius * flockRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

       for(int i =0; i< agentInstances; i ++)
        {
            agents.Add(Instantiate(agentPrefab));

            agents[i].player = GameObject.Find("player").transform;
            agents[i].grid = GameObject.FindAnyObjectByType<AiGrid>();

            agents[i].transform.position = new Vector3(2.80f+i, 2.40f+i, 0);    
        }

    }

    Collider2D[] GetNearbyObjects(Vector2 pos, Collider2D box)
    {
       
       return Physics2D.OverlapCircleAll(pos, flockRadius);

      
    }

    public void Update()
    {
        foreach (var agent in agents)
        {
            agent.Flocked = false;
            nearbyCollider = GetNearbyObjects(agent.transform.position, agent.AgentCollider);
            

            Vector2 move = Vector2.zero;
            if(nearbyCollider.Length>0)
            {
                if (agent.align)
                {
                    move += agent.BehaviorAlign.CalculateMove(agent, nearbyCollider, squareAvoidanceRadius);



                }
                if (agent.avoid)
                {
                    move += agent.BehaviorAvoid.CalculateMove(agent, nearbyCollider, squareAvoidanceRadius);
                }

                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
               
            }
            agent.MoveVector = move;
            agent.Flocked = true;


        }
    }


}
