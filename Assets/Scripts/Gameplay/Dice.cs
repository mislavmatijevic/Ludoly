using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody _diceRigidbody;
    private Vector3 _startingPosition;
    private Quaternion _startingRotation;
    private bool _isDiceRolling = false;

    private void Start()
    {
        _diceRigidbody = GetComponent<Rigidbody>();
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
    }

    public async Task<int> RollDice()
    {
        MoveToStartingPosition();

        _ = StartCoroutine(ThrowDice());
        while (_isDiceRolling)
        {
            await Task.Yield();
        }
        return GetNumberFacingUp();
    }

    private void MoveToStartingPosition()
    {
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;
    }

    private IEnumerator ThrowDice()
    {
        _isDiceRolling = true;
        _diceRigidbody.AddRelativeTorque(new Vector3(UnityEngine.Random.Range(500, 1000), UnityEngine.Random.Range(500, 1000), UnityEngine.Random.Range(500, 1000)));
        _diceRigidbody.AddForce((transform.forward * 750) + (transform.right * 750), ForceMode.Impulse);
        _diceRigidbody.velocity = Vector3.forward;
        while (_diceRigidbody.velocity.magnitude > 0.15f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        _isDiceRolling = false;
    }

    private int GetNumberFacingUp()
    {
        Vector3 direction = transform.InverseTransformDirection(Vector3.up);

        return direction.z >= 0.5
            ? 1
            : direction.z <= -0.5
            ? 6
            : direction.y >= 0.5 ? 2 : direction.y <= -0.5 ? 5 : direction.x >= 0.5 ? 4 : direction.x <= -0.5 ? 3 : -1;
    }
}
