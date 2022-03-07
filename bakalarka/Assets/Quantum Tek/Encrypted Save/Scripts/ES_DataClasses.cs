using UnityEngine;

namespace QuantumTek.EncryptedSave
{
    /// <summary> A class for storing Data about a class. </summary>
    [System.Serializable]
    public class ES_Data<T>
    {
        public T SaveData;
        public ES_Data() { }
        public ES_Data(T Data)
        { SaveData = Data; }
    }

    /// <summary> A class for storing Transform data. </summary>
    [System.Serializable]
    public class ES_TransformData
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;

        public ES_TransformData() { }
        public ES_TransformData(Transform Data)
        {
            localPosition = Data.localPosition;
            localRotation = Data.localRotation;
            localScale = Data.localScale;
        }
    }

    /// <summary> A class for storing RectTransform data. </summary>
    [System.Serializable]
    public class ES_RectTransformData
    {
        public Vector2 anchoredPosition;
        public Vector3 eulerAngles;
        public Vector2 sizeDelta;

        public ES_RectTransformData() { }
        public ES_RectTransformData(RectTransform Data)
        {
            anchoredPosition = Data.anchoredPosition;
            eulerAngles = Data.eulerAngles;
            sizeDelta = Data.sizeDelta;
        }
    }

    /// <summary> Used to load the transform's data in the background. </summary>
    [System.Serializable]
    public struct ES_TransformLoad
    {
        public Transform target;
        public ES_TransformData data;
    }

    /// <summary> Used to load the rect transform's data in the background. </summary>
    [System.Serializable]
    public struct ES_RectTransformLoad
    {
        public RectTransform target;
        public ES_RectTransformData data;
    }
}