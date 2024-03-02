using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStrategy {
    void MoveForward(float speed, Rigidbody2D rigidbody2D);
    void Rotate(float rotationSpeed, float direction, Rigidbody2D rigidbody2D);
}
