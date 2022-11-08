using JetBrains.Annotations;
using UnityEngine;

public class SlabDoorInteraction : MonoBehaviour
{
    [SerializeField] private Animator door;
    
    private static readonly int OnSlab = Animator.StringToHash("OnSlab");

    //Methods Used For Animations Events
    [UsedImplicitly] public void OpenDoor() => door.SetBool(OnSlab, true);

    [UsedImplicitly] public void CloseDoor() => door.SetBool(OnSlab, false);
}