using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _player;

    //Booleans
    private bool _isDashing;

    private bool _canDash = true;

    //Player Config
    [Header("Player Config")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    //Dashing Stuff
    [Header("Dashing Config")]
    [SerializeField] private float _dashingDuration;
    [SerializeField] private float _dashingRange;
    [SerializeField] private float _dashingDelay;

    [Header("Misc")]
    [SerializeField] private LayerMask _groundLayer;

    //Animation Stuff
    private Animator _anim;

    private void Start() {
        _player = GetComponent<Rigidbody2D>();

        _anim = GetComponent<Animator>();
    }

    //Jumping, Dashing, Switching Weapons are Handled Seperately from FixedUpdate since it needs to be check constantly
    private void Update() {
        //Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            _player.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);

            PlayerManager.instance.ReduceMana(3f);
        }

        //Handle Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash) {
            var _playerVelX = Input.GetAxisRaw("Horizontal");

            if (_playerVelX > 0f) {
                StartCoroutine(Dash(_playerVelX));
            }

            else if (_playerVelX < 0f) {
                StartCoroutine(Dash(_playerVelX));
            }

            PlayerManager.instance.ReduceMana(5f);
        }


    }

    //Handle the Physics
    private void FixedUpdate() {
        var _playerVelX = Input.GetAxisRaw("Horizontal");

        //If not dashing then move to normal
        if (!_isDashing) {
            //Moving
            _player.velocity = new Vector2(_playerVelX * _speed, _player.velocity.y);
        }

        //Handle Flipping
        if (_playerVelX < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (_playerVelX > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else {
            transform.eulerAngles = gameObject.transform.eulerAngles;
        }

        //Handle Animation
        if(_playerVelX != 0) {
            _anim.SetInteger("PlayerState", 1);
        }

        else _anim.SetInteger("PlayerState", 0);
    }

    //Coroutine for dashing
    private IEnumerator Dash(float dir) {
        _canDash = false;

        _isDashing = true;
        _player.gravityScale = 0;
        _player.velocity = new Vector2(_player.velocity.x, 0f);

        _player.AddForce(new Vector2(_dashingRange * dir, 0f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(_dashingDuration);

        _isDashing = false;
        _player.gravityScale = 1.4f;
        _player.velocity = Vector2.zero;

        yield return new WaitForSeconds(_dashingDelay);

        _canDash = true;
    }

    //Check grounded, cast raycast downwards, ignoring the player. If there's smt = grounded, else !grounded.
    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundLayer);

        if (hit.collider == null)
            return false;

        else {
            return true;
        }
    }

}
