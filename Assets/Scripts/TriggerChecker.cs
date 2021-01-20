using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
            Invoke(nameof(FallDown), 0.5f);
    }

    private void FallDown()
    {
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Destroy(transform.parent.gameObject, 2f);
    }
}
