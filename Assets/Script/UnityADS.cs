using UnityEngine.Advertisements;
using UnityEngine;

public class UnityADS : MonoBehaviour
{
    string gameId = "3575916";
    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}
