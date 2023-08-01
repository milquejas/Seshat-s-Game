using UnityEngine;

/* 
 * Press screen -> move player towards spot
 * clamped movementDirection
 * Handles player animations
 * TODO: walk sounds
*/

[RequireComponent(typeof(Rigidbody2D))]
public class TouchMovementAndInteraction : MonoBehaviour, IPlayerTouch
{
    [SerializeField] private float minimumMove, movementSpeed, interactionCircleSize;
    [SerializeField] private LevelSpawnSO spawnPoint;
    [SerializeField] private Transform spriteAndAnimationChild;

    private Vector2 movementDirection, playerPosition;
    private bool disableMovement;
    private bool thisTouchInteracting;
    private bool isTouchMoving;
    private bool isFacingUp;
    [SerializeField] private bool isFacingLeft = true;

    // for animation
    private bool isInWalkAnim;
    private bool isWalking;

    public Rigidbody2D RB { get; private set; }
    [SerializeField] private Animator animatorFront;
    [SerializeField] private Animator animatorBack;
    private Animator currentAnimator;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        currentAnimator = animatorFront;

        // setup spawn
        transform.position = spawnPoint.CurrentSpawnLocation;
        //Camera.main.transform.position = spawnPoint.CurrentSpawnLocation;
    }

    public void SetSpawnLocation()
    {
        spawnPoint.CurrentSpawnLocation = transform.position;
    }

    void Update()
    {
        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }

        // keyboard movement
        if (disableMovement) return;

        if (!isTouchMoving)
        {
            movementDirection = GetKeyboardMovement();
            if (GetKeyboardMovement() != Vector2.zero)
            {
                isWalking = true;
                return;
            }
            isWalking = false;
        }
    }

    //P�ivitettty liike sopimaan paremmin isometriseen n�kym��n 6.7.23
    private Vector2 GetKeyboardMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Use these to adjust the movement speed in each direction
        float horizontalFactor = 1.0f;
        float verticalFactor = 0.5f;

        return new Vector2((horizontalInput + verticalInput) * horizontalFactor, (verticalInput - horizontalInput) * verticalFactor).normalized;
    }


    public void DisablePlayerMovement(bool disable)
    {
        disableMovement = disable;
        movementDirection = Vector2.zero;

        isWalking = false;
        AnimatePlayer();
    }


    private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase)
    {
        if (disableMovement)
        {
            return;
        }
        
        switch (touchPhase)
        {
            case TouchPhase.Began:
                if (InteractSystem.TryToInteract(touchPosition, interactionCircleSize))
                {
                    thisTouchInteracting = true;
                    RB.velocity = Vector2.zero;
                    return;
                }

                TouchBegin(touchPosition);
                break;

            case TouchPhase.Moved:
                if (thisTouchInteracting) return;

                TouchMoving(touchPosition);
                break;

            case TouchPhase.Stationary:
                if (thisTouchInteracting) return;

                TouchStationary(touchPosition);
                break;

            case TouchPhase.Ended:
                if (thisTouchInteracting)
                {
                    thisTouchInteracting = false;
                    return;
                }
                TouchEnding(touchPosition);
                break;
        }
    }

    private void TouchBegin(Vector2 touchPosition)
    {
        isTouchMoving = true;
        playerPosition = transform.position;
        movementDirection = touchPosition - playerPosition;
    }

    private void TouchMoving(Vector2 touchPosition)
    {
        playerPosition = transform.position;
        movementDirection = touchPosition - playerPosition;
    }

    private void TouchStationary(Vector2 touchPosition)
    {
        playerPosition = transform.position;
        movementDirection = touchPosition - playerPosition;
    }

    private void TouchEnding(Vector2 touchPosition)
    {
        movementDirection = Vector2.zero;
        isTouchMoving = false;
    }

    private void FixedUpdate()
    {
        if (disableMovement) return;

        MovePlayer();
        AnimatePlayer();
    }

    private void AnimatePlayer()
    {
        if (isWalking && !isInWalkAnim)
        {
            currentAnimator.SetTrigger("StartWalking");
            isInWalkAnim = true;
        }

        if (!isWalking && isInWalkAnim) 
        {
            currentAnimator.SetTrigger("StartIdle");
            isInWalkAnim = false;
        }
    }

    private void MovePlayer()
    {
        if (Mathf.Abs(movementDirection.x) > minimumMove || Mathf.Abs(movementDirection.y) > minimumMove)
        {
            
            movementDirection = movementDirection.normalized * movementSpeed;
            movementDirection = new Vector2(movementDirection.x, movementDirection.y * 0.75f);
            isWalking = true;
            PlayerFlipY();
            PlayerFlipX();
            RB.velocity = movementDirection;
        }
        else
        {
            isWalking = false;
            AnimatePlayer();
        }
    }

    private void PlayerFlipY()
    {
        if (movementDirection.y == 0) return;
        if (movementDirection.y <= 0 && isFacingUp) return;
        if (movementDirection.y > 0 && !isFacingUp) return;

        isFacingUp = !isFacingUp;

        animatorFront.gameObject.SetActive(isFacingUp);
        animatorBack.gameObject.SetActive(!isFacingUp);

        if (isFacingUp)
            currentAnimator = animatorFront;
        else
            currentAnimator = animatorBack;
        isInWalkAnim = false;
    }

    private void PlayerFlipX()
    {
        if (movementDirection.x == 0) return;
        if (movementDirection.x < 0 && isFacingLeft) return;
        if (movementDirection.x > 0 && !isFacingLeft) return;

        isFacingLeft = !isFacingLeft;
        animatorFront.transform.localScale = new Vector3(animatorFront.transform.localScale.x * -1, animatorFront.transform.localScale.y, 1);
        animatorBack.transform.localScale = new Vector3(animatorBack.transform.localScale.x * -1, animatorBack.transform.localScale.y, 1);
    }
}