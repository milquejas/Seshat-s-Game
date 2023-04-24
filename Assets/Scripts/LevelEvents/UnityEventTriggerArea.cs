using UnityEngine;
using UnityEngine.Events;

/*
 * Very generic editor friendly trigger area script
*/

public class UnityEventTriggerArea : MonoBehaviour
{
    public class TriggerColliderUnityEvent : MonoBehaviour
    {
        public UnityEvent OnTriggerEnter;
        public UnityEvent OnTriggerExit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnter.Invoke();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            OnTriggerExit.Invoke();
        }
    }
}
