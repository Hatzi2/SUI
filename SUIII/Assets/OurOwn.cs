using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;

public class OurOwn : MonoBehaviour
{
    XRHandSubsystem m_HandSubsystem;
    List<XRHand> m_Hands = new List<XRHand>();
    public Transform displayTransform; // Assign this in the inspector
    public float someThreshold = 0.1f; // Set this to a value that suits your needs

    void Start()
    {
        var handSubsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(handSubsystems);

        for (var i = 0; i < handSubsystems.Count; ++i)
        {
            var handSubsystem = handSubsystems[i];
            if (handSubsystem.running)
            {
                m_HandSubsystem = handSubsystem;
                break;
            }
        }
    }

    void OnUpdatedHands(List<XRHand> updatedHands,
        XRHandSubsystem.UpdateSuccessFlags updateSuccessFlags,
        XRHandSubsystem.UpdateType updateType)
    {
        foreach (var hand in updatedHands)
        {
            var thumbTip = hand.GetJoint(XRHandJointID.ThumbTip);
            var indexTip = hand.GetJoint(XRHandJointID.IndexTip);

            if (thumbTip.TryGetPose(out Pose thumbPose) && indexTip.TryGetPose(out Pose indexPose))
            {
                float distance = Vector3.Distance(thumbPose.position, indexPose.position);

                if (distance < someThreshold)
                {
                    // The thumb tip and index finger tip are in close proximity
                    Debug.Log("Thumb and index finger tips are close.");
                }
                else
                {
                    // The thumb tip and index finger tip are not in close proximity
                    Debug.Log("Thumb and index finger tips are not close.");
                }
            }
        }
    }
}