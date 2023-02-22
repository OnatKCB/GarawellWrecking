using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WreckingBallController_Player : MonoBehaviour
{
    #region Variables
    public float hitPower;
    public Rigidbody rb;
    [SerializeField] TrailRenderer ballTrail;
    [SerializeField] LineRenderer ropeLine;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform bonusPos;
    [SerializeField] Transform carPos;
    #endregion
    #region MonoBehaviour
    private void Start()
    {
        ballTrail.emitting = false;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.Status != GameManager.GameStatus.Playing)
            return;
        TurnAround();
    }
    #endregion
    #region Methods
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyCar"))
        {
            float velocity = rb.velocity.magnitude;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * velocity * hitPower * 2);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * velocity * hitPower / 4);

        }
    }
    void TurnAround()
    {
        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();
        ballTrail.emitting = false;
        ropeLine.enabled = true;
        if (!transform.GetComponent<ConfigurableJoint>())
        {
            transform.AddComponent<ConfigurableJoint>().connectedBody = carPos.GetComponent<Rigidbody>();
            ConfigurableJoint newJoint = GetComponent<ConfigurableJoint>();
            newJoint.xMotion = ConfigurableJointMotion.Limited;
            newJoint.yMotion = ConfigurableJointMotion.Limited;
            newJoint.zMotion = ConfigurableJointMotion.Limited;
            newJoint.autoConfigureConnectedAnchor = false;
            newJoint.connectedAnchor = new Vector3(-1.76f, 0, 0);
            newJoint.anchor = new Vector3(0, 0, 0);
            SoftJointLimit limit = new SoftJointLimit();
            limit.limit = 1;
            newJoint.linearLimit = limit;
        }
    }        
    #endregion
}
