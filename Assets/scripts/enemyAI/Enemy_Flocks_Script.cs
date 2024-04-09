using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flocks_Script : MonoBehaviour
{
    [SerializeField]
    List<Transform> spawn_points = new List<Transform>();
    List<Flock_Group_Script> groups = new List<Flock_Group_Script>();
    [Range(0f, 10f)]
    public float weightCohesion = 2.5f;
    [Range(0f, 10f)]
    public float weightAlignment = 2.5f;
    [Range(0f, 10f)]
    public float weightAvoidance = 2.5f;
    //[Range(0f, 10f)]
   float weightStayWithinRadius = 2.5f;
    [Range(0f, 10f)]
    public float speed = 3;
    [Range(1f, 10f)]
    public float chaseRange = 5;
    float maxSpeed = 10;
    public int layerCount =6;
    public int agentCount = 5;
    public Zombie_Flock_Prefab_Script agentPreFab; // ZOMBIE ONE (BUGS IF I CHANGE THIS NAME, 
    public Zombie_Flock_Prefab_Script zombieOne;
    public Zombie_Flock_Prefab_Script zombieTwo;
    public Zombie_Flock_Prefab_Script zombieThree;
    


    public Transform player;
    public float avoidanceThreshold = 1;
    public float alignThreshold = 1;
    public float cohesionThreshold = 1;
    float gridcellsize;
    public AiGrid grid;
    AStar2D navigation;
    private void Start()
    {
        gridcellsize = grid.GetCellSize();

        Zombie_Flock_Prefab_Script[] zombiePrefabs = new Zombie_Flock_Prefab_Script[] 
        { agentPreFab, zombieOne, zombieTwo /*, zombieThree, zombieFour*/ };

        foreach (Transform t in spawn_points)
        {
            List<Zombie_Flock_Prefab_Script> agents = new List<Zombie_Flock_Prefab_Script>();
            for (int i = 0; i < agentCount; i++)
            {
                // SELECT RANDOM PREFAB FROM ARRAY
                Zombie_Flock_Prefab_Script selectedPrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
                agents.Add(Instantiate(selectedPrefab, t.position, t.rotation));
            }

            groups.Add(new Flock_Group_Script(t, agentCount, chaseRange, weightCohesion, weightAvoidance, weightAlignment,
                weightStayWithinRadius, speed, avoidanceThreshold, alignThreshold, cohesionThreshold, layerCount, maxSpeed, agents, gridcellsize));
        }


        navigation = new AStar2D(grid);
    }
    private void Update()
    {
       for(int i=0;i<groups.Count; i++)
        {
            
            groups[i].UpdateVariables(chaseRange, weightCohesion, weightAvoidance, weightAlignment,
                   weightStayWithinRadius, speed, avoidanceThreshold,alignThreshold,cohesionThreshold);
            groups[i].Update(player.position, navigation);

         
        }
    }

 
}
