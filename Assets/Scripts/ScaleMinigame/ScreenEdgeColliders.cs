// https://github.com/UnityCommunity/UnityLibrary/blob/master/Assets/Scripts/2D/Colliders/ScreenEdgeColliders.cs
// adds EdgeCollider2D colliders to screen edges
// only works with orthographic camera

using UnityEngine;

namespace UnityLibrary
{
    public class ScreenEdgeColliders : MonoBehaviour
    {
        private Camera cam;
        private EdgeCollider2D edge;
        private Vector2[] edgePoints;

        [SerializeField] private float sideExtraSpace;
        [SerializeField] private float topExtraSpace;
        [SerializeField] private float bottomLessSpace;

        void Awake()
        {
            if (Camera.main == null) Debug.LogError("Camera.main not found, failed to create edge colliders");
            else cam = Camera.main;

            if (!cam.orthographic) Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders");

            // add or use existing EdgeCollider2D
            edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

            edgePoints = new Vector2[5];

            AddCollider();
        }

        //Use this if you're okay with using the global fields and code in Awake() (more efficient)
        //You can just ignore/delete StandaloneAddCollider() if thats the case
        void AddCollider()
        {
            //Vector2's for the corners of the screen
            Vector2 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
            Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
            Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

            //Update Vector2 array for edge collider
            edgePoints[0] = bottomLeft + new Vector2(-sideExtraSpace, bottomLessSpace);
            edgePoints[1] = topLeft + new Vector2(-sideExtraSpace, topExtraSpace);
            edgePoints[2] = topRight + new Vector2(sideExtraSpace, topExtraSpace);
            edgePoints[3] = bottomRight + new Vector2(sideExtraSpace, bottomLessSpace);
            edgePoints[4] = bottomLeft + new Vector2(-sideExtraSpace, bottomLessSpace);

            edge.points = edgePoints;
        }
    }
}