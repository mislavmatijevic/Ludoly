using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody diceRigidbody;
    private bool isDiceRolling = false;

    private void Start()
    {
        diceRigidbody = GetComponent<Rigidbody>();
    }

    public async Task<int> RollDice()
    {
        StartCoroutine(ThrowDice());
        while (isDiceRolling)
        {
            await Task.Yield();
        }
        return GetNumberFacingUp();
    }

    private IEnumerator ThrowDice()
    {
        isDiceRolling = true;
        diceRigidbody.AddRelativeTorque(new Vector3(UnityEngine.Random.Range(500, 1000), UnityEngine.Random.Range(500, 1000), UnityEngine.Random.Range(500, 1000)));
        diceRigidbody.AddForce(transform.forward * 750 + transform.right * 750, ForceMode.Impulse);
        diceRigidbody.velocity = Vector3.forward;
        while (diceRigidbody.velocity.magnitude > 0.15f)
        {
            yield return new WaitForSeconds(0.1f);
        }
        isDiceRolling = false;
    }

    private int GetNumberFacingUp()
    {
        Vector3 direction = transform.InverseTransformDirection(Vector3.up);

        if (direction.z >= 0.5)
        {
            return 1;
        }
        if (direction.z <= -0.5)
        {
            return 6;
        }
        if (direction.y >= 0.5)
        {
            return 2;
        }
        if (direction.y <= -0.5)
        {
            return 5;
        }
        if (direction.x >= 0.5)
        {
            return 4;
        }
        if (direction.x <= -0.5)
        {
            return 3;
        }

        return -1;
    }
}
