using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Jump = Animator.StringToHash("Jump");

    public float speed;
    public Transform groundCheck;
    public LayerMask groundMask;

    private float gravityValue = -9.81f;

    private Animator animator;
    private CharacterController controller;
    private Vector3 playerVelocityY = Vector3.zero;

    private float prevPlayerXPosition = 0f;
    private float targetPlayerXPosition = 0f;
    private int xSize = 3;
    private float timeElapsed = 0f;

    private bool isJumping;
    private bool isGrounded;

    private int previousScore = 0;

    private void Start()
    {
        GameScoreManager.scoreChanged += ChangePlayerSpeedByScore;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void ChangePlayerSpeedByScore(int score)
    {
        if (previousScore + 3000 > score) return;
        previousScore = score;
        speed += 5f;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);
        UpdateInput();
        UpdateMove();
    }

    private void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && canMove(-xSize))
        {
            prevPlayerXPosition = transform.position.x;
            targetPlayerXPosition = transform.position.x - xSize;
            timeElapsed = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMove(xSize))
        {
            prevPlayerXPosition = transform.position.x;
            targetPlayerXPosition = transform.position.x + xSize;
            timeElapsed = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            animator?.SetTrigger(Jump);
        }
    }

    private bool canMove(int direction)
    {
        bool isMoving = 0 < timeElapsed && timeElapsed < 1f;
        if (isMoving) return false;

        int playerX = (int)transform.position.x;
        if (direction < 0) return -(xSize) <= playerX + direction;
        if (direction > 0) return playerX + direction <= xSize;
        return false;
    }

    private void UpdateMove()
    {
        if (controller is null) return;

        // Ground checking
        if (isGrounded && playerVelocityY.y < 0)
        {
            playerVelocityY.y = -2f;
        }

        if (timeElapsed < 1f)
        {
            controller.enabled = false;
            timeElapsed += speed * Time.deltaTime;
            float t = timeElapsed / 1f;
            float lerpX = Mathf.Lerp(prevPlayerXPosition, targetPlayerXPosition, t);
            transform.position = new Vector3(lerpX, transform.position.y, transform.position.z);
            controller.enabled = true;
        }

        if (isGrounded && isJumping)
        {
            isJumping = false;
            playerVelocityY.y = Mathf.Sqrt(2f * -2f * gravityValue);
            // playerVelocityY.y = 2f;
        }

        playerVelocityY.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocityY * Time.deltaTime);
        controller.Move(Vector3.forward * (speed * Time.deltaTime));
    }
}