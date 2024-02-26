using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Zombie_Flock_Prefab_Script : MonoBehaviour
{
    Flock_Agent_Script flocking_script = new Flock_Agent_Script();
    CircleCollider2D agentCollider;

    Vector2 direction = Vector2.zero;
    Vector2 playerPos;
    BoxCollider2D box;
    Rigidbody2D rb;
    public float pushbackForce = 0.05f;

    public int healthPoints = 100;
    float chaseRange;
    Behavior_Tree.Root root_AI_Node;
    Animator animation;
    public float attackRange = 3;
    ParticleSystem blood;
    public bool physicsKNockback = false;
    public Behavior_Tree.Root Root_AI_Node { get { return root_AI_Node; } }
    public float ChaseRange { set { chaseRange = value; } }
    public Vector2 PlayerPos { set { playerPos = value; } }
    public Vector2 Direction { get { return direction; } set { direction = value; } }
    public CircleCollider2D AgentCollider { get { return agentCollider; } }
    public Flock_Agent_Script Flock_Agent { get { return flocking_script; } }
  
    void Start()
    {
        var ps = GameObject.FindObjectsByType<ParticleSystem>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (ParticleSystem pas in ps)
        {
            if (pas.gameObject.name == "BloodSplatter")
            {
                blood = Instantiate<ParticleSystem>(pas, this.transform);
                break;
            }
        }
        blood.gameObject.SetActive(true);
        blood.Stop();

        animation = GetComponent<Animator>();
        agentCollider = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Behavior_Tree.Idle idle_AI_Node = new Behavior_Tree.Idle();
        Behavior_Tree.Chase chase_AI_Node = new Behavior_Tree.Chase();
        Behavior_Tree.Attack attack_AI_Node = new Behavior_Tree.Attack();
        Behavior_Tree.Death death_AI_Node = new Behavior_Tree.Death();

        List<Behavior_Tree.Node> children_AI_Nodes;

        children_AI_Nodes = new List<Behavior_Tree.Node>
        {
            idle_AI_Node,
            death_AI_Node,
            chase_AI_Node,
            attack_AI_Node
        };

        root_AI_Node = new Behavior_Tree.Root(children_AI_Nodes);


        root_AI_Node.SetData("withinChaseRange", false);
        root_AI_Node.SetData("dead", false);
        root_AI_Node.SetData("withinAttackRange", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "bullet")
        //{
        //    animation.SetBool("attacking", false);
        //    animation.SetBool("chasing", false);

        //    int damage = collision.gameObject.GetComponent<Bullet>().damage;
        //    healthPoints -= damage;
        //    animation.Play("hurt");

        //    blood.Play();

        //    //rb.AddForce((collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce, ForceMode2D.Impulse);
           
        //}

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
            if(physicsKNockback)
                rb.AddForce((collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce, ForceMode2D.Impulse);
            else
                transform.position += (collision.transform.position - GameObject.Find("Player").transform.position).normalized * pushbackForce;
        }
    }

    public bool WithinChaseRangeCheck()
    {

        return ((Vector2)transform.position - playerPos).magnitude < chaseRange;
    }

    private void Update()
    {

       

        blood.transform.position = transform.position;
        root_AI_Node.SetData("dead", healthPoints <= 0);

        Behavior_Tree.NodeState state = root_AI_Node.Evaluate();

        if (state != Behavior_Tree.NodeState.FAILURE)
        {
            switch ((Behavior_Tree.Current_Leaf_Node)root_AI_Node.GetData("currentLeafNode"))
            {
                default:
                    break;

                case Behavior_Tree.Current_Leaf_Node.IDLE:
                    {
                        break;
                    }
                case Behavior_Tree.Current_Leaf_Node.CHASE:
                    {
                        animation.SetBool("chasing", true);
                        break;
                    }
                case Behavior_Tree.Current_Leaf_Node.ATTACK:
                    {
                        animation.SetBool("attacking", true);
                        break;
                    }
                case Behavior_Tree.Current_Leaf_Node.DEATH:
                    {
                        int nr = Random.Range(0, 1);

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
            }
        }
    }
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(animation.GetCurrentAnimatorStateInfo(0).length);

        Destroy(this.gameObject);
    }
}
