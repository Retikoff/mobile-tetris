package com.retikoff.tetris;

import android.view.KeyEvent;
import com.unity3d.player.UnityPlayerActivity;

public class CustomUnityActivity extends UnityPlayerActivity {

    @Override
    public boolean dispatchKeyEvent(KeyEvent event) {
        int keyCode = event.getKeyCode();
        int action = event.getAction();

        if (keyCode == KeyEvent.KEYCODE_VOLUME_DOWN) {
            if (action == KeyEvent.ACTION_DOWN) {
                com.unity3d.player.UnityPlayer.UnitySendMessage(
                    "VolumeButtonHandler", 
                    "OnVolumeDownPressed", 
                    ""
                );
            }
            return true;
        }

        if (keyCode == KeyEvent.KEYCODE_VOLUME_UP) {
            if(action == KeyEvent.ACTION_UP){
                com.unity3d.player.UnityPlayer.UnitySendMessage(
                    "VolumeButtonHandler",
                    "OnVolumeUpPressed",
                    ""
                );
            }
            return true;
        }

        return super.dispatchKeyEvent(event);
    }
}