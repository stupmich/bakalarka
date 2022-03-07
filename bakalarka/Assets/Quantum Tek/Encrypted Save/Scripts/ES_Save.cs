using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.EncryptedSave
{
    /// <summary> This gives the methods needed to save data. </summary>
    public class ES_Save : MonoBehaviour
    {
        private static List<ES_TransformLoad> transformLoads = new List<ES_TransformLoad>();
        private static List<ES_RectTransformLoad> rectTransformLoads = new List<ES_RectTransformLoad>();

        // Uses late update to change the position, rotation, and scale of the transforms and rect transforms as requested by the LoadTransform, LoadTransformWeb, LoadRectTransform, and LoadRectTransformWeb functions.
        private void LateUpdate()
        {
            for (int i = transformLoads.Count - 1; i >= 0; --i)
            {
                LoadBackgroundTransform(transformLoads[i]);
                transformLoads.RemoveAt(i);
            }
            for (int i = rectTransformLoads.Count - 1; i >= 0; --i)
            {
                LoadBackgroundRectTransform(rectTransformLoads[i]);
                rectTransformLoads.RemoveAt(i);
            }
        }

        /// <summary> Checks if a certain file has been saved already. </summary>
        /// <param name="path">The Path of the file, such as 'Albert'.</param>
        public static bool Exists(string path)
        {
            string dataPath = Application.persistentDataPath + path + ".save";
            return File.Exists(dataPath);
        }
        /// <summary> Checks if a certain key has been saved already. </summary>
        /// <param name="key">The key of the save, such as 'Albert'.</param>
        public static bool ExistsWeb(string key)
        { return PlayerPrefs.HasKey(key); }

        /// <summary> Deletes a save file if it exists. </summary>
        /// <param name="path">The Path of the file, such as 'Albert'.</param>
        /// <returns></returns>
        public static void DeleteData(string path)
        {
            string dataPath = Application.persistentDataPath + path + ".save";

            if (File.Exists(dataPath)) File.Delete(dataPath);
            else Debug.LogError("The given save file '" + path + "' does not exist.");
        }
        /// <summary> Deletes a save key if it exists. </summary>
        /// <param name="key">The key of the save, such as 'Albert'.</param>
        /// <returns></returns>
        public static void DeleteDataWeb(string key)
        {
            if (PlayerPrefs.HasKey(key)) PlayerPrefs.DeleteKey(key);
            else Debug.LogError("The given save key '" + key + "' does not exist.");
        }

        /// <summary> Saves Data into encrypted memory. </summary>
        /// <param name="data">The Data to save, such as 'Albert', or 0, 1.5, false, and so on.</param>
        /// <param name="path">The Path to save Data into, such as 'Player Data/Albert'.</param>
        public static void Save<T>(T data, string path)
        { ES_Encryption.Save(data, path); }

        /// <summary> Saves Data into encrypted memory. </summary>
        /// <param name="data">The Data to save, such as 'Albert', or 0, 1.5, false, and so on.</param>
        /// <param name="key">The key to save Data into, such as 'Albert'.</param>
        public static void SaveWeb<T>(T data, string key)
        { ES_Encryption.SaveWeb(data, key); }

        /// <summary> Returns a Data object from encrypted memory, if it exists. </summary>
        /// <param name="path">The Path to load Data from, such as 'Player Data/Albert'.</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        { return ES_Encryption.Load<T>(path); }

        /// <summary> Returns a Data object from encrypted memory, if it exists. </summary>
        /// <param name="key">The key to load Data from, such as 'Albert'.</param>
        /// <returns></returns>
        public static T LoadWeb<T>(string key)
        { return ES_Encryption.LoadWeb<T>(key); }

        /// <summary> Saves a transform into memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to save.</param>
        /// <param name="path">The path to save to.</param>
        public static void SaveTransform(Transform data, string path)
        {
            ES_TransformData tData = new ES_TransformData { localPosition = data.localPosition, localRotation = data.localRotation, localScale = data.localScale };
            Save(tData, path);
        }

        /// <summary> Saves a transform into memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to save.</param>
        /// <param name="key">The key to save to.</param>
        public static void SaveTransformWeb(Transform data, string key)
        {
            ES_TransformData tData = new ES_TransformData { localPosition = data.localPosition, localRotation = data.localRotation, localScale = data.localScale };
            SaveWeb(tData, key);
        }

        /// <summary> Loads a transform from memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to load data into.</param>
        /// <param name="path">The path to load from.</param>
        public static void LoadTransform(Transform data, string path)
        {
            ES_TransformData tData = Load<ES_TransformData>(path);
            transformLoads.Add(new ES_TransformLoad { target = data, data = tData });
        }

        /// <summary> Loads a transform from memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to load data into.</param>
        /// <param name="key">The key to load from.</param>
        public static void LoadTransformWeb(Transform data, string key)
        {
            ES_TransformData tData = LoadWeb<ES_TransformData>(key);
            transformLoads.Add(new ES_TransformLoad { target = data, data = tData });
        }

        private static void LoadBackgroundTransform(ES_TransformLoad data)
        {
            data.target.localPosition = data.data.localPosition;
            data.target.localRotation = data.data.localRotation;
            data.target.localScale = data.data.localScale;
        }

        /// <summary> Saves a rect transform into memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to save.</param>
        /// <param name="path">The path to save to.</param>
        public static void SaveRectTransform(RectTransform data, string path)
        {
            ES_RectTransformData tData = new ES_RectTransformData { anchoredPosition = data.anchoredPosition, eulerAngles = data.eulerAngles, sizeDelta = data.sizeDelta };
            Save(tData, path);
        }

        /// <summary> Saves a rect transform into memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to save.</param>
        /// <param name="key">The key to save to.</param>
        public static void SaveRectTransformWeb(RectTransform data, string key)
        {
            ES_RectTransformData tData = new ES_RectTransformData { anchoredPosition = data.anchoredPosition, eulerAngles = data.eulerAngles, sizeDelta = data.sizeDelta };
            SaveWeb(tData, key);
        }

        /// <summary> Loads a rect transform from memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to load data into.</param>
        /// <param name="path">The path to load from.</param>
        public static void LoadRectTransform(RectTransform data, string path)
        {
            ES_RectTransformData tData = Load<ES_RectTransformData>(path);
            rectTransformLoads.Add(new ES_RectTransformLoad { target = data, data = tData });
        }

        /// <summary> Loads a rect transform from memory as position, rotation, and scale. </summary>
        /// <param name="data">The transform to load data into.</param>
        /// <param name="key">The key to load from.</param>
        public static void LoadRectTransformWeb(RectTransform data, string key)
        {
            ES_RectTransformData tData = LoadWeb<ES_RectTransformData>(key);
            rectTransformLoads.Add(new ES_RectTransformLoad { target = data, data = tData });
        }

        private static void LoadBackgroundRectTransform(ES_RectTransformLoad data)
        {
            data.target.anchoredPosition = data.data.anchoredPosition;
            data.target.eulerAngles = data.data.eulerAngles;
            data.target.sizeDelta = data.data.sizeDelta;
        }
    }
}