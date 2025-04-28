using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        transform.Rotate(0.0f, 180 * Time.deltaTime, 0.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        ////
        //Player player = other.GetComponent<Player>();
        //if(player!=null)
        //{
        //    //
        //    player.AddCoin(1);
        //    //
        //    Destroy(gameObject);
        //}
    }
}
