package com.retikoff.tetris;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.view.KeyEvent;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class CustomUnityActivity extends UnityPlayerActivity implements SensorEventListener {

    private SensorManager sensorManager;
    private Sensor accelerometer;

    @Override
    protected void onResume() {
        super.onResume();
        
        sensorManager = (SensorManager) getSystemService(SENSOR_SERVICE);
        accelerometer = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        
        if (accelerometer != null) {
            sensorManager.registerListener(this, accelerometer, SensorManager.SENSOR_DELAY_GAME); // быстрый режим
        }
    }

    @Override
    protected void onPause() {
        super.onPause();
        if (sensorManager != null) {
            sensorManager.unregisterListener(this);
        }
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        if (event.sensor.getType() == Sensor.TYPE_ACCELEROMETER) {
            float x = event.values[0];
            float y = event.values[1];
            float z = event.values[2];
            
            String data = x + "," + y + "," + z;
            UnityPlayer.UnitySendMessage("AccelerometerHandler", "OnAccelerometerChanged", data);
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) { }

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