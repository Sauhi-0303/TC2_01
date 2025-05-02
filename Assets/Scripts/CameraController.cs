using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private float smoothness = 0.1f;]

    [SerializeField]
    private Vector3 targetPosition;

    // �J�����ʒu�̃v���Z�b�g
    private Vector3 titlePos = new Vector3(1280f + 500f, 0f, 0f);
    private Vector3 descriptionPos = new Vector3(0f, 0f, 10f);
    private Vector3 gamePos = new Vector3(0f, 0f, 0f);
    private Vector3 resultPos = new Vector3(-500f, 0f, 0f);

    [SerializeField] private Renderer titleRenderer;
    [SerializeField] private Renderer descriptionRenderer;
    [SerializeField] private Renderer gameRenderer;
    [SerializeField] private Renderer resultRenderer;

    private enum CameraState
    {
        Title,
        Description,
        Game,
        Result
    }

    private CameraState currentState = CameraState.Title;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = titlePos;

        // �eRenderer�Ƀ}�e���A����ݒ�
        titleRenderer.material = titleMaterial;
        descriptionRenderer.material = descriptionMaterial;
        gameRenderer.material = gameMaterial;
        resultRenderer.material = resultMaterial;
    }

    // �J�����̏�Ԃɉ�����Renderer��؂�ւ���
    void SetActiveRenderer(Renderer active)
    {
        titleRenderer.enabled = (active == titleRenderer);
        descriptionRenderer.enabled = (active == descriptionRenderer);
        gameRenderer.enabled = (active == gameRenderer);
        resultRenderer.enabled = (active == resultRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothness);
            
        }

        // �J�����̈ړ�����
        //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
        
        // ���͂ɂ��J�����؂�ւ�
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if (currentState == CameraState.Title)
        //    {
        //        currentState = CameraState.Description;
        //        targetPosition = descriptionPos;
        //    }
        //    else if (currentState == CameraState.Game)
        //    {
        //        currentState = CameraState.Description;
        //        targetPosition = descriptionPos;
        //    }
        //}
        //
        //if (Input.GetMouseButtonDown(0)) // ���N���b�N
        //{
        //    if (currentState == CameraState.Title)
        //    {
        //        currentState = CameraState.Game;
        //        targetPosition = gamePos;
        //    }
        //}
        //
        // �v���C����Hako�̑ϋv���[���ɂȂ�����Result�Ɉڍs
        if (currentState == CameraState.Game && Hako.IsAllHakoDestroyed())
        {
            currentState = CameraState.Result;
            targetPosition = resultPos;
        }
    }
}