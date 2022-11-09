using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static bool LevelStarted;
    
    [SerializeField] private List<GameObject> keys, rocks;

    public void ResetLevel()
    {
        ResetKeys();
        ResetTraps();
    }

    [UsedImplicitly] public void StartLevel() => LevelStarted = true;

    private void ResetKeys()
    {
        if (TriggerInteractions.NbKeys == 3) return;
        
        TriggerInteractions.NbKeys = 0;

        foreach (var key in keys)
        {
            key.SetActive(true);
        }
    }

    private void ResetTraps()
    {
        foreach (var rock in rocks)
        {
            Rigidbody rockRb = rock.GetComponent<Rigidbody>();
            rockRb.velocity = Vector3.zero;
            rockRb.useGravity = false;
            
            rock.transform.position = new Vector3(rock.transform.position.x, 20f, rock.transform.position.z);
        }
    }
}