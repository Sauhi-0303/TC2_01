using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotate;//
    // Start is called before the first frame update
    [SerializeField]
    private int itemCount = 5;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Transform appearPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);
    }
    //���̉����ɓ����������̏���
    //private void OnTriggerEnter(Collider other)
    //{
    //    //�v���C���[�ɏՓ˂����ꍇ
    //    Player player = other.GetComponent<Player>();
    //    if(player !=null)
    //    {
    //        //�R�C�����o������񐔂𐧌�
    //        if(itemCount>0)
    //        {
    //            itemCount--;
    //            //�R�C������
    //            GameObject coin = Instantiate(coinPrefab,appearPoint.position,Quaternion.identity);
    //            Destroy(coin.gameObject, 0.5f);//������A�w�莞�Ԍ�ɍ폜
    //            player.AddCoin(1);//�R�C�����l��
    //        }
    //    }
    //}
}
