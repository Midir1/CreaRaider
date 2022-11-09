using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerInteractions : MonoBehaviour
{
    public static int NbKeys;

    private Transform _spawnPoint;

    private CharacterController _character;
    
    private const string KeyTag = "Key", SlabTag = "Slab", TrappedSlabTag = "TrappedSlab", DeathTag = "Death";
    private const string OnSlabAnim = "OnSlab", OffSlabAnim = "OffSlab";
    private const string OnTrappedSlabAnim = "OnTrappedSlab", OffTrappedSlabAnim = "OffTrappedSlab";
    private const string SpawnPoint = "SpawnPoint";
    private const string FinishTag = "Finish";

    private void Start() => _character = GetComponent<CharacterController>();

    private void OnTriggerEnter(Collider other)
    {
        KeyInteraction(other);
        SlabInteraction(other, true);
        SpawnPointInteraction(other);
        
        DeathTrigger(other);
        RestartLevel(other);
    }

    private void OnTriggerExit(Collider other) => SlabInteraction(other, false);

    private static void KeyInteraction(Component other)
    {
        if (!other.CompareTag(KeyTag)) return;
        
        NbKeys++;
        other.gameObject.SetActive(false);
    }

    private static void SlabInteraction(Component other, bool onSlab)
    {
        if (other.CompareTag(SlabTag)) 
            other.GetComponentInParent<Animator>().Play(onSlab ? OnSlabAnim : OffSlabAnim);
        else if (other.CompareTag(TrappedSlabTag)) 
            other.GetComponentInParent<Animator>().Play(onSlab ? OnTrappedSlabAnim : OffTrappedSlabAnim);
    }

    private void SpawnPointInteraction(Component other)
    {
        if (!other.CompareTag(SpawnPoint)) return;

        other.gameObject.SetActive(false);
        _spawnPoint = other.transform;

        HelpManager.ZoneIndex++;
    }

    private void DeathTrigger(Component other)
    {
        if (other.CompareTag(DeathTag)) _character.RespawnPlayer(_spawnPoint);
    }

    private void RestartLevel(Component other)
    {
        if (other.CompareTag(FinishTag))
        {
            LevelManager.LevelStarted = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}