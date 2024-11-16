using UnityEngine;
using UnityEngine.Events;

public class MoverPlayer : Mover
{
    [SerializeField] private float _sittingSize = 0.7f;

    private string _horizontalInput = "Horizontal";
    private string _jumpInput = "Jump";
    private string _animationMoveX = "MoveX";

    private Vector2 _directionMove;
    private float _defualtScaleY;

    public UnityEvent Attacked;

    private void Start()
    {
        _defualtScaleY = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        if (IsGround && _directionMove.x != 0)
            Move(_directionMove);

        if (IsJump)
            Jump();

        SetGravity();
        LimitVelocity();
    }

    private void Update()
    {
        SitDown();
        TurnToSide();

        _directionMove = new(Input.GetAxis(_horizontalInput), this.Rigidbody.velocity.y);

        if (Input.GetButtonDown(_jumpInput) && IsGround)
            IsJump = true;

        if (Input.GetMouseButtonDown(0))
            Attacked.Invoke();

        Animator.SetFloat(_animationMoveX, Mathf.Abs(_directionMove.x));
    }

    private void SitDown()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            transform.localScale = new(transform.localScale.x, _sittingSize);
        else
            transform.localScale = new(transform.localScale.x, _defualtScaleY);
    }

    private void TurnToSide()
    {
        if (Input.GetAxis(_horizontalInput) < 0)
            SpriteRenderer.flipX = true;
        else if (Input.GetAxis(_horizontalInput) > 0)
            SpriteRenderer.flipX = false;
    }
}
