using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataAndNetwork;
using Google.Protobuf.Collections;
using UnityEngine;

public static class StaticFuncs
{
    public static Vector3 ProtoToUnity(UnityVec3 vec3)
    {
        Vector3 vector3 = new Vector3(vec3.X, vec3.Y, vec3.Z);
        return vector3;
    }

    public static UnityVec3 UnityToProto(Vector3 vector3)
    {
        UnityVec3 unityVec3 = new UnityVec3
        {
            X = vector3.x,
            Y = vector3.y,
            Z = vector3.z,
        };
        return unityVec3;
    }

    public static Quaternion ProtoToUnity(UnityQuater quater)
    {
        Quaternion quaternion = new Quaternion(quater.X, quater.Y, quater.Z, quater.W);
        return quaternion;
    }

    public static UnityQuater UnityToProto(Quaternion quaternion)
    {
        UnityQuater unityQuater = new UnityQuater
        {
            X = quaternion.x,
            Y = quaternion.y,
            Z = quaternion.z,
            W = quaternion.w,
        };
        return unityQuater;
    }

    public static void ProtoToUnity(UnityTrans unityTrans, Transform trans, bool includePos = true)
    {
        if (includePos)
        {
            trans.localPosition = ProtoToUnity(unityTrans.Pos);
        }

        trans.localRotation = ProtoToUnity(unityTrans.Rota);
    }

    public static UnityTrans UnityToProto(Transform trans, bool isWorld = false)
    {
        UnityTrans unityTrans = new UnityTrans
        {
            Pos = UnityToProto(trans.localPosition),
            Rota = UnityToProto(trans.localRotation),
        };

        if (isWorld)
        {
            unityTrans = new UnityTrans
            {
                Pos = UnityToProto(trans.position),
                Rota = UnityToProto(trans.rotation),
            };
        }
        
        return unityTrans;
    }

    public static void ProtoToUnity(BodyTrans bodyTrans, List<Transform> transforms)
    {
        if (bodyTrans.CalculateSize() <= 0)
        {
            return;
        }
        
        if (transforms.Count != 3)
        {
            Debug.Log("Length wrong");
            return;
        }

        ProtoToUnity(bodyTrans.Head, transforms[0]);
        ProtoToUnity(bodyTrans.LeftHand, transforms[1]);
        ProtoToUnity(bodyTrans.RightHand, transforms[2]);
    }

    public static void ProtoToUnity(BodyTrans bodyTrans, Transform transParent)
    {
        if (bodyTrans.CalculateSize() <= 0)
        {
            return;
        }
        
        List<Transform> Points = transParent.GetComponentsInChildren<Transform>().ToList();
        Points.RemoveAt(0);
        if (Points.Count != 3)
        {
            Debug.Log("Length wrong");
            return;
        }

        ProtoToUnity(bodyTrans.Head, Points[0]);
        ProtoToUnity(bodyTrans.LeftHand, Points[1]);
        ProtoToUnity(bodyTrans.RightHand, Points[2]);
    }

    public static BodyTrans UnityToProto(List<Transform> transforms)
    {
        if (transforms.Count != 5)
        {
            Debug.Log("Length wrong");
            return null;
        }

        BodyTrans bodyTrans = new BodyTrans
        {
            Head = UnityToProto(transforms[0], true),
            LeftHand = UnityToProto(transforms[1], true),
            RightHand = UnityToProto(transforms[2], true),

            LeftFingers = {UnityToProto(transforms[3].GetComponentsInChildren<Transform>())},
            RightFingers = {UnityToProto(transforms[4].GetComponentsInChildren<Transform>())}
        };

        return bodyTrans;
    }

    public static void ProtoToUnity(RepeatedField<UnityTrans> unityTranses, Transform trans)
    {
        Transform[] childs = trans.GetComponentsInChildren<Transform>();
        for (int i = 0; i < unityTranses.Count; i++)
        {
            ProtoToUnity(unityTranses[i], childs[i + 1], false);
        }
    }

    public static RepeatedField<UnityTrans> UnityToProto(Transform[] trans)
    {
        RepeatedField<UnityTrans> unityTranses = new RepeatedField<UnityTrans>();
        for (int i = 1; i < trans.Length; i++)
        {
            unityTranses.Add(UnityToProto(trans[i]));
        }

        return unityTranses;
    }
}