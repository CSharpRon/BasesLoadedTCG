package com.ucfstuds.GrandSlamAllstarsTwoElectricBoogaloo;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;

import com.unity3d.player.UnityPlayer;

public class LoginActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
    }

    public void updatePrefs() {

        //there are other ways you can get your app context, this is for using in static functions:
        Context appContext = UnityPlayer.currentActivity.getApplicationContext();

        //PlayerPrefs: unity uses package name for preferences file ("com.example.app")
        SharedPreferences prefs = appContext.getSharedPreferences(appContext.getPackageName(), Context.MODE_PRIVATE);

        //set stuff:
        prefs.edit().putInt("NumberOfShownAds", 0).commit();

        //get stuff:
        prefs.getInt("NumberOfShownAds", 0);
    }
}
