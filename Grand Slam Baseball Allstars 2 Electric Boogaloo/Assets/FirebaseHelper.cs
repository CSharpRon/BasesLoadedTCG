using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FirebaseHelper : MonoBehaviour
{
    private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

    private static FirebaseHelper mInstance;
    public static DatabaseReference teamRef;
    public static DatabaseReference usersRef;
    public static DatabaseReference playersRef;

    public FirebaseHelper()
    {
        // playersRef = FirebaseDatabase.DefaultInstance.GetReference("Players");
    }

    public static DatabaseReference Users()
    {
        return baseRef.Child("Users");
    }

    public static DatabaseReference Teams()
    {
        return baseRef.Child("Teams");
    }

    public static DatabaseReference Team(string teamname)
    {
        return baseRef.Child("Teams").OrderByChild("teamname").EqualTo(teamname).Reference;
    }

    public static DatabaseReference SetWithUUIDPlayers(string uuid)
    {
        return baseRef.Child("Users").Child(uuid);
    }

    public static DatabaseReference SetWithUUIDTeams(string uuid)
    {
        return baseRef.Child("Teams").Child(uuid);
    }

}
