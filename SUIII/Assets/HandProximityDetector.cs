using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using System.Collections.Generic;

public class HandProximityDetector : MonoBehaviour
{
    private XRHandSubsystem handSubsystem;
    private List<XRNodeState> nodeStates = new List<XRNodeState>();

    void Start()
    {
        var handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);
        handSubsystem = GetHandSubsystem();

    }

    private XRHandSubsystem GetHandSubsystem()
    {
        List<XRHandSubsystem> handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetInstances(handSubsystems);
        foreach (var subsystem in handSubsystems)
        {
            if (subsystem.running)
            {
                return subsystem;
            }
        }
        return null;
    }

    private void trackingChanged(XRHandSubsystem subsystem)
{
    // Access updated hand data here
}

void Update()
{
    InputTracking.GetNodeStates(nodeStates);
    foreach (var nodeState in nodeStates)
    {
        XRNode nodeType = nodeState.nodeType;
        if (nodeType == XRNode.LeftHand || nodeType == XRNode.RightHand)
        {
            Debug.Log("Hands exist");
            Vector3 position;
            Quaternion rotation;
            if (nodeState.TryGetPosition(out position) && nodeState.TryGetRotation(out rotation))
            {
                    Debug.Log("Works");
            }
        }
    }
}
}