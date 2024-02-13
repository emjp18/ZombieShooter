using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;


public class zombie_alone : MonoBehaviour
{
    BoxCollider2D box;
    Rigidbody2D rb;
    public float pushbackForce = 3.5f;

    public int healthPoints = 100;

    Root root_AI_Node;

    public ParticleSystem blood;

    public float chaseRange = 10;
    public float attacRange = 5;
    Vector2 dir;

    public float speed = 3;
    AStar2D navigation;

    int pathIndex = 0;

    Vector2 moveVector;
    public Vector2 MoveVector { get { return moveVector; } set { moveVector = value; } }
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    bool hasPlayedDeathAni = false;
    
    bool[] behaviourOrderBooleans = new bool[3];

    public bool[] BehaviourOrderBooleans { get { return behaviourOrderBooleans; } }
    public enum behavior_order { align = 0, avoid = 1, cohesion = 2 };

    public Transform player;
    public AiGrid grid;
    Animator animation;

   


    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        animation = GetComponent<Animator>();

        navigation = new AStar2D(grid);

        Idle idle_AI_Node = new Idle();
        Chase chase_AI_Node = new Chase();
        Attack attack_AI_Node = new Attack();
        Death death_AI_Node = new Death();

        List<Node> children_AI_Nodes;

        children_AI_Nodes = new List<Node>
        {
            idle_AI_Node,
            death_AI_Node,
            chase_AI_Node,
            attack_AI_Node
        };

        blood = Instantiate<ParticleSystem>(blood, transform);
        blood.gameObject.SetActive(true);
        
        root_AI_Node = new Root(children_AI_Nodes);

        root_AI_Node.SetData("withinChaseRange", WithinChaseRangeCheck());
        root_AI_Node.SetData("movementDirection", Vector2.zero);
        root_AI_Node.SetData("chasing", false);
        root_AI_Node.SetData("dead", false);
        root_AI_Node.SetData("attacking", false);
        root_AI_Node.SetData("resetPath", false);
        root_AI_Node.SetData("cellExtent", grid.GetCellSize() * 0.5f);
        root_AI_Node.SetData("withinAttackRange", WithinAttackRangeCheck());
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
    bool WithinAttackRangeCheck()
    {
        if ((transform.position - player.position).magnitude < attacRange)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public void IsDead()
    {
        hasPlayedDeathAni = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            animation.SetBool("attacking",false);
            animation.SetBool("chasing", false);

            int damage = collision.gameObject.GetComponent<weapon_DamageScript>().damagePerHit;
            healthPoints -= damage;
            animation.Play("hurt");
            blood.Play();
            rb.AddForce((collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce, ForceMode2D.Impulse);
        }
        
    }
    private void FixedUpdate()
    {
        var coll = Physics2D.OverlapCircleAll(transform.position, box.size.x*0.25f);


        foreach(Collider2D col in coll)
        {
            if(col.gameObject.tag =="Player")
            {
                root_AI_Node.SetData("withinAttackRange", true);
               
            }
            else
            {
                root_AI_Node.SetData("withinAttackRange", false);
            }
        }
        
    }

    void Update()
    {
        blood.transform.position = transform.position;

        root_AI_Node.SetData("dead", healthPoints<=0);

        root_AI_Node.SetData("withinChaseRange", (transform.position - player.position).magnitude < chaseRange);
 

     
   

        animation.SetBool("attacking", false);
        

        NodeState state = root_AI_Node.Evaluate();

        if(state!=NodeState.FAILURE)
        {

      

            switch((Current_Leaf_Node)root_AI_Node.GetData("currentLeafNode"))
            {
                case Current_Leaf_Node.IDLE:
                    {
                        
                        animation.SetBool("chasing", false);
                        animation.SetBool("attacking", false);
                        break;
                    }
                case Current_Leaf_Node.CHASE:
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

                                if((goal-(Vector2)player.position).magnitude>4)
                                {
                                    pathIndex = 0;
                                    navigation.ResetPath();
                                    navigation.AStarSearch(transform.position, player.position);
                                }

                                dir = -((Vector2)transform.position - goal).normalized;
                                root_AI_Node.SetData("movementDirection", dir);
                                animation.SetBool("chasing", true);
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

                        break;
                    }
                case Current_Leaf_Node.ATTACK:
                    {
                        
                        animation.SetBool("chasing", false);
                        animation.SetBool("attacking", true);
                      
                    
                        break;
                    }
                case Current_Leaf_Node.DEATH:
                    {
                        int nr = Random.Range(0,1);

                        switch (nr)
                        {
                            case 0:
                            {
                                    animation.Play("death_01");
                                    break;
                            }
                            case 1:
                                {
                                    animation.Play("death_02");
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        
                        Dying();
                      
                       


                        
                            
                        break;
                    }
                     default:
                    {
                        break;
                    }
            }
        }

        
      
        
    }
     IEnumerator Dying()
    {
        yield return new WaitForSeconds(animation.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        Destroy(this.gameObject);
    }
    
}
