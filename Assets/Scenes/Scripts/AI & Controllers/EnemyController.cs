using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Private-Public Variables
    [Header("Movement")]
    public float forwardSpeed;
    public float turnSpeed;
    Vector3 destination;
    public Transform playerDestination;
    private bool isGrounded = true;
    #endregion
    #region AI Movement
    void EnemyCarMovement()
    {
        if ((playerDestination.position - transform.position).magnitude < 10)
        {
            //spin
            transform.Rotate(transform.up, 10 * turnSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerDestination.rotation, Time.deltaTime * turnSpeed);
        }
    }
    #endregion
    #region MonoBehaviour
    void Update()
    {
        if (GameManager.Instance.Status == GameManager.GameStatus.Playing)
            return;
        Grounded();
        Fail();
        if (!isGrounded)
        {
            Falling();
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.Status == GameManager.GameStatus.Playing)
            return;
        if (isGrounded)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,transform.eulerAngles.z);
        }
        transform.position += transform.right * forwardSpeed * Time.deltaTime;
        EnemyCarMovement();
    }
    #endregion
    #region Methods
    void Grounded()
    {
        const float posY = 1.5f;
        if (transform.position.y > posY)
        {
            isGrounded = false;
        }
    }
    void Falling()
    {
        const float posY = 1.5f;
        Quaternion pos = new Quaternion(0, 0, 0, transform.rotation.w);
        if (transform.position.y < posY)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, pos, .1f);
            StartCoroutine(Fall());
        }
    }
    void Fail()
    {
        const float posY = .5f;
        if (transform.position.y < posY)
        {
            EnemyCarsManager.Instance.enemyCars.Remove(transform.parent.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(.3f);
        isGrounded = true;
    }
    #endregion
}
