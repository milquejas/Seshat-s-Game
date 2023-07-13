using UnityEngine;

public class BasicCameraFollow : MonoBehaviour 
{
    [SerializeField] private LevelSpawnSO spawnPoint;


	public Transform followTarget;
	private Vector3 targetPos;
	public float moveSpeed;
	
	void Start()
	{
		transform.position = spawnPoint.CurrentSpawnLocation - new Vector3(0,0, 10f);
    }

	void LateUpdate () 
	{
		if(followTarget != null)
		{
			targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
			Vector3 velocity = (targetPos - transform.position) * moveSpeed;
			transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime);
		}
	}
}

