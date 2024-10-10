using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NpcHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovementHandler playerMovementHandler;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runSpeed = 2f;
    
    [SerializeField] private float leftLimit = -1f;
    [SerializeField] private float rightLimit = 1f;
    private float currentLeftLimit;
    private float currentRightLimit;
    
    [FormerlySerializedAs("isMovingRight")] [SerializeField] private bool isMovingLeft = true;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isRun;
    
    private float targetX;
    
    [SerializeField] private float idleTime = 1f;
    [SerializeField] private float findDistance = 1f;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    [SerializeField] private float destination = 10f;

    private void Awake()
    {
        playerMovementHandler = FindObjectOfType<PlayerMovementHandler>();
    }

    private void Start()
    {
        isRun = false;
        
        StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
        isIdle = true; // 처음에는 대기 상태
        
        var coefficient = Random.Range(0.5f, 1.0f); // 0.5 ~ 1.0 사이의 값
                    
        // 좌우 이동 제한
        currentLeftLimit = leftLimit * coefficient;
        currentRightLimit = rightLimit * coefficient;
        
        // 이동 방향 설정
        targetX = isMovingLeft ? currentRightLimit : currentLeftLimit;
        // 스프라이트 방향 변경
        transform.localScale = new Vector3(isMovingLeft ? 1 : -1, 1, 1);
        
        // 이동 상태
        while (true)
        {
            if (isRun) // 달리기 상태
            {
                var currentX = transform.position.x;
                
                var newX = Mathf.MoveTowards(currentX, destination, runSpeed * Time.deltaTime);
                
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                
                if (Math.Abs(currentX - destination) < 0.1f)
                {
                    Destroy(gameObject);
                }
                
                yield return null;
            }
            else if (isIdle) // 대기 상태
            {
                // 대기 애니메이션
                animator.SetBool(IsMoving, false);
                
                // idleTime 동안 거리 측정
                var time = 0f;
                while (time < idleTime)
                {
                    time += Time.deltaTime; // 시간 증가
                    
                    // 왼쪽으로 이동 중이라면
                    if (isMovingLeft) 
                    {
                        // 플레이어와의 거리 측정
                        var distanceX = playerMovementHandler.currentPlayer.position.x - transform.position.x;
                
                        // 플레이어가 findDistance 이내에 있다면
                        if (Mathf.Abs(distanceX) < findDistance)
                        {
                            // 블랙인 경우
                            if (Player.playerType == Enums.PlayerType.Black)
                            {
                                // 오른쪽으로 이동
                                isMovingLeft = false;
                                // 스프라이트 방향 변경
                                transform.localScale = new Vector3(1, 1, 1);

                                // 대기 애니메이션 종료, 달리기 애니메이션 시작
                                animator.SetBool(IsMoving, false);
                                animator.SetBool(IsRunning, true);
                                // 달리기 상태로 변경
                                isRun = true;
                            }
                            else if (Player.playerType == Enums.PlayerType.White)
                            {
                                // 화이트인 경우 (아무것도 하지 않음)
                            }
                        }
                    }
                    
                    yield return null;
                }
                
                // 대기 상태 종료
                isIdle = false;
                
                // 스프라이트 방향 변경
                transform.localScale = new Vector3(isMovingLeft ? 1 : -1, 1, 1);
                // 도착 지점 설정
                targetX = isMovingLeft ? currentRightLimit : currentLeftLimit;
            }
            else // 이동 상태   
            {
                // 오른쪽 이동 중이라면
                if (!isMovingLeft)
                {
                    // 플레이어와의 거리 측정
                    var distanceX = playerMovementHandler.currentPlayer.position.x - transform.position.x;
                
                    // 플레이어가 findDistance 이내에 있다면
                    if (Mathf.Abs(distanceX) < findDistance)
                    {
                        if (Player.playerType == Enums.PlayerType.Black)
                        {
                            //  오른쪽으로 이동
                            isMovingLeft = false;
                            
                            // 스프라이트 방향 변경
                            transform.localScale = new Vector3(1, 1, 1);
                            
                            // 이동 애니메이션 종료, 달리기 애니메이션 시작
                            animator.SetBool(IsMoving, false);
                            animator.SetBool(IsRunning, true);
                            isRun = true;
                        }
                        else if (Player.playerType == Enums.PlayerType.White)
                        {
                            // 화이트인 경우 (아무것도 하지 않음)
                        }
                    }
                }
                
                // 이동 애니메이션
                animator.SetBool(IsMoving, true);
                
                var currentX = transform.position.x;
                
                var newX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);
                
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                
                if (Math.Abs(currentX - targetX) < 0.1f)
                {
                    isMovingLeft = !isMovingLeft;
                    isIdle = true;
                    
                    coefficient = Random.Range(0.5f, 1.0f);
                    
                    currentLeftLimit = leftLimit * coefficient;
                    currentRightLimit = rightLimit * coefficient;
                }
            }
            
            yield return null;
        }
    }

    public void Die()
    {
        moveSpeed = 0;
        runSpeed = 0;
        
        CurrencyManager.I.AddCurrency(Random.Range(40, 60));
        
        Destroy(gameObject, .5f);
    }
}
