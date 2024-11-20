using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class MoverPlayer : Mover
{
    [SerializeField] private float _sittingSize = 0.7f;
    [Header("Layers for regurgulation")]
    [SerializeField] private string _nameLayerPlatform = "Platform";

    private int _layerPlatform;

    private string _horizontalInput = "Horizontal";
    private string _jumpInput = "Jump";
    private string _animationMoveX = "MoveX";

    private Vector2 _directionMove;
    private float _defualtScaleY;

    public event Action Attacked;

    private void Start()
    {
        _defualtScaleY = transform.localScale.y;
        _layerPlatform = LayerMask.NameToLayer(_nameLayerPlatform);
    }

    private void FixedUpdate()
    {
        if (_checkGround.IsGround && _directionMove.x != 0)
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

        if (Input.GetButtonDown(_jumpInput) && _checkGround.IsGround)
            IsJump = true;

        if (Input.GetMouseButtonDown(0))
            Attacked.Invoke();

        Animator.SetFloat(_animationMoveX, Mathf.Abs(_directionMove.x));
    }

    private void SitDown()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!Physics2D.GetIgnoreLayerCollision(gameObject.layer, _layerPlatform))
                Physics2D.IgnoreLayerCollision(gameObject.layer, _layerPlatform, true);

            transform.localScale = new(transform.localScale.x, _sittingSize);
        }
        else
        {
            if (Physics2D.GetIgnoreLayerCollision(gameObject.layer, _layerPlatform))
                Physics2D.IgnoreLayerCollision(gameObject.layer, _layerPlatform, false);

            transform.localScale = new(transform.localScale.x, _defualtScaleY);
        }
    }

    private void TurnToSide()
    {
        if (Input.GetAxis(_horizontalInput) < 0)
            SpriteRenderer.flipX = true;
        else if (Input.GetAxis(_horizontalInput) > 0)
            SpriteRenderer.flipX = false;
    }
}
