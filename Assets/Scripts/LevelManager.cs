using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> keys, rocks;

    public void ResetLevel()
    {
        ResetKeys();
        ResetTraps();
    }

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