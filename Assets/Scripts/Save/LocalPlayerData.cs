using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalPlayerData 
{  
    public class PlayerData
    {
        public class PlayerInfo
        {
            public float maxHealth;
            public float currentHealth;
            public float attackPower;
            public float criticalRate;
            public float criticalDamage;

            public PlayerInfo(float maxHealth, float currentHealth, float attackPower, float criticalRate, float criticalDamage)
            {
                this.maxHealth = maxHealth;
                this.currentHealth = currentHealth;
                this.attackPower = attackPower;
                this.criticalRate = criticalRate;
                this.criticalDamage = criticalDamage;
            }
            public PlayerInfo()
            {
                this.maxHealth = 100;
                this.currentHealth = 100;
                this.attackPower = 5;
                this.criticalRate = 0.05f;
                this.criticalDamage = 0.5f;
            }
        }
        public PlayerInfo playerInfo;
        public float playerTransformx;
        public float playerTransformy;
        public int numScene;

        public PlayerData()
        {
            this.playerInfo = new PlayerInfo();
            this.playerTransformx=0;
            this.playerTransformy=0;
            this.numScene = 0;
        }

        public PlayerData(PlayerInfo playerInfo, Transform playerTransform, int numScene)
        {
            this.playerInfo = new PlayerInfo(playerInfo.maxHealth,playerInfo.currentHealth,playerInfo.attackPower,playerInfo.criticalRate,playerInfo.criticalDamage);
            this.playerTransformx = playerTransform.position.x;
            this.playerTransformy = playerTransform.position.y;
            this.numScene = numScene;
        }
    }
    public static void SavePlayerData(PlayerData playerData)
    {
        if (!File.Exists(Application.persistentDataPath))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath);
        }
        if (!File.Exists(Application.persistentDataPath + string.Format("/SaveData"))) 
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + string.Format("/SaveData"));
        }
        string jsonData = JsonConvert.SerializeObject(playerData);
        File.WriteAllText(Application.persistentDataPath + string.Format("/SaveData/PlayerData.json"), jsonData);
    }
    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/SaveData/PlayerData.json";
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            PlayerData playerdata = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return playerdata;

        }
        else
        {
            return null;
        }
    }
}
