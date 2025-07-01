using Photon.Pun;

using UnityEngine;

using UnityEngine.InputSystem;

public partial class PlayerManager : MonoBehaviourPun
{
    [SerializeField]

    private Rigidbody playerCharacterBody;

    private Vector3 playerMoveDirection;

    private Vector3 playerPushDirection;

    public Rigidbody PlayerRigBody { get { return playerCharacterBody; } }

    public Vector3 PlayerMoveDirection { get { return playerMoveDirection; } }

    [Header("이동 소리 관련")]

    [SerializeField]

    private AudioSource moveSoundSource;

    [SerializeField]

    private AudioClip moveSoundClip;

    [SerializeField]

    private bool isMoveSoundPlay = false;

    [Space]

    [SerializeField]

    private float rotateSpeed = 10f;

    public bool isGathering = false;

    public bool isStunning = false;

    public bool canMove = true;

    public void MoveStart()
    {
        moveSoundSource.clip = moveSoundClip;
    }

    public void MoveUpdate()
    {
        PlayerMoveBySpeed();

    }

    public void PlayerMoveBySpeed()
    {
        // 기준 중심 (원점 기준이면)
        Vector3 center = Vector3.zero;

        float radius = 20f;

        Vector3 offset = transform.position - center;

        if (offset.sqrMagnitude > radius * radius)
        {
            // 원 밖이면 반지름 거리로 끌어당김
            transform.position = center + offset.normalized * radius;

            playerSpellType.StopSpell();
        }

        if (canMove == true)
        {
            Vector2 headDirection = new Vector2(playerMoveDirection.x, playerMoveDirection.z);

            if (playerMoveDirection.magnitude > float.Epsilon)
            {
                isMoveSoundPlay = true;
            }

            else
            {
                isMoveSoundPlay = false;
            }

            GetRotate(headDirection);

            transform.position += PlayerStatus.moveSpeed * Time.deltaTime * playerMoveDirection;

            if (isMoveSoundPlay == true)
            {
                moveSoundSource?.Play();
            }

            else
            {
                moveSoundSource?.Stop();
            }
        }
    }

    public void SetMoveSoundPlayOn()
    {
        isMoveSoundPlay = true;
    }

    public void SetMoveSoundPlayOff()
    {
        isMoveSoundPlay = false;
    }

    public void PlayerMoveByPush(Vector3 pushVector)
    {
        if (pushVector.magnitude < float.Epsilon)
        {
            playerPushDirection = Vector3.zero;

            return;
        }

        playerPushDirection = pushVector;
    }

    public void GetRotate(Vector2 headDirection)
    {
        Vector3 headingVector3 = new Vector3(headDirection.x, 0f, headDirection.y);

        if (isGathering == true && targetObject != null)
        {
            Vector3 headDir = new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z);

            transform.LookAt(headDir);

            gatheringEffect.transform.LookAt(targetObject.transform);
        }

        else
        {
            if (headDirection.sqrMagnitude < 0.001f)
            {
                return;
            }

            Quaternion nextRotation = Quaternion.LookRotation(headingVector3);

            Quaternion firstRotation = Quaternion.LookRotation(new Vector3(transform.forward.x, 0f, transform.forward.z));

            transform.rotation = Quaternion.SlerpUnclamped(firstRotation, nextRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();

        playerMoveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
    }
}