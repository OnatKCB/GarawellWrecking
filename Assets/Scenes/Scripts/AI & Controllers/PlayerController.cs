using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header ("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    private float startMousePosition;
    private float lastMousePosition;
    private bool isGrounded = true;
    #endregion
    #region Monobehaviour
    void Update()
    {
        if (GameManager.Instance.Status == GameManager.GameStatus.Playing)
            return;
        SwipeMovement();
        Grounded();
        Fail();
        if(!isGrounded)
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
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y);
        }
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }
    #endregion
    #region Movement
    void SwipeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            lastMousePosition = startMousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            lastMousePosition = Input.mousePosition.x;
            if (lastMousePosition > startMousePosition)
            {
                transform.Rotate(transform.up, 10 * turnSpeed * Time.deltaTime);
            }
            else if (lastMousePosition < startMousePosition)
            {
                transform.Rotate(-transform.up, 10 * turnSpeed * Time.deltaTime);
            }
        }
    }
    #endregion
    #region GroundCheck
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
            Debug.LogError("Fail");
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
