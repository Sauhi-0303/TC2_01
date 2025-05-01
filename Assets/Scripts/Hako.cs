using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hako : MonoBehaviour
{
    public int durability = 1000;

    // 爆弾によるダメージを受ける関数
    public void TakeDamage(int damage)
    {
        durability -= damage;
        Debug.Log("箱の耐久値: " + durability);

        if (durability <= 0)
        {
            Destroy(gameObject);
            Debug.Log("箱が壊れました！");
        }
    }

    void Explode()
    {
        // 半径2.0f内のコライダーを取得
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider col in colliders)
        {
            Hako hako = col.GetComponent<Hako>();
            if (hako != null)
            {
                hako.TakeDamage(100); // 一度だけ100ダメージ
            }
        }

        // 爆発エフェクトや効果音をここに追加してもOK

        Destroy(gameObject); // 爆弾自身を消す
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
