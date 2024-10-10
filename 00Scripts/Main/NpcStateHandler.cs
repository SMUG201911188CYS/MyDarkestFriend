using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NpcStateHandler : MonoBehaviour
{
    [SerializeField] private Enums.EnemyState enemyState;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private Transform destinationPoint;
    private Transform runDestinationPoint;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float coefficient;

    private Transform target;
    
    private bool isCaught = false;
    private bool isWaring = false;
    
    private static readonly int IsMoving  = Animator.StringToHash("IsMoving");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int IsWarning = Animator.StringToHash("IsWarning");
    private static readonly int IsCaught = Animator.StringToHash("IsCaught");

    void Start()
    {
        coefficient = Random.Range(1f, 5f);
        enemyState = Enums.EnemyState.Idle;
        
        animator.SetBool(IsMoving, false);
        animator.SetBool(IsRunning, false);
        
        runDestinationPoint = transform.parent.parent.parent.GetComponent<NpcSpawner>().runDestinationPoint;
        
        destinationPoint = Random.Range(0, 2) == 0 ? leftLimit : rightLimit;

        transform.position = destinationPoint == leftLimit ? rightLimit.position : leftLimit.position;
        
        // wait
        Invoke(nameof(ChangeToMove), Random.Range(1, 3));
    }

    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case Enums.EnemyState.Idle:
                Idle();
                break;
            
            case Enums.EnemyState.Move:
                Direction();
                
                Move();
                break;
            
            case Enums.EnemyState.Following:
                if (!target)
                {
                    enemyState = Enums.EnemyState.Idle;
                    ChangeToMove();
                }
                DirectionToTarget();
                
                MoveTo();
                break;
            
            case Enums.EnemyState.Caught:
                Caught();
                break;
            
            case Enums.EnemyState.Warning:
                Warning();
                break;
            
            case Enums.EnemyState.Run:
                Run();
                break;
            
            case Enums.EnemyState.Hide:
                Hide();
                break;
        }
    }

    void Idle()
    {
        animator.SetBool(IsMoving, false);
        animator.SetBool(IsRunning, false);
    }

    async void Caught()
    {
        if (isCaught) return;
        CancelInvoke();

        isCaught = true;
        await UniTask.Delay(10000);

        isCaught = false;
        enemyState = Enums.EnemyState.Idle;
        animator.Play("Npc_A_Stand");
        ChangeToMove();
    }
    
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destinationPoint.position, moveSpeed * Time.fixedDeltaTime);

        animator.SetBool(IsMoving, true);
        
        if (Vector2.Distance(transform.position, destinationPoint.position) < 0.1f * coefficient)
        {
            // change state to idle
            enemyState = Enums.EnemyState.Idle;
            
            // new destination point
            destinationPoint = destinationPoint == rightLimit ? leftLimit : rightLimit;
            coefficient = Random.Range(1f, 5f);
            
            // wait
            Invoke(nameof(ChangeToMove), Random.Range(1, 3));
        }
    }
    
    void ChangeToMove()
    {
        if (enemyState == Enums.EnemyState.Idle)
            enemyState = Enums.EnemyState.Move;
    }

    void Direction()
    {
        if (destinationPoint == rightLimit)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    void DirectionToTarget()
    {
        if(!target) return;
        
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    public void SetTarget(Transform t)
    {
        target = t;
        DirectionToTarget();
        enemyState = Enums.EnemyState.Following;
    }
    
    async void MoveTo()
    {
        if(!target) return;
        
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.fixedDeltaTime);

        animator.SetBool(IsMoving, true);
        
        if (Vector2.Distance(transform.position, new Vector2(target.position.x, transform.position.y)) < 0.1f * coefficient)
        {
            // change state to idle
            enemyState = Enums.EnemyState.Idle;
            
            // new destination point
            destinationPoint = destinationPoint == rightLimit ? leftLimit : rightLimit;
            coefficient = Random.Range(1f, 5f);

            await UniTask.Delay(4000);
            enemyState = Enums.EnemyState.Idle;
            Direction();
            ChangeToMove();
        }
    }
    
    public void ChangeToWarning()
    {
        enemyState = Enums.EnemyState.Warning;
    }
    
    public void ChangeToCaught()
    {
        animator.SetBool(IsRunning, false);
        animator.SetBool(IsMoving, false);
        animator.Play("Caught");
        enemyState = Enums.EnemyState.Caught;
    }
    
    void Warning()
    {
        animator.SetBool(IsMoving, false);
        animator.SetBool(IsRunning, true);
        
        if (FindObjectsOfType<NpcStateHandler>().Length >= 2 && !isWaring)
        {
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Risk, 2f);
            isWaring = true;
        }
        // wait
        Invoke(nameof(ChangeToRun), .5f);
    }
    
    public void ChangeToRun()
    {
        if (enemyState == Enums.EnemyState.Dead)
        {
            // do nothing
        }
        else
        {
            animator.SetBool(IsRunning, false);
            animator.SetBool(IsMoving, false);
            animator.Play("Run");
            
            enemyState = Enums.EnemyState.Run;
            
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Run()
    {
        if (Player.LockedDoor)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(runDestinationPoint.position.x - 1.3f, transform.position.y), runSpeed * Time.fixedDeltaTime);
            if (Vector2.Distance(transform.position, new Vector2(runDestinationPoint.position.x - 1.3f, transform.position.y)) < 0.1f)
            {
                animator.Play("Npc_A_Stand");
            }
        }
        else
        {
            animator.Play("Run");
            transform.position = Vector2.MoveTowards(transform.position, runDestinationPoint.position, runSpeed * Time.fixedDeltaTime);
        }
        
        if (Vector2.Distance(transform.position, runDestinationPoint.position) < 0.1f)
        {
            enemyState = Enums.EnemyState.Hide;
        }
    }

    void Hide()
    {
        if (enemyState == Enums.EnemyState.Hide)
        {
            moveSpeed = 0;
            runSpeed = 0;
            
            StatManager.I.ChangePlayerStat(Enums.PlayerStat.Risk, 2f);
            Destroy(transform.parent.gameObject);
        }
    }
    
    public void Die()
    {
        enemyState = Enums.EnemyState.Dead;
        
        moveSpeed = 0;
        runSpeed = 0;

        CurrencyManager.I.AddCurrency(Random.Range(40, 60));

        Destroy(transform.parent.gameObject, .5f);
    }
}
