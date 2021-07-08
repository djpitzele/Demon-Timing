using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public static void Save()
    {
        //DONT FORGET TO SET SaveGame.current RIGHT BEFORE SAVING
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/savedGame.dt"))
        {
            File.Delete(Application.persistentDataPath + "/savedGame.dt");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.dt");
        bf.Serialize(file, SaveGame.current);
        file.Close();
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Lobby")
        {
            if (File.Exists(Application.persistentDataPath + "/PermVar.dt"))
            {
                File.Delete(Application.persistentDataPath + "/PermVar.dt");
            }
            FileStream PermFile = File.Create(Application.persistentDataPath + "/PermVar.dt");
            bf.Serialize(file, PermVar.current);
            PermFile.Close();
        }
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.dt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.dt", FileMode.Open);
            SaveGame.current = (SaveGame)bf.Deserialize(file);
            file.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/PermVar.dt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PermVar.dt", FileMode.Open);
            PermVar.current = (PermVar)bf.Deserialize(file);
            file.Close();
        }
        //AFTER CALLING LOAD, use SaveGame.sg (static) to update game state
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
