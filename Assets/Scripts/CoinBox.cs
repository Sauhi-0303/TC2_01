using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinBox : MonoBehaviour
{
    [SerializeField]
    private int itemCount = 5;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Transform appearPoint;
    [SerializeField]
    private float pushPower = 1.0f;
    [SerializeField]
    private float gravity = 1.0f;
    [SerializeField]
    private Vector3 originalPosition;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float verticalSpeed;
    [SerializeField]
    private Material emptyBoxMaterial;

    // Start is called before the first frame update
    void Start()
    {
        position = originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            verticalSpeed -= gravity * Time.deltaTime;
            position.y += verticalSpeed;
            if (position.y < originalPosition.y)
            {
                Debug.Log("stop");
                position.y = originalPosition.y;
                isMoving = false;
            }
            transform.position = position;
        }
    }
    //箱の下側に当たった時の処理
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに衝突した場合
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            //コインが出現する回数を制限
            if (itemCount > 0)
            {
                itemCount--;
                if(itemCount==0)
                {
                    MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                    meshRenderer.material = emptyBoxMaterial;
                }
                //箱の縦揺れ処理開始
                verticalSpeed = pushPower;
                isMoving = true;
                //コイン生成
                //GameObject coin = Instantiate(coinPrefab, appearPoint.position, Quaternion.identity);
                //Destroy(coin.gameObject, 0.5f);//生成後、指定時間後に削除
                //player.AddCoin(1);//コインを獲得
                ////剛体コンポーネントを追加し、上方向に力を設定
                //Rigidbody body = coin.AddComponent<Rigidbody>();
                //body.velocity = new Vector3(0.0f, 6.0f, 0.0f);
            }
        }
    }
}
