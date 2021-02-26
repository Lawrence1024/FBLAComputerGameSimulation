using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void saveAccount(Account account)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string tempName = account.userName;
        string path = Application.persistentDataPath + "/" + tempName + ".account";
        FileStream stream = new FileStream(path, FileMode.Create);

        AccountData data = new AccountData(account);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static AccountData loadAccount(Account account)
    {
        string tempName = account.userName;
        string path = Application.persistentDataPath + "/"+tempName+".account";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AccountData data = formatter.Deserialize(stream) as AccountData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
