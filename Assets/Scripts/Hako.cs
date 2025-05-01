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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
