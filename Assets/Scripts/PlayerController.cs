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

    //Handle Animations
    private Animator _anime;

    private void Start() {
        _player = GetComponent<Rigidbody2D>();

        _anime = this.GetComponent<Animator>();
    }

    //Jumping, Dashing, Switching Weapons are Handled Seperately from FixedUpdate since it needs to be check constantly
    private void Update() {
        //Handle Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            _player.AddForce(transform.up * _jumpPower, ForceMode2D.Impulse);
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
        if (!IsGrounded()) {
            _anime.SetInteger("States", 2);
        }

        else if (_isDashing) {
            _anime.SetInteger("States", 3);
        }

        else if(_playerVelX > 0 || _playerVelX < 0) {
            _anime.SetInteger("States", 1);
        }

        else
            _anime.SetInteger("States", 0);

        _anime.SetBool("isDashingAnimation", _isDashing);
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
