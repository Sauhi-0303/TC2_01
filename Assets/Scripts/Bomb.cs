using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Bomb : MonoBehaviour

{

    public GameObject bombPrefab; // 爆弾のプレハブ

    private Camera mainCamera;

    public BombType bombType; // 爆弾の種類

    private static Dictionary<BombType, int> bombCounts = new Dictionary<BombType, int>();


    void Start()

    {

        if (!bombCounts.ContainsKey(bombType))

        {

            bombCounts[bombType] = 0;

        }

        bombCounts[bombType]++;

        mainCamera = Camera.main;

    }

    // 爆弾の種類を列挙する

    void OnDestroy()

    {

        bombCounts[bombType]--;

    }

    // 爆弾の種類を列挙する

    public static int GetBombCount(BombType type)

    {

        return bombCounts.ContainsKey(type) ? bombCounts[type] : 0;

    }

    // 爆弾の爆発処理

    void OnTriggerEnter(Collider other)

    {

        Bomb otherBomb = other.GetComponent<Bomb>();

        if (otherBomb != null && otherBomb.bombType == bombType)

        {

            int count = GetBombCount(bombType);

            if (count >= 4)

            {

                Explode();

            }

        }

    }

    void SpawnBomb()

    {

        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10f; // カメラからの距離を設定

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        Instantiate(bombPrefab, worldPos, Quaternion.identity);

    }

    void KeepBombOffScreen()

    {

        Vector3 offScreenPos = new Vector3(-1000f, -1000f, 0f); // 画面外の座標

        Instantiate(bombPrefab, offScreenPos, Quaternion.identity);

    }

    void Explode()
    {
        // 半径2.0fの範囲内にあるオブジェクトを検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.0f);

        foreach (Collider nearbyObject in colliders)
        {
            Hako hako = nearbyObject.GetComponent<Hako>();
            if (hako != null)
            {
                hako.TakeDamage(100); // 箱に100ダメージ
            }
        }

        // エフェクトなどをここで追加することも可能

        Destroy(gameObject); // 爆発後に自身を破壊
    }

    void Update()

    {

        if (Input.GetMouseButtonDown(0)) // 左クリック

        {

            SpawnBomb();

        }

        else if (Input.GetMouseButtonDown(1)) // 右クリック

        {

            KeepBombOffScreen();

        }

    }

}