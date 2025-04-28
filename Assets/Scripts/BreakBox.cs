using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour
{
    [SerializeField]
    private float impulse = 4.0f;
    [SerializeField]
    private GameObject destroyBoxPrefab;
    private void OnTriggerEnter(Collider other)
    {
        //ƒvƒŒƒCƒ„[‚ÉÕ“Ë‚µ‚½ê‡ 
        Rigidbody cannonBall = other.GetComponent<Rigidbody>();
        if(cannonBall != null)
        {
            ////”j‰ó” ¶¬
            //GameObject destroyBox = Instantiate(destroyBoxPrefab, transform.position, transform.rotation);
            ////”j‰ó” ‚Ìq‚Ì”j•Ğ‘S‚Ä‚ğ—ñ‹“
            //foreach(Rigidbody body in destroyBox.GetComponentsInChildren<Rigidbody>())
            //{
            //    //”j•Ğ‚Éƒ‰ƒ“ƒ_ƒ€‚ÈÕŒ‚‚ğ‰Á‚¦‚é
            //    body.AddForce(
            //        Random.Range(-impulse, impulse),
            //        Random.Range(0.0f, impulse),
            //        Random.Range(-impulse, impulse),
            //        ForceMode. Impulse);
            //}
            ////w’èŠÔŒã‚É”j‰ó” ‚ğíœ
            //Destroy(destroyBox, 3.0f);
            //©•ª‚ğíœ
            Destroy(gameObject);
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
