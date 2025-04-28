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
    //箱の下側に当たった時の処理
    //private void OnTriggerEnter(Collider other)
    //{
    //    //プレイヤーに衝突した場合
    //    Player player = other.GetComponent<Player>();
    //    if(player !=null)
    //    {
    //        //コインが出現する回数を制限
    //        if(itemCount>0)
    //        {
    //            itemCount--;
    //            //コイン生成
    //            GameObject coin = Instantiate(coinPrefab,appearPoint.position,Quaternion.identity);
    //            Destroy(coin.gameObject, 0.5f);//生成後、指定時間後に削除
    //            player.AddCoin(1);//コインを獲得
    //        }
    //    }
    //}
}
