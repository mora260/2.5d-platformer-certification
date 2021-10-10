using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed, _gravity, _jumpHeight;
    private float _yVelocity = 0;
    private float _recentlyGrounded = 0;

    CharacterController _cc;

    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();

        if (_cc == null) {
            Debug.LogError("No character controller found on the player!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement() {
        if (_cc.isGrounded) {
            _yVelocity = 0;
            _recentlyGrounded = 0.3f;
        }

        float direction = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0, 0, direction) * _speed;

        if (_recentlyGrounded >= 0 && Input.GetButtonDown("Jump")) {
            _yVelocity = Mathf.Sqrt(_jumpHeight * -2.0f * _gravity);
        }

        ApplyGravity(ref movement);
        _cc.Move(movement * Time.deltaTime);

        if (_recentlyGrounded > 0) {
            _recentlyGrounded-=Time.deltaTime;
        }
    }

    private void ApplyGravity(ref Vector3 movement) {
        if (_cc.isGrounded == false) {
            _yVelocity+= _gravity * Time.deltaTime;
            movement.y=_yVelocity;
        }
    }
}
