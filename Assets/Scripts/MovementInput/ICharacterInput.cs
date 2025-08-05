using UnityEngine;
using UnityEngine.InputSystem;

public interface ICharacterInput
{
    float GetHorizontalInput();
    float GetVerticalInput();
    bool GetHInput();
    bool GetSpaceInput();
    bool GetKInput();
    Vector2 GetMoveJoyStick(InputActionReference _input);
    bool GetAttackButton(InputActionReference _attackButton);
    bool GetTakeButton(InputActionReference _takeButton);
}
