using Behavior_Tree;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering;

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
    bool leafIdle = false;
    bool leafChase = false;
    Vector2 oldPlayerpos = Vector2.zero;
    Vector2 randomPos = Vector2.zero;
    bool lookingRight = true;

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
        blood.Stop();
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
 
            int damage = collision.gameObject.GetComponent<Bullet>().damage;
            healthPoints -= damage;
            animation.Play("hurt");
            blood.Play();
         
            rb.AddForce((collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce, ForceMode2D.Impulse);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            animation.SetBool("attacking", false);
            animation.SetBool("chasing", false);
    
            int damage = collision.gameObject.GetComponent<Bullet>().damage;
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
    void FindRandomPath()
    {
        int x = Random.Range(-100, 100);
        int y = Random.Range(-100, 100);
        if (x < 20 && x > 0)
            x = 20;
        if (y < 20 && y > 0)
            y = 20;
        if (x > -20 && x < 0)
            x = -20;
        if (y > -20 && y < 0)
            y = -20;
        randomPos.x = transform.position.x + x;
        randomPos.y = transform.position.y + y;
    }

    void Update()
    {
        Debug.Log("tjo katt"); 
        blood.transform.position = transform.position;

        root_AI_Node.SetData("dead", healthPoints<=0);

        root_AI_Node.SetData("withinChaseRange", (transform.position - player.position).magnitude < chaseRange);


        //if((player.position-transform.position).x<0&& lookingRight)
        //{
        //    lookingRight = false;
        //    GetComponentInParent<SpriteRenderer>().flipX = !GetComponentInParent<SpriteRenderer>().flipX;
        //}

        //if ((player.position - transform.position).x > 0 && !lookingRight)
        //{
        //    lookingRight = false;
        //    GetComponentInParent<SpriteRenderer>().flipX = !GetComponentInParent<SpriteRenderer>().flipX;
        //}
        animation.SetBool("attacking", false);
        

        NodeState state = root_AI_Node.Evaluate();

        if(state!=NodeState.FAILURE)
        {

            

            switch ((Current_Leaf_Node)root_AI_Node.GetData("currentLeafNode"))
            {
                 
                case Current_Leaf_Node.IDLE:
                    {
                        //Debug.Log("idle");
                     

                        if (leafChase)
                        {
                            pathIndex = 0;
                            navigation.ResetPath();
                        }

                        leafIdle = true;
                        leafChase = false;
                        if (navigation.GetPathFound())
                        {
                            //Debug.Log("foundrandom");
                           
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
                                animation.SetBool("chasing", true);
                                if (Vector2.Distance(transform.position, goal) <
                                    (float)root_AI_Node.GetData("cellExtent"))
                                {
                                    pathIndex++;

                                }

                            }
                        }
                        else
                        {
                            FindRandomPath();
                            navigation.AStarSearch(transform.position, randomPos);
                            animation.SetBool("chasing", false);
                            //Debug.Log(randomPos);
                            
                        }


                        //animation.SetBool("chasing", false);
                        animation.SetBool("attacking", false);
                        break;
                    }
                case Current_Leaf_Node.CHASE:
                    {
                    
                        //Debug.Log("chase");
                        if (leafIdle)
                        {
                      
                            pathIndex = 0;
                            navigation.ResetPath();
                        }
                        leafChase = true;
                        leafIdle = false;
                        if (navigation.GetPathFound())
                        {
                            if(((Vector2)player.position- (Vector2)oldPlayerpos).magnitude>5)
                            {
                                pathIndex = 0;
                                navigation.ResetPath();
                            }
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
                                animation.SetBool("chasing", true);
                                transform.position += ((Vector3)(dir)) * speed*2 * Time.deltaTime;

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
                            oldPlayerpos = player.position;
                        }

                        break;
                    }
                case Current_Leaf_Node.ATTACK:
                    {
                        
                        animation.SetBool("chasing", false);
                        animation.SetBool("attacking", true);
                        //Debug.Log("attack");

                        break;
                    }
                case Current_Leaf_Node.DEATH:
                    {
                        int nr = Random.Range(0,1);
                        //Debug.Log("death");
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


                       StartCoroutine(Dying());






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
     
        yield return new WaitForSeconds(animation.GetCurrentAnimatorStateInfo(0).length);

        SceneValues.coinsForPlayer += 1;

        Debug.Log(SceneValues.coinsForPlayer);
        Debug.Log("Ser man detta eller?");
        GetComponent<LootBag>().InstatiateLoot(transform.position);
        Destroy(this.gameObject);
    }
    
}
