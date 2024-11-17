using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float JumpPower;

    [SerializeField] protected Rigidbody2D Rigidbody;
    [SerializeField] protected CapsuleCollider2D Collider;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    [SerializeField] protected CheckGround _checkGround;

    [SerializeField] private float _fallingGravityScale = 3f;

    protected bool IsJump = false;

    private float _defualtGravityScale;

    private void Awake()
    {
        _defualtGravityScale = Rigidbody.gravityScale;
    }

    protected void Move(Vector2 direction)
    {
        Vector2 velocity = Speed * direction;
        Rigidbody.velocity = velocity;
    }

    protected void Jump()
    {
        Vector2 velocity = new(Rigidbody.velocity.x, JumpPower);
        Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        IsJump = false;
    }

    protected void SetGravity()
    {
        if (!_checkGround.IsGround && Rigidbody.velocity.y < 0)
            Rigidbody.gravityScale = _fallingGravityScale;
        else if (_checkGround.IsGround || Rigidbody.velocity.y >= 0)
            Rigidbody.gravityScale = _defualtGravityScale;
    }

    protected void LimitVelocity() => Rigidbody.velocity = new(Rigidbody.velocity.x, Mathf.Clamp(Rigidbody.velocity.y, -JumpPower, JumpPower));
}
