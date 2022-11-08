using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private float raycastMaxDistance;

    [SerializeField] private GameObject interactText;
    
    private const int LayerMask = 1 << 7; //Door Layer
    private const int DefaultLayer = 0; //Remove Layer
    
    private const string InteractInput = "Interact";
    private const string DoorAnimation = "DoorOpen";
    private const string KeyDoorTag = "KeyDoor";
    
    private static readonly int OnSlab = Animator.StringToHash("OnSlab");
    
    private void FixedUpdate() => CheckDoorInRange();

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckDoorInRange()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastMaxDistance, LayerMask))
        {
            if (hit.transform.CompareTag(KeyDoorTag))
            {
                if(TriggerInteractions.NbKeys == 3) OpenDoor(hit);
            }
            else
            {
                interactText.SetActive(true);
            
                if (Input.GetAxisRaw(InteractInput) != 0f) OpenDoor(hit);
            }
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    private static void OpenDoor(RaycastHit hit)
    {
        hit.transform.gameObject.layer = DefaultLayer;

        Animator animator = hit.transform.GetComponentInParent<Animator>();
        animator.Play(DoorAnimation);
        animator.SetBool(OnSlab, true);
    }
}