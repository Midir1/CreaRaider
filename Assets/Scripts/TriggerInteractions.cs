using System;
using UnityEngine;

public class TriggerInteractions : MonoBehaviour
{
    public static int NbKeys;
    
    private const string KeyTag = "Key", SlabTag = "Slab";
    private const string OnSlabAnim = "OnSlab", OffSlabAnim = "OffSlab";

    private void OnTriggerEnter(Collider other)
    {
        KeyInteraction(other);
        SlabInteraction(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        SlabInteraction(other, false);
    }

    private static void KeyInteraction(Collider other)
    {
        if (!other.CompareTag(KeyTag)) return;
        
        NbKeys++;
        other.gameObject.SetActive(false);
    }

    private static void SlabInteraction(Collider other, bool onSlab)
    {
        if (!other.CompareTag(SlabTag)) return;

        other.GetComponentInParent<Animator>().Play(onSlab ? OnSlabAnim : OffSlabAnim);
    }
}