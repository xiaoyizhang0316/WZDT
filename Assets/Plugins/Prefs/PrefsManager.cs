using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed class PrefsManager
{
    #region Inst

    private static readonly PrefsManager _inst = new PrefsManager();
    public static PrefsManager inst
    {
        get { return _inst; }
    }

    static PrefsManager()
    {
    }

    private PrefsManager()
    {
    }


    #endregion

    public bool hasInit { get; private set; }

    private const string SaveFileName = "prefsData.txt";
    private bool ScrambleSaveData = false;
    private string EncryptionCodeword = null;
    private bool AutoSaveData = true;
    private PrefsModel _latestData;

    const string String_Empty = "";

    public void Initialize(bool autoSaveData = false, string encryptionCodeword = null)
    {
        hasInit = true;
        AutoSaveData = autoSaveData;
        EncryptionCodeword = encryptionCodeword;
        ScrambleSaveData = EncryptionCodeword != null;
        GetSaveFile();
    }


    #region Public Get, Set and util

    public void SetStringArray(string key, string[] stringArray)
    {
        SetString(key, String.Join(",", stringArray));
    }

    public string[] GetStringArray(string key)
    {
        if (HasKeyForString(key))
        {
            if (!String.IsNullOrEmpty(GetString(key)))
                return GetString(key).Split(',');
        }

        return new string[0];
    }

    public void SetBoolArray(string key, bool[] boolArray)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < boolArray.Length - 1; i++)
            sb.Append(boolArray[i]).Append(",");
        sb.Append(boolArray[boolArray.Length - 1]);
        SetString(key, sb.ToString());
    }

    public bool[] GetBoolArray(string key)
    {
        string[] stringArray = GetStringArray(key);
        if (stringArray.Length > 0)
        {
            bool[] boolArray = new bool[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
                boolArray[i] = Convert.ToBoolean(stringArray[i]);
            return boolArray;
        }

        return new bool[0];
    }

    public void SetIntArray(string key, int[] intArray)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < intArray.Length - 1; i++)
            sb.Append(intArray[i]).Append(",");
        sb.Append(intArray[intArray.Length - 1]);
        SetString(key, sb.ToString());
    }

    public int[] GetIntArray(string key)
    {
        string[] stringArray = GetStringArray(key);
        if (stringArray.Length > 0)
        {
            int[] intArray = new int[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
                intArray[i] = Convert.ToInt32(stringArray[i]);
            return intArray;
        }

        return new int[0];
    }

    public void SetUintArray(string key, uint[] intArray)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < intArray.Length - 1; i++)
            sb.Append(intArray[i]).Append(",");
        sb.Append(intArray[intArray.Length - 1]);
        SetString(key, sb.ToString());
    }

    public uint[] GetUintArray(string key)
    {
        string[] stringArray = GetStringArray(key);
        if (stringArray.Length > 0)
        {
            uint[] uintArray = new uint[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
                uintArray[i] = Convert.ToUInt32(stringArray[i]);
            return uintArray;
        }

        return new uint[0];
    }

    public void SetFloatArray(string key, float[] floatArray)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < floatArray.Length - 1; i++)
            sb.Append(floatArray[i]).Append(",");
        sb.Append(floatArray[floatArray.Length - 1]);
        SetString(key, sb.ToString());
    }

    public float[] GetFloatArray(string key)
    {
        string[] stringArray = GetStringArray(key);
        if (stringArray.Length > 0)
        {
            float[] floatArray = new float[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
                floatArray[i] = Convert.ToSingle(stringArray[i]);
            return floatArray;
        }

        return new float[0];
    }

    public void SetString(string key, string value = String_Empty)
    {
        // Debug.LogErrorFormat("SetString {0} : {1}",key,value);
        AddDataToSaveFile(key, value);
    }

    public string GetString(string key, string defaultValue = String_Empty)
    {
        string str= (string) GetDataFromSaveFile(key, defaultValue);
        // Debug.LogErrorFormat("GetString {0} : {1}",key,str);
        return str;
    }

    public void SetInt(string key, int value = default(int))
    {
        AddDataToSaveFile(key, value);
    }

    public int GetInt(string key, int defaultValue = default(int))
    {
        return (int) GetDataFromSaveFile(key, defaultValue);
    }

    public void SetUint(string key, uint value = default(uint))
    {
        AddDataToSaveFile(key, value);
    }

    public uint GetUint(string key, uint defaultValue = default(uint))
    {
        return (uint) GetDataFromSaveFile(key, defaultValue);
    }

    public void SetFloat(string key, float value = default(float))
    {
        AddDataToSaveFile(key, value);
    }

    public float GetFloat(string key, float defaultValue = default(float))
    {
        return (float) GetDataFromSaveFile(key, defaultValue);
    }

    public void SetBool(string key, bool value = default(bool))
    {
        AddDataToSaveFile(key, value);
    }

    public bool GetBool(string key, bool defaultValue = default(bool))
    {
        return (bool) GetDataFromSaveFile(key, defaultValue);
    }

    public bool HasKey(string key)
    {
        return GetSaveFile().HasKey(key);
    }

    public bool HasKeyForString(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, String.Empty);
    }

    public bool HasKeyForInt(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(int));
    }

    public bool HasKeyForUint(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(uint));
    }

    public bool HasKeyForFloat(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(float));
    }

    public bool HasKeyForBool(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(bool));
    }

    public void DeleteKey(string key)
    {
        GetSaveFile().DeleteKey(key);
        SaveSaveFile();
    }

    public void DeleteString(string key)
    {
        GetSaveFile().DeleteString(key);
        SaveSaveFile();
    }

    public void DeleteInt(string key)
    {
        GetSaveFile().DeleteInt(key);
        SaveSaveFile();
    }

    public void DeleteUint(string key)
    {
        GetSaveFile().DeleteUint(key);
        SaveSaveFile();
    }

    public void DeleteFloat(string key)
    {
        GetSaveFile().DeleteFloat(key);
        SaveSaveFile();
    }

    public void DeleteBool(string key)
    {
        GetSaveFile().DeleteBool(key);
        SaveSaveFile();
    }

    public void DeleteAll()
    {
        WriteToSaveFile(LitJson.JsonMapper.ToJson(new PrefsModel()));
        _latestData = new PrefsModel();
    }

    public void OverwriteLocalSaveFile(string data)
    {
        WriteToSaveFile(data);
        _latestData = null;
    }

    #endregion


    #region Read data

    private PrefsModel GetSaveFile()
    {
        CheckSaveFileExists();
        if (_latestData == null)
        {
            var saveFileText = File.ReadAllText(GetSaveFilePath());
            if (ScrambleSaveData)
            {
                saveFileText = DataScrambler(saveFileText);
            }

            try
            {
                _latestData = JsonUtility.FromJson<PrefsModel>(saveFileText);
            }
            catch (ArgumentException e)
            {
                Debug.LogException(
                    new Exception("SAVE FILE IN WRONG FORMAT, CREATING NEW SAVE FILE : " + e.Message));
                DeleteAll();
            }
        }

        return _latestData;
    }

    public static string GetSaveFilePath()
    {
        string persistentDataPath = 
            //Application.isEditor ? Application.dataPath + "/PersistentAssets": 
            Application.persistentDataPath;
        return Path.Combine(persistentDataPath, SaveFileName);
    }

    public string GetSaveFileAsJson()
    {
        CheckSaveFileExists();
        return File.ReadAllText(GetSaveFilePath());
    }

    private object GetDataFromSaveFile(string key, object defaultValue)
    {
        return GetSaveFile().GetValueFromKey(key, defaultValue);
    }

    #endregion


    #region write data

    private void AddDataToSaveFile(string key, object value)
    {
        GetSaveFile().UpdateOrAddData(key, value);
        SaveSaveFile();
    }

    public void ManualySave()
    {
        SaveSaveFile(true);
    }

    private void SaveSaveFile(bool manualSave = false)
    {
        if (AutoSaveData || manualSave)
        {
            //WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
            WriteToSaveFile(LitJson.JsonMapper.ToJson(GetSaveFile()));
        }
    }

    private void WriteToSaveFile(string data)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(GetSaveFilePath()));

        var tw = new StreamWriter(GetSaveFilePath());
        if (ScrambleSaveData)
        {
            data = DataScrambler(data);
        }

        tw.Write(data);
        tw.Close();
    }

    #endregion


    #region File Utils

    private void CheckSaveFileExists()
    {
        if (!DoesSaveFileExist())
        {
            CreateNewSaveFile();
        }
    }

    private bool DoesSaveFileExist()
    {
        return File.Exists(GetSaveFilePath());
    }

    private void CreateNewSaveFile()
    {
        WriteToSaveFile(LitJson.JsonMapper.ToJson(new PrefsModel()));
    }

    public string DataScrambler(string data)
    {
        string res = "";
        for (int i = 0; i < data.Length; i++)
        {
            res += (char) (data[i] ^ EncryptionCodeword[i % EncryptionCodeword.Length]);
        }

        return res;
    }

    public void WriteStringAsJson(string fileName, string text)
    {
        string PathURL = // Application.isEditor ? Application.dataPath + "/PersistentAssets/" :
            Application.persistentDataPath + "/";

        Directory.CreateDirectory(Path.GetDirectoryName(PathURL));
        StreamWriter sw = new StreamWriter(PathURL + fileName + ".json");
        sw.Write(text);
        sw.Close();
    }

    public string ReadFileAsString(string fileName)
    {
        string persistentDataPath =
            //Application.isEditor ? Application.dataPath + "/PersistentAssets" :
            Application.persistentDataPath;
        string path = persistentDataPath + "/" + fileName + ".json";
        if (!File.Exists(path))
        {
            return string.Empty;
        }

        return File.ReadAllText(path);
    }

    #endregion


    public static void SetValue<T>(string key, T value)
    {
        try
        {
            var type = value.GetType();
            if (type == typeof(string))
            {
                inst.SetString(key, value.ToString());
            }
            else if (type == typeof(float))
            {
                inst.SetFloat(key, Convert.ToSingle(value));
            }
            else if (type == typeof(int))
            {
                inst.SetInt(key, Convert.ToInt32(value));
            }
            else if (type == typeof(long))
            {
                inst.SetString(key, Convert.ToString(value));
            }
            else if (type == typeof(bool))
            {
                inst.SetInt(key, Convert.ToInt32(value) > 0 ? 1 : 0);
            }
            else if (type == typeof(Enum))
            {
                inst.SetInt(key, Convert.ToInt32(value));
            }
            else if (type is object)
            {
                inst.SetString(key, JsonHelper.SerializeObject(value));
            }
            else
            {
                Debug.LogError("key:" + key + " type:" + value.GetType());
            }
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("SetValue<T:{0}> type{1}  error :{2}", typeof(T), value.GetType(), e.Message);
        }
    }

    public static T GetValue<T>(string key)
    {
        try
        {
            var type = typeof(T);

            if (type == typeof(string))
            {
                return (T) Convert.ChangeType(inst.GetString(key, string.Empty), type);
            }
            else if (type == typeof(float))
            {
                return (T) Convert.ChangeType(inst.GetFloat(key, 0.0f), type);
            }
            else if (type == typeof(int) ||(type == typeof(Enum)))
            {
                return (T) Convert.ChangeType(inst.GetInt(key), type);
            }
            else if (type == typeof(bool))
            {
                return (T) Convert.ChangeType(inst.GetInt(key) > 0, type);
            }
            else if (type == typeof(long))
            {
                return (T) Convert.ChangeType(inst.GetString(key,default(long).ToString()), type);
            } 
            else if (type is object)
            {
                var str = inst.GetString(key, string.Empty);
                if (string.IsNullOrEmpty(str))
                {
                    return default(T);
                }
                else
                    return JsonHelper.DeserializeObject<T>(str);
            }

            return default(T);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("GetValue<T:{0}>(key:{1})  error :{2}", typeof(T), key, e.Message);
            return default(T);
        }
    }

    public static T GetValue<T>(string key, T defaultValue)
    {
        if (!inst.HasKey(key))
        {
            return defaultValue;
        }

        return GetValue<T>(key);
    }
    
}

