using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, ICharacterInput
{
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }
    
    public bool GetHInput()
    {
        return Input.GetKeyDown(KeyCode.H);
    }

    public bool GetSpaceInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetKInput()
    {
        return Input.GetKeyDown(KeyCode.K);
    }

    public Vector2 GetMoveJoyStick(InputActionReference _input)
    {
        return _input.action.ReadValue<Vector2>();
    }

    public bool GetAttackButton(InputActionReference _attackButton)
    {
        return _attackButton.action.triggered;
    }

    public bool GetTakeButton(InputActionReference _takeButton)
    {
        return _takeButton.action.triggered;
    }
}
