using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallImpact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CinemachineShake.Instance.ShakeCamera(5f, 0.2f);
        Destroy(gameObject, 0.75f);        
    }

}