#region PrefsModel

[Serializable]
class PrefsModel
{
    public StringItem[] StringData = new StringItem[0];
    public IntItem[] IntData = new IntItem[0];
    public UintItem[] UintData = new UintItem[0];
    public FloatItem[] FloatData = new FloatItem[0];
    public BoolItem[] BoolData = new BoolItem[0];

    [Serializable]
    public class StringItem
    {
        public string Key;
        public string Value;

        public StringItem(string K, string V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class IntItem
    {
        public string Key;
        public int Value;

        public IntItem(string K, int V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class UintItem
    {
        public string Key;
        public uint Value;

        public UintItem(string K, uint V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class FloatItem
    {
        public string Key;
        public double Value;

        public FloatItem(string K, float V)
        {
            Key = K;
            Value = V;
        }
    }

    [Serializable]
    public class BoolItem
    {
        public string Key;
        public bool Value;

        public BoolItem(string K, bool V)
        {
            Key = K;
            Value = V;
        }
    }

    public object GetValueFromKey(string key, object defaultValue)
    {
        if (defaultValue is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    return StringData[i].Value;
                }
            }
        }

        if (defaultValue is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    return IntData[i].Value;
                }
            }
        }

        if (defaultValue is uint)
        {
            for (int i = 0; i < UintData.Length; i++)
            {
                if (UintData[i].Key.Equals(key))
                {
                    return UintData[i].Value;
                }
            }
        }

        if (defaultValue is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    return FloatData[i].Value;
                }
            }
        }

