using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForceReceivable
{
    void ReceiveForce(GameObject source, float force);
}
