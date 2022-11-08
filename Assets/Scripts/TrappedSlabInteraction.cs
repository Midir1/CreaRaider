using JetBrains.Annotations;
using UnityEngine;

public class TrappedSlabInteraction : MonoBehaviour
{
    [SerializeField] private Rigidbody rockRb;
    
    //Method Used For Animation Event
    [UsedImplicitly] public void EnableTrap() => rockRb.useGravity = true;
}