using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;
    public Transform grabbedObject;
    private Rigidbody objectRb;

    public float grabDistance;
    [SerializeField] private float grabForce;
    [SerializeField] private float releaseForce;

    private void Update()
    {
        Grab();
    }

    private void Grab()
    {
        if (!InputManager.Instance.CheckIfPlayerIsGrabbing())
        {
            ReleaseObject();
            return;
        }

        Ray ray = InputManager.Instance.GetCrosshairPoint();
        RaycastHit hit;

        if (CheckIfPlayerGrabbedAnObject(ray, out hit))
        {
            grabbedObject = hit.transform;
            grabbedObject.parent = objectHolder;

            objectRb = grabbedObject.GetComponent<Rigidbody>();
            objectRb.isKinematic = true;
        }

        if (grabbedObject != null)
        {
            grabbedObject.position = Vector3.MoveTowards(grabbedObject.position, objectHolder.position, grabForce * Time.deltaTime);
        }
    }

    public bool CheckIfPlayerGrabbedAnObject(Ray ray, out RaycastHit hit)
    {
        if (Physics.Raycast(ray, out hit)
            && hit.transform.CompareTag("Object")
            && Vector3.Distance(transform.position, hit.point) <= grabDistance
            && grabbedObject == null)
        {
            return true;
        }
        
        return false;
    }

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.parent = null;

            Vector3 releaseDirection = InputManager.Instance.GetCrosshairPoint().direction;

            objectRb.isKinematic = false;
            objectRb.AddForce(releaseDirection * releaseForce, ForceMode.Impulse);

            grabbedObject = null;
        }
    }
}
