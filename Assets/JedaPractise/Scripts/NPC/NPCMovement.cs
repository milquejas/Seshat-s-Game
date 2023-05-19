using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform[] waypoints;
    public float stopDistance = 1f; // distance at which NPC stops to interact with player

    private int currentWaypoint = 0;
    private bool isInteracting = false; // is NPC currently interacting with the player?
    private bool isWaiting = false; // is NPC currently waiting at a waypoint?
    private GameObject npcCharacter;

    void Update()
    {
        if (waypoints.Length == 0 || isInteracting || isWaiting) return;

        // Move towards the waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);

        // If NPC is at the current waypoint, wait for a second and then move to the next one
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            isWaiting = true;
            StartCoroutine(WaitAtWaypoint(1f)); // wait for a second
        }

        // If the player is close, stop moving and start interacting
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector2.Distance(transform.position, player.transform.position) < stopDistance)
        {
            isInteracting = true;
            StartCoroutine(InteractWithPlayer());
        }
    }

    IEnumerator WaitAtWaypoint(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        isWaiting = false;
    }

    IEnumerator InteractWithPlayer()
    {
        // Wait a few seconds to simulate interaction
        yield return new WaitForSeconds(5);
        isInteracting = false;
    }
}
