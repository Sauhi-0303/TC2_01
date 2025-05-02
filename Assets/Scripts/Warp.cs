using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField]
    private Transform outPoint;

    //
    private void OnTriggerEnter(Collider other)
    {
        //
        if (outPoint == null) return;
        //
        Player player = other.GetComponent<Player>();
        if(player!=null)
        {
            //
            //player.Warp(outPoint.position);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