        if (defaultValue is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    return BoolData[i].Value;
                }
            }
        }

        return defaultValue;
    }

    public void UpdateOrAddData(string key, object value)
    {
        if (HasKeyFromObject(key, value))
        {
            SetValueForExistingKey(key, value);
        }
        else
        {
            SetValueForNewKey(key, value);
        }
    }

    private void SetValueForNewKey(string key, object value)
    {
        if (value is string)
        {
            var dataAsList = StringData.ToList();
            dataAsList.Add(new StringItem(key, (string) value));
            StringData = dataAsList.ToArray();
        }

        if (value is int)
        {
            var dataAsList = IntData.ToList();
            dataAsList.Add(new IntItem(key, (int) value));
            IntData = dataAsList.ToArray();
        }

        if (value is uint)
        {
            var dataAsList = UintData.ToList();
            dataAsList.Add(new UintItem(key, (uint) value));
            UintData = dataAsList.ToArray();
        }

        if (value is float)
        {
            var dataAsList = FloatData.ToList();
            dataAsList.Add(new FloatItem(key, (float) value));
            FloatData = dataAsList.ToArray();
        }

        if (value is bool)
        {
            var dataAsList = BoolData.ToList();
            dataAsList.Add(new BoolItem(key, (bool) value));
            BoolData = dataAsList.ToArray();
        }
    }

    private void SetValueForExistingKey(string key, object value)
    {
        if (value is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    StringData[i].Value = (string) value;
                }
            }
        }

        if (value is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    IntData[i].Value = (int) value;
                }
            }
        }

        if (value is uint)
        {
            for (int i = 0; i < UintData.Length; i++)
            {
                if (UintData[i].Key.Equals(key))
                {
                    UintData[i].Value = (uint) value;
                }
            }
        }

        if (value is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    FloatData[i].Value = (float) value;
                }
            }
        }

        if (value is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    BoolData[i].Value = (bool) value;
                }
            }
        }
    }

    public bool HasKeyFromObject(string key, object value)
    {
        if (value is string)
        {
            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is int)
        {
            for (int i = 0; i < IntData.Length; i++)
            {
                if (IntData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is uint)
        {
            for (int i = 0; i < UintData.Length; i++)
            {
                if (UintData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is float)
        {
            for (int i = 0; i < FloatData.Length; i++)
            {
                if (FloatData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        if (value is bool)
        {
            for (int i = 0; i < BoolData.Length; i++)
            {
                if (BoolData[i].Key.Equals(key))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void DeleteKey(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                var dataAsList = StringData.ToList();
                dataAsList.RemoveAt(i);
                StringData = dataAsList.ToArray();
            }
        }

        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                var dataAsList = IntData.ToList();
                dataAsList.RemoveAt(i);
                IntData = dataAsList.ToArray();
            }
        }

        for (int i = 0; i < UintData.Length; i++)
        {
            if (UintData[i].Key.Equals(key))
            {
                var dataAsList = UintData.ToList();
                dataAsList.RemoveAt(i);
                UintData = dataAsList.ToArray();
            }
        }

        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                var dataAsList = FloatData.ToList();
                dataAsList.RemoveAt(i);
                FloatData = dataAsList.ToArray();
            }
        }

        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                var dataAsList = BoolData.ToList();
                dataAsList.RemoveAt(i);
                BoolData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteString(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                var dataAsList = StringData.ToList();
                dataAsList.RemoveAt(i);
                StringData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteInt(string key)
    {
        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                var dataAsList = IntData.ToList();
                dataAsList.RemoveAt(i);
                IntData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteUint(string key)
    {
        for (int i = 0; i < UintData.Length; i++)
        {
            if (UintData[i].Key.Equals(key))
            {
                var dataAsList = UintData.ToList();
                dataAsList.RemoveAt(i);
                UintData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteFloat(string key)
    {
        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                var dataAsList = FloatData.ToList();
                dataAsList.RemoveAt(i);
                FloatData = dataAsList.ToArray();
            }
        }
    }

    public void DeleteBool(string key)
    {
        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                var dataAsList = BoolData.ToList();
                dataAsList.RemoveAt(i);
                BoolData = dataAsList.ToArray();
            }
        }
    }

    public bool HasKey(string key)
    {
        for (int i = 0; i < StringData.Length; i++)
        {
            if (StringData[i].Key.Equals(key))
            {
                return true;
            }
        }

        for (int i = 0; i < IntData.Length; i++)
        {
            if (IntData[i].Key.Equals(key))
            {
                return true;
            }
        }

        for (int i = 0; i < UintData.Length; i++)
        {
            if (UintData[i].Key.Equals(key))
            {
                return true;
            }
        }

        for (int i = 0; i < FloatData.Length; i++)
        {
            if (FloatData[i].Key.Equals(key))
            {
                return true;
            }
        }

        for (int i = 0; i < BoolData.Length; i++)
        {
            if (BoolData[i].Key.Equals(key))
            {
                return true;
            }
        }

        return false;
    }
}

#endregion