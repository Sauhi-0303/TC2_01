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
        //�v���C���[�ɏՓ˂����ꍇ 
        Rigidbody cannonBall = other.GetComponent<Rigidbody>();
        if(cannonBall != null)
        {
            ////�j�󔠐���
            //GameObject destroyBox = Instantiate(destroyBoxPrefab, transform.position, transform.rotation);
            ////�j�󔠂̎q�̔j�БS�Ă��
            //foreach(Rigidbody body in destroyBox.GetComponentsInChildren<Rigidbody>())
            //{
            //    //�j�ЂɃ����_���ȏՌ���������
            //    body.AddForce(
            //        Random.Range(-impulse, impulse),
            //        Random.Range(0.0f, impulse),
            //        Random.Range(-impulse, impulse),
            //        ForceMode. Impulse);
            //}
            ////�w�莞�Ԍ�ɔj�󔠂��폜
            //Destroy(destroyBox, 3.0f);
            //�������폜
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
