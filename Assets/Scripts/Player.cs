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
    private float maxMoveSpeed = 5.0f;//�ő�ړ����x
    private CharacterController controller;
    private float horizontalSpeed;//�����ړ����x
    //[SerializeField]
    //private float maxFallSpeed = 20.0f;//�ő嗎�����x
    [SerializeField]
    private float gravity = 60.0f;//�d��
    private float verticalSpeed;//�����ړ����x
    //[SerializeField]
    //private float jumpSpeed = 20.0f;//�W�����v���x
    [SerializeField]
    private float acceleration = 999.0f;//�����x
    [SerializeField]
    private float friction = 0.0f;//���C�W��
    [SerializeField]
    private float airControl = 0.3f;//�󒆓��͑��쎞�̕␳�l
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
    private float pushPower = 0.75f;//������
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform bombSpawnPoint; // ���e�𗎂Ƃ��ʒu
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
    //���t���[���X�V����
    void Update()
    {
        ////�����������͏��擾
        //float horizontal = Input.GetAxisRaw("Horizontal");
        ////�ړ�����
        //Vector3 move;
        //move.x = horizontal * maxMoveSpeed;
        //move.y = 0.0f;
        //move.z = 0.0f;
        //controller.Move(move * Time.deltaTime);
        //�i�s�����X�V����
        UpdateDirection();
        //�d�͏���
        //UpdateGravity();
        //�W�����v����
        //UpdateJump();
        //�ړ������X�V����
        UpdateMovement();
    }
    //�Œ莞�Ԗ��X�V����
    private void FixedUpdate()
    {
        //�O�̂���Z���ʒu�������Ȃ��悤�ɌŒ�
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    //�i�s�����X�V����
    private void UpdateDirection()
    {
        //�����������͏�񏊓�
        float horizontal = Input.GetAxisRaw("Horizontal");
        //��������
        float power = Mathf.Abs(horizontal);
        if (power > 0.1f)
        {
            //���������͏�񂩂�i�s������ݒ�
            Vector3 direction = new Vector3(horizontal, 0, 0);
            //�i�s�����Ɍ����悤�ɉ�]�ݒ�
            transform.rotation = Quaternion.LookRotation(direction);
            //�����ʂ��v�Z
            float speed = horizontal * acceleration * Time.deltaTime;
            //�󒆂ɕ����Ă��鎞�͈ړ��l��␳����
            //if (!controller.isGrounded)
            //{
            //    speed *= airControl;
            //}
            //��������
            horizontalSpeed += speed;
            //���x�����ȏ�Ȃ琧������
            if (Mathf.Abs(horizontalSpeed) > maxMoveSpeed)
            {
                horizontalSpeed = Mathf.Sign(horizontalSpeed) * maxMoveSpeed;
            }
        }

        //��������
        else
        {
            if (Mathf.Abs(horizontalSpeed) > 0)
            {
                //�����ʂ��v�Z
                float speed = Mathf.Sign(horizontalSpeed) * friction * Time.deltaTime;
                //�󒆂ɕ����Ă��鎞�͈ړ��l��␳����
                if (!controller.isGrounded)
                {
                    speed *= airControl;
                }
                //��������
                horizontalSpeed -= speed;
                //���x�����ȉ��Ȃ�~�߂�
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
        ////�����ړ����x
        //horizontalSpeed = horizontal * maxMoveSpeed;
    }
    //�ړ��X�V����
    private void UpdateMovement()
    {
        //
        animator.SetBool("Ground", controller.isGrounded);
        //�ړ��ʂ��v�Z
        // Vector3 move = new Vector3(horizontalSpeed, 0, 0);
        Vector3 move = new Vector3(horizontalSpeed, verticalSpeed, 0);
        //�ړ�����
        controller.Move(move * Time.deltaTime);

        // ���N���b�N�F���e�𗎂Ƃ�
        if (Input.GetMouseButtonDown(0))
        {
            if (inventoryBombCount > 0)
            {
                DropBomb();
                inventoryBombCount--;
            }
        }

        // �E�N���b�N�F�C���x���g���ɔ��e��ǉ�
        if (Input.GetMouseButtonDown(1))
        {
            KeepBomb();
        }
    }

    private void DropBomb()
    {
        Vector3 dropPosition = bombSpawnPoint != null ? bombSpawnPoint.position : transform.position + Vector3.down;
        Instantiate(bombPrefab, dropPosition, Quaternion.identity);
        Debug.Log("���e�𗎂Ƃ����I");
    }

    private void KeepBomb()
    {
        inventoryBombCount++;
        Debug.Log("���e���C���x���g���ɒǉ��B���݂̏������F" + inventoryBombCount);
    }

    //�d�͏���
    //private void UpdateGravity()
    //{
    //    //�n�ʂɐڒn���Ă��鎞�̐����ړ����x�͈��̏d�͗�
    //    if (controller.isGrounded)
    //    {
    //        verticalSpeed = -gravity * Time.deltaTime;
    //    }
    //    //�󒆂ł͐����ړ����x�ɏd�͂����Z���Ă���
    //    else
    //    {
    //        verticalSpeed -= gravity * Time.deltaTime;
    //    }
    //    //�����ړ����x���ő嗎�����x�𒴂��Ȃ��悤�ɐ���     
    //    verticalSpeed = Mathf.Max(verticalSpeed, -maxFallSpeed);
    //}
    //�W�����v����
    //private void UpdateJump()
    //{
    //    //�n�ʂɐڒn���Ă����ԂŃW�����v�{�^���������Ɛ����ړ����x��ݒ肷��
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
        //�ړ��͂����Z�b�g
        verticalSpeed = 0.0f;
        horizontalSpeed = 0.0f;
        //�L�����N�^�[�R���g���[���[�d�l���ɒ��ڈʒu��ύX����ꍇ��
        //�L�����N�^�[�R���g���[���[�𖳌������Ă���ݒ肷��K�v������
        //controller.enabled = false;
        //transform.position = spawnPoint.position;
        //controller.enabled = true;
        //Warp(spawnPoint.position);
    }

    //���[�v
    //public void Warp(Vector3 position)
    //{
    //    //�L�����N�^�[�R���g���[���[�d�l���ɒ��ڈʒu��ύX����ꍇ��
    //    //�L�����N�^�[�R���g���[���[�𖳌������Ă���ݒ肷��K�v������
    //    controller.enabled = false;
    //    transform.position = position;
    //    controller.enabled = true;
    //}

    //���S����
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
        //�X�|�[��
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
    //�f�X�g���K�[�Ƃ̏Փˏ���
    //private void CollisionDeathTrigger(ControllerColliderHit hit)
    //{
    //    //�f�X�g���K�[�ƏՓ˂����玀�S����
    //    DeathTrigger deathTrigger = hit.gameObject.GetComponent<DeathTrigger>();
    //    if(deathTrigger!=null)
    //    {
    //        Death();
    //    }
    //}
}
