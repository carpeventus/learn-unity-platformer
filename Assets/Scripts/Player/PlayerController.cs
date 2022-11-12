using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;
    
    [Header("Move")]
    public float moveSpeed = 10.0f;
    // public LayerMask groundLayer;
    public Vector2 groundRayCastOffset;
    public Vector2 wallRayCastOffset;
    private Vector2 direction;
    private bool canMove;
    private bool canFlip;


    [Header("TouchingGround")]
    public float groundLength = 1.1f;
    public float wallLength = 0.5f;
    public float wallSlideSpeed = 0.2f;

    [Header("PlayerStatus")]
    public bool onGround;
    public bool isTouchingWall;
    public bool isWallSliding;
    [SerializeField] private bool onOneWayPlatform;
    [SerializeField] private bool isLadder;
    [SerializeField] private bool isClimbing;


    [Header("Climb")]
    public float climbSpeed = 4;


    [Header("Jump")]
    public float jumpForce = 16.0f;
    public int jumpsLeftNum;
    public int totalJumpNum = 1;
    public float airDragMultiplier = 0.8f;
    public float wallJumpForce = 10.0f;
    public Vector2 wallJumpDirection;
    public float gravity = 1.0f;   
    public float linearDrag = 4f;
    public float fallMultiplier = 10f;
    public float oneWayPlatformFallTime = 0.2f;

    private float jumpTimer;
    public float jumpDelay = 0.15f;
    private int faceDirection;
    private const int FACE_RIGHT = 1;
    private const int FACE_LEFT = -1;
    
    [Header("Input")] private bool isHoldingJump;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        jumpsLeftNum = totalJumpNum;
        faceDirection = FACE_RIGHT;
        canFlip = true;
        canMove = true;
        // 归一化，作用主要是防止向量的定义影响到其大小，因此让其模始终为1
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.isGamePlay()) {
            return;
        }
        CheckTouching();
        CheckPlayerStatus();
        UpdateAnimations();

    }

    private void FixedUpdate() {
        if (!GameController.isGamePlay()) {
            return;
        }
        Move();
        Jump();
        Climb();
        ModifyPhysics();
    }
    

    // 输入监听
    public void InputJump(InputAction.CallbackContext ctx) {
        isHoldingJump = ctx.action.IsPressed();
        if (onOneWayPlatform && isHoldingJump && direction.y < -0.1f) {
            IgnoreOneWayPlatformCollision(oneWayPlatformFallTime);
        } else if (ctx.action.WasPerformedThisFrame()) {
            jumpTimer = Time.time + jumpDelay;
        }
    }
    
    
    public void InputMove(InputAction.CallbackContext ctx) {
        direction = ctx.ReadValue<Vector2>();
    }

    private void CheckClimbing() {
        isClimbing = isLadder && Mathf.Abs(direction.y) > 0.5f;
    }

    private void IgnoreOneWayPlatformCollision(float time) {
        // 已经忽略碰撞就不执行
        if ( Physics2D.GetIgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("OneWayPlatform"))) {
            return;
        }
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("OneWayPlatform"));
        StartCoroutine(RestoreCollisionWithOeWayPlatform(time));
    }

    private IEnumerator RestoreCollisionWithOeWayPlatform(float time) {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("OneWayPlatform"), false);
    }

    private void Jump() {
        if (Time.time < jumpTimer && jumpsLeftNum > 0) {
            DoJump();
        }
    }
    
    private void DoJump() {
        if (!onGround && isTouchingWall) {
            WallJump();
        } else {
            NormalJump();
        }
    }

    private void CheckPlayerStatus() {
        CheckFlip();
        CheckIfCanJump();
        CheckWallSlide();
        CheckClimbing();
    }
    
    
    private void CheckTouching() {
        int platform = LayerMask.GetMask("Platform");
        onOneWayPlatform = col.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isLadder = col.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        onGround = Raycast(groundRayCastOffset, Vector2.down, groundLength, platform)
                   || Raycast(-groundRayCastOffset, Vector2.down, groundLength, platform) || onOneWayPlatform;

        // transform.right 是gameObject上的红色X轴，因此转向时候，X轴也跟着转向了。可以简单认为transform.right就是角色面向的方向
        isTouchingWall = Raycast(wallRayCastOffset, transform.right, wallLength, platform);
    }

    private void Climb() {

        if (isClimbing) {
            if (onOneWayPlatform) {
                IgnoreOneWayPlatformCollision(oneWayPlatformFallTime);
            }
            anim.SetBool("climbing", true);
            rb.velocity = new Vector2(rb.velocity.x, direction.y * climbSpeed);
            rb.gravityScale = 0.0f;
        } else if (ParkAtLadder()) {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        } else {
            anim.SetBool("climbing", false);
        }
    }
    

    private void CheckWallSlide() {
        isWallSliding =  !onGround && isTouchingWall && InputDirectionEqualsFaceDirection() && rb.velocity.y < 0;
    }

    private void CheckIfCanJump()
    {
        if(onGround && rb.velocity.y < 0.01f || isWallSliding)
        {
            jumpsLeftNum = totalJumpNum;
        }
    }


    private void NormalJump() {

        // 重置Y轴速度
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        DecreaseJumpCount();
    }

    private void DecreaseJumpCount() {
        jumpsLeftNum--;
        jumpTimer = 0;
    }
    
    private void WallJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection.x * (-faceDirection), wallJumpForce * wallJumpDirection.y), ForceMode2D.Impulse);
        DecreaseJumpCount();
    }
    
    void ModifyPhysics() {
        if (isClimbing || ParkAtLadder()) {
            return;
        }

        if (onGround) {
            rb.gravityScale = 0f;
            AddDragWhenStartMoveOrTurnAround();
        } else  {
            InitPhysicsWhenJumping();
            if (RisingButReleaseJump()) {
                // 放开后受到的重力中等
                rb.gravityScale = gravity * (fallMultiplier / 2);
            } else if (Falling()){
                // 下降时重力最大
                rb.gravityScale = gravity * fallMultiplier;
            } 
        }
        
        if (isWallSliding) {
            HoldSpeedWhenWallSlide();
        }

    }

    private bool ParkAtLadder() {
        return !onGround && isLadder && !anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAndFall");
    }
    

    private void AddDragWhenStartMoveOrTurnAround() {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        if (Mathf.Abs(direction.x) < 0.4f || changingDirections) {
            rb.drag = linearDrag;
        }
        else {
            rb.drag = 0f;
        }
    }

    private void InitPhysicsWhenJumping() {
        rb.gravityScale = gravity;
        // 消除线性阻力（不完全）
        rb.drag = linearDrag * 0.15f;
    }

    private bool Falling() {
        return rb.velocity.y < 0;
    }
    
    private bool RisingButReleaseJump() {
        return rb.velocity.y > 0 && !isHoldingJump;
    }

    private void Move() {
        if (!canMove) {
            return;
        }
        // 空中的移动
        if (JustInAir()) {
            SlowDownHorizontal();
        }else {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
    }

    /**
     *  玩家在空中不按方键，逐步降低横向速度
     */
    private void SlowDownHorizontal() {
        rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
    }

    /*
     * 不在墙上且不在地面上时玩家在空中按左右
     */
    private bool JustInAir() {
        return !onGround && !isWallSliding && direction.x == 0;
    }
    private void HoldSpeedWhenWallSlide() {
        if (rb.velocity.y < - wallSlideSpeed) {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }
    
    private void UpdateAnimations() {
        anim.SetBool("isRuning", Mathf.Abs(rb.velocity.x) > 0.1f);
        anim.SetBool("onGround", onGround);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isLadder", isLadder);

    }
    
    private void CheckFlip() {
        if (ShouldFlip()) {
            DoFlip();
        }
    }

    private bool InputDirectionEqualsFaceDirection() {
        return faceDirection == FACE_RIGHT && direction.x > 0 || faceDirection == FACE_LEFT && direction.x < 0;
    }
    
    private bool ShouldFlip() {
        if (isWallSliding || !canFlip) {
            return false;
        }
        return direction.x > 0 && faceDirection == FACE_LEFT || direction.x < 0 && faceDirection == FACE_RIGHT;
    }
    
    private void DoFlip() {
        faceDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void DisableFlip() {
        canFlip = false;
    }
    
    private void EnableFlip() {
        canFlip = true;
    }
    
    private void DisableMove() {
        canMove = false;
    }
    
    private void EnableMove() {
        canMove = true;
    }

    private void DisableAction() {
        DisableFlip();
        DisableMove();
    }
    
    private void EnableAction() {
        EnableFlip();
        EnableMove();
    }
    RaycastHit2D Raycast(Vector2 offset, Vector2 dir, float distance, LayerMask layerMask) {
        Vector2 player = transform.position;
        return Physics2D.Raycast(player + offset, dir, distance, layerMask);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector2 position = transform.position;
        Gizmos.DrawLine(position + groundRayCastOffset, position + groundRayCastOffset + Vector2.down * groundLength);
        Gizmos.DrawLine(position - groundRayCastOffset, position - groundRayCastOffset + Vector2.down * groundLength);
        Gizmos.DrawLine(position + wallRayCastOffset, position + wallRayCastOffset + (Vector2)transform.right * wallLength);
    }

}
