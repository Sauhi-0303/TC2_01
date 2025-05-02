using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float maxMoveSpeed = 5.0f;//最大移動速度
    private CharacterController controller;
    private float horizontalSpeed;//水平移動速度
    //[SerializeField]
    //private float maxFallSpeed = 20.0f;//最大落下速度
    [SerializeField]
    private float gravity = 60.0f;//重力
    private float verticalSpeed;//垂直移動速度
    //[SerializeField]
    //private float jumpSpeed = 20.0f;//ジャンプ速度
    [SerializeField]
    private float acceleration = 999.0f;//加速度
    [SerializeField]
    private float friction = 0.0f;//摩擦係数
    [SerializeField]
    private float airControl = 0.3f;//空中入力操作時の補正値
    private Animator animator;
    [SerializeField]
    private Text coinText;
    private int coinCount;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private int life = 5;
    [SerializeField]
    private float pushPower = 0.75f;//押す力
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombSpawnPoint; // 爆弾を落とす位置
    private int inventoryBombCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Spawn();
        //UpdateCoinText();
        UpdateLifeText();
    }

    // Update is called once per frame
    //毎フレーム更新処理
    void Update()
    {
        ////水平方向入力情報取得
        //float horizontal = Input.GetAxisRaw("Horizontal");
        ////移動処理
        //Vector3 move;
        //move.x = horizontal * maxMoveSpeed;
        //move.y = 0.0f;
        //move.z = 0.0f;
        //controller.Move(move * Time.deltaTime);
        //進行方向更新処理
        UpdateDirection();
        //重力処理
        //UpdateGravity();
        //ジャンプ処理
        //UpdateJump();
        //移動方向更新処理
        UpdateMovement();
    }
    //固定時間毎更新処理
    private void FixedUpdate()
    {
        //念のためZ軸位置が動かないように固定
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    //進行方向更新処理
    private void UpdateDirection()
    {
        //水平方向入力情報所得
        float horizontal = Input.GetAxisRaw("Horizontal");
        //加速処理
        float power = Mathf.Abs(horizontal);
        if (power > 0.1f)
        {
            //水平軸入力情報から進行方向を設定
            Vector3 direction = new Vector3(horizontal, 0, 0);
            //進行方向に向くように回転設定
            transform.rotation = Quaternion.LookRotation(direction);
            //加速量を計算
            float speed = horizontal * acceleration * Time.deltaTime;
            //空中に浮いている時は移動値を補正する
            //if (!controller.isGrounded)
            //{
            //    speed *= airControl;
            //}
            //加速処理
            horizontalSpeed += speed;
            //速度が一定以上なら制限する
            if (Mathf.Abs(horizontalSpeed) > maxMoveSpeed)
            {
                horizontalSpeed = Mathf.Sign(horizontalSpeed) * maxMoveSpeed;
            }
        }

        //減速処理
        else
        {
            if (Mathf.Abs(horizontalSpeed) > 0)
            {
                //減速量を計算
                float speed = Mathf.Sign(horizontalSpeed) * friction * Time.deltaTime;
                //空中に浮いている時は移動値を補正する
                if (!controller.isGrounded)
                {
                    speed *= airControl;
                }
                //減速処理
                horizontalSpeed -= speed;
                //速度が一定以下なら止める
                if (Mathf.Abs(horizontalSpeed) < friction * Time.deltaTime)
                {
                    horizontalSpeed = 0.0f;
                }
            }
        }
        if (controller.isGrounded)
        {
            animator.SetFloat("Speed", power);
        }
        ////水平移動速度
        //horizontalSpeed = horizontal * maxMoveSpeed;
    }
    //移動更新処理
    private void UpdateMovement()
    {
        //
        animator.SetBool("Ground", controller.isGrounded);
        //移動量を計算
        // Vector3 move = new Vector3(horizontalSpeed, 0, 0);
        Vector3 move = new Vector3(horizontalSpeed, verticalSpeed, 0);
        //移動処理
        controller.Move(move * Time.deltaTime);

        // 左クリック：爆弾を落とす
        if (Input.GetMouseButtonDown(0))
        {
            if (inventoryBombCount > 0)
            {
                DropBomb();
                inventoryBombCount--;
            }
        }

        // 右クリック：インベントリに爆弾を追加
        if (Input.GetMouseButtonDown(1))
        {
            KeepBomb();
        }
    }

    private void DropBomb()
    {
        Vector3 dropPosition = bombSpawnPoint != null ? bombSpawnPoint.position : transform.position + Vector3.down;
        Instantiate(bombPrefab, dropPosition, Quaternion.identity);
        Debug.Log("爆弾を落とした！");
    }

    private void KeepBomb()
    {
        inventoryBombCount++;
        Debug.Log("爆弾をインベントリに追加。現在の所持数：" + inventoryBombCount);
    }

    //重力処理
    //private void UpdateGravity()
    //{
    //    //地面に接地している時の垂直移動速度は一定の重力量
    //    if (controller.isGrounded)
    //    {
    //        verticalSpeed = -gravity * Time.deltaTime;
    //    }
    //    //空中では垂直移動速度に重力を加算していく
    //    else
    //    {
    //        verticalSpeed -= gravity * Time.deltaTime;
    //    }
    //    //垂直移動速度が最大落下速度を超えないように制限     
    //    verticalSpeed = Mathf.Max(verticalSpeed, -maxFallSpeed);
    //}
    //ジャンプ処理
    //private void UpdateJump()
    //{
    //    //地面に接地している状態でジャンプボタンを押すと垂直移動速度を設定する
    //    if (controller.isGrounded)
    //    {
    //        if (Input.GetButtonDown("Jump"))
    //        {
    //            verticalSpeed = jumpSpeed;
    //            //
    //            animator.SetTrigger("Jump");
    //        }
    //    }
    //}
    //private void UpdateCoinText()
    //{
    //    coinText.text = "x" + coinCount;
    //}
    //
    //public void AddCoin(int amount)
    //{
    //    coinCount += amount;
    //    //
    //    UpdateCoinText();
    //}

    private void Spawn()
    {
        //移動力をリセット
        verticalSpeed = 0.0f;
        horizontalSpeed = 0.0f;
        //キャラクターコントローラー仕様時に直接位置を変更する場合は
        //キャラクターコントローラーを無効化してから設定する必要がある
        //controller.enabled = false;
        //transform.position = spawnPoint.position;
        //controller.enabled = true;
        //Warp(spawnPoint.position);
    }

    //ワープ
    //public void Warp(Vector3 position)
    //{
    //    //キャラクターコントローラー仕様時に直接位置を変更する場合は
    //    //キャラクターコントローラーを無効化してから設定する必要がある
    //    controller.enabled = false;
    //    transform.position = position;
    //    controller.enabled = true;
    //}

    //死亡処理
    public void Death()
    {
        life--;
        if (life <= 0)
        {
            //
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //
        UpdateLifeText();
        //スポーン
        Spawn();
    }
    private void UpdateLifeText()
    {
        lifeText.text = "x" + life;
    }
    public void SetSpawnPoint(Transform transform)
    {
        spawnPoint = transform;
    }
    //
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //
        PushRigidbody(hit);
        //
        CeilingCheck(hit);
        //
        //CollisionDeathTrigger(hit);
    }
    //
    private void CeilingCheck(ControllerColliderHit hit)
    {
        //
        if (hit.normal.y < -0.8f)
        {
            //
            if (verticalSpeed > 0.0f)
            {
                verticalSpeed = 0.0f;
            }
        }
    }
    //
    private void PushRigidbody(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //
        if (body == null || body.isKinematic)
        {
            return;
        }
        //
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }
        //
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        Vector3 pushVelocity = pushDir * pushPower;
        pushVelocity.y = body.velocity.y;
        //
        body.velocity = pushVelocity;
    }
    //デストリガーとの衝突処理
    //private void CollisionDeathTrigger(ControllerColliderHit hit)
    //{
    //    //デストリガーと衝突したら死亡する
    //    DeathTrigger deathTrigger = hit.gameObject.GetComponent<DeathTrigger>();
    //    if(deathTrigger!=null)
    //    {
    //        Death();
    //    }
    //}
}
