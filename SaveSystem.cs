using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
   public static void SaveHighscore(int Highscore)
    {
        //Formatter wird generiert
        BinaryFormatter formatter = new BinaryFormatter();

        //Path der Save Datei wird festgelegt
        string path = Application.persistentDataPath + "/highscore.dotaquizz";

        //stream wird geöffnet
        FileStream stream = new FileStream(path, FileMode.Create);

        //Datei wird generiert und gespeichert
        HighScore score = new HighScore();
        score.highScore = Highscore;

        //stream wird geschlossen
        formatter.Serialize(stream, score);
        stream.Close();
    }

    public static HighScore LoadHighscore()
    {
        //Path wird geöffnet
        string path = Application.persistentDataPath + "/highscore.dotaquizz";
        //Save Datei wird gesucht
        if (File.Exists(path))
        {
            //Formatter wird generiert
            BinaryFormatter formatter = new BinaryFormatter();

            //Datei wird geöffnet
            FileStream stream = new FileStream(path, FileMode.Open);

            HighScore highScore = (HighScore)formatter.Deserialize(stream);  
            
            //stream wird geschlossen
            stream.Close();

            return highScore;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
