using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class LocalConfig
{
    public static Configdata cacheConfigData;
    public class Configdata
    {
        public float maxHealth;
        public float currentHealth;
    }
    public static void SaveConfigData(Configdata configdata)
    {
        if(!File.Exists(Application.persistentDataPath+"/users"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        string jsonData=JsonConvert.SerializeObject(configdata);
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/config.json"), jsonData);
        cacheConfigData = configdata;
    }
    public static Configdata LoadConfigData()
    {
        string path = Application.persistentDataPath + "/users/config.json";
        if (File.Exists(path))
        {
            if(cacheConfigData!=null) return cacheConfigData;
            else
            {
                string jsonData = File.ReadAllText(path);
                Configdata configdata = JsonConvert.DeserializeObject<Configdata>(jsonData);
                cacheConfigData = configdata;
                return configdata;
            }
            
        }
        else
        {
            return null;
        }
    }
}
