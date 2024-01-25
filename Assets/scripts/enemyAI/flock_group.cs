using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class flock_group : MonoBehaviour
{

    [Range(1f, 10f)]
    public float weightCohesion = 2.5f;
    [Range(1f, 10f)]
    public float weightAlignment = 2.5f;
    [Range(1f, 10f)]
    public float weightAvoidance = 2.5f;

    float[] weights = new float[3];
    public float speed = 10;
    Vector2 dir;
    public int layerMask = 6;
    ContactFilter2D contactFilter = new ContactFilter2D();

    public flock_agent agentPrefab;
    public int agentInstances = 5;
    List<flock_agent>  agents = new List<flock_agent>();
    List<Collider2D> nearbyCollider = new List<Collider2D>();
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


    Vector2 RandomSpawn(flock_agent agent)
    {
        Vector2 vec = Vector2.zero;
        vec.x = Random.Range(agent.grid.UpperLeft.x  , agent.grid.LowerRightCorner.x);
        vec.y = Random.Range(agent.grid.LowerRightCorner.y, agent.grid.UpperLeft.y);

        return vec;

    }

    void Start()
    {
        contactFilter.layerMask = layerMask;
        

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = flockRadius * flockRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
      
        for (int i =0; i< agentInstances; i ++)
        {
            agents.Add(Instantiate(agentPrefab));

            agents[i].player = GameObject.Find("player").transform;
            agents[i].grid = GameObject.FindAnyObjectByType<AiGrid>();
            agents[i].transform.position = this.transform.position;


        }

        //var vecspawn = (Vector3)RandomSpawn(agents[0]);
        //for (int i = 0; i < agentInstances; i++)
        //{
        //    agents[i].transform.position = vecspawn;
           

        //}
            weights[0] = weightAlignment;
        weights[1] = weightAvoidance;
        weights[2] = weightCohesion;
    }

    //Collider2D[] GetNearbyObjects(Vector2 pos, float radius)
    //{
    //    Debug.Log(pos);

    //    var res = Physics2D.OverlapCircleAll(pos, radius*10, layerMask);
    //    Debug.Log(res.Length);

    //    return res;

    //}

    public void Update()
    {

        weights[0] = weightAlignment;
        weights[1] = weightAvoidance;
        weights[2] = weightCohesion;

        foreach (var agent in agents)
        {
            agent.Flocked = false;
            //nearbyCollider = GetNearbyObjects(agent.transform.position, agent.AgentCollider.bounds.extents.y);
            nearbyCollider.Clear();
           int count = agent.AgentCollider.OverlapCollider(contactFilter, nearbyCollider);


            Debug.Log(nearbyCollider.Count);

            Vector2 move = Vector2.zero;

            Vector2 partialMove = Vector2.zero;

            if (nearbyCollider.Count > 0)
            {
               
                for(int i=0; i<agent.Behaviours.Length; i ++)
                {
                    if (agent.BehaviourOrderBooleans[i])
                    {
                        partialMove = agent.Behaviours[i].CalculateMove(agent, nearbyCollider, squareAvoidanceRadius);
                        Debug.Log(partialMove);

                        if (partialMove != Vector2.zero)
                        {
                            if (partialMove.sqrMagnitude > weights[i] * weights[i])
                            {
                                partialMove.Normalize();
                                partialMove *= weights[i];
                            }

                            move += partialMove;
                            Debug.Log(partialMove);
                        }

                    }
                }

                
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
               
            }
            agent.MoveVector = move;
            agent.Flocked = true;
            dir = Vector2.zero;
            agent.EvaluateTree(ref dir);

            //agent.transform.position += ((Vector3)(dir)) * speed*Time.deltaTime;

            agent.transform.position += (Vector3)move.normalized*speed * Time.deltaTime;

            Debug.Log(move);
        }
    }


}
