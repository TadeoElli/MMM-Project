using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStrategy {
    void MoveForward(float speed, Transform transform);
    void Rotate(float rotationSpeed, float direction, Transform transform);
}
