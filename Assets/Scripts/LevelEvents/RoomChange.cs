using System.Collections;
using UnityEngine;

/*
 * 
 * Meant to be placed outside camera view?
 * Start player automove when entered? 
 * Disable teleport one after a teleport?
 * Player enters trigger area -> fade out -> teleport player and camera -> fade in
 * Call destination RoomChange class method to travel
 *
*/

[RequireComponent(typeof(Collider2D))]
public class RoomChange : MonoBehaviour
{
    [SerializeField] private Vector2 exitDirection;
    private Transform SpawnLocation;
    [SerializeField] private Transform CameraLocation;
    [SerializeField] private RoomChange Destination;

    private TouchMovementAndInteraction player;
    private IEnumerator fixedUpdateCoroutine;

    private Camera mainCamera;
    private Collider2D trigger;

    private void Start()
    {
        mainCamera = Camera.main;
        trigger = GetComponent<Collider2D>();

        // remember if prefab gets more children this needs to change...
        SpawnLocation = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TouchMovementAndInteraction _player))
        {
            player = _player;
            
            StartCoroutine(StartRoomTransition());
        }
    }

    public IEnumerator StartExit(TouchMovementAndInteraction _player)
    {
        player = _player;

        trigger.enabled = false;
        _player.transform.position = SpawnLocation.position;
        mainCamera.transform.position = CameraLocation.position;

        fixedUpdateCoroutine = AutoMovePlayer(exitDirection);
        StartCoroutine(fixedUpdateCoroutine);

        yield return new WaitForSeconds(0.8f);

        StopCoroutine(fixedUpdateCoroutine);
        trigger.enabled = true;
        player.ControlDisabled = false;
    }

    private IEnumerator StartRoomTransition()
    {
        player.ControlDisabled = true;

        fixedUpdateCoroutine = AutoMovePlayer(-exitDirection);
        StartCoroutine(fixedUpdateCoroutine);
        
        // fadeout?
        yield return new WaitForSeconds(0.8f);
        StopCoroutine(fixedUpdateCoroutine);

        StartCoroutine(Destination.StartExit(player));
    }
    
    private IEnumerator AutoMovePlayer(Vector2 moveDirection)
    {
        for ( ; ; ) // ;..;  ´,,`  (•,,•)
        {
            player.PlayerRigidbody.velocity = Vector2.ClampMagnitude(moveDirection, player.MaxMoveSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
}
