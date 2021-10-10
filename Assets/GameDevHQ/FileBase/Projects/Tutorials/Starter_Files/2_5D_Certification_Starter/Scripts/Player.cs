using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _speed, _gravity;
    float _yVelocity;

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
        }

        float direction = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0, 0, direction) * _speed;
        ApplyGravity(ref movement);
        _cc.Move(movement * Time.deltaTime);
    }

    private void ApplyGravity(ref Vector3 movement) {
        if (_cc.isGrounded == false) {
            _yVelocity+= _gravity * Time.deltaTime;
            movement.y=_yVelocity;
        }
    }
}
