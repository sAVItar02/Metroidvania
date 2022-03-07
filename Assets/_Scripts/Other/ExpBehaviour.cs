using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBehaviour : MonoBehaviour
{
    public Transform target;
    Vector3 speed = Vector3.zero;
    [SerializeField] float minModifier = 7f;
    [SerializeField] float maxModifier = 11f;
    [SerializeField] float waitTime = 1f;

    private void Start()
    {
        waitTime = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        waitTime -= Time.deltaTime;
        if(waitTime< 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y + 0.5f, target.position.z), ref speed, Time.deltaTime * Random.Range(minModifier, maxModifier));        
        }
    }

    public void StartFollowing()
    {
        //
    }
}
