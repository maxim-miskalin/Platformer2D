using System;
using UnityEngine;

public class MoverPlayer : Mover
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _sittingSize = 0.7f;
    [Header("Layers for regurgulation")]
    [SerializeField] private string _nameLayerPlatform = "Platform";

    private int _layerPlatform;

    private string _animationMoveX = "MoveX";

    private Vector2 _directionMove;
    private float _defualtScaleY;

    private void OnEnable()
    {
        _input.Moved += UpdateMove;
        _input.Jumped += HandleJump;
        _input.DownSquatted += SitDown;
    }

    private void OnDisable()
    {
        _input.Moved -= UpdateMove;
        _input.Jumped -= HandleJump;
        _input.DownSquatted -= SitDown;
    }

    private void Start()
    {
        _defualtScaleY = transform.localScale.y;
        _layerPlatform = LayerMask.NameToLayer(_nameLayerPlatform);
    }

    private void FixedUpdate()
    {
        if (_groundCheck.IsGround && _directionMove.x != 0)
            Move(_directionMove);

        SetGravity();
        LimitVelocity();
    }

    private void Update()
    {
        TurnToSide();

        Animator.SetFloat(_animationMoveX, Mathf.Abs(_directionMove.x));
    }

    private void UpdateMove(float valueMovement)
    {
        _directionMove = new(valueMovement, this.Rigidbody.velocity.y);
    }

    private void HandleJump()
    {
        if (_groundCheck.IsGround)
           Jump();
    }

    private void SitDown(bool isActive)
    {
        if (isActive)
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
        if (_directionMove.x < 0)
            SpriteRenderer.flipX = true;
        else if (_directionMove.x > 0)
            SpriteRenderer.flipX = false;
    }
}
