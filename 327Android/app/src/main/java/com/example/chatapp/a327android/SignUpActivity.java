package com.example.chatapp.a327android;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import org.json.JSONObject;

import java.net.HttpURLConnection;
import java.net.URL;

public class SignUpActivity extends AppCompatActivity {
    Button btn_login;
    Button btn_signup;

    private URL url;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);
        btn_login = (Button) findViewById(R.id.btn_login);
        btn_login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                LoginActivity();
            }
        });
        btn_signup = (Button) findViewById(R.id.btn_signup);
        btn_signup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    JSONObject cred   = new JSONObject();
                    cred.put("Username","");
                    url=new URL("https://localhost:44398/api/authenticate/register");
                    HttpURLConnection con = (HttpURLConnection)url.openConnection();
                    con.setRequestMethod("POST");
                    con.setRequestProperty("Content-Type", "application/json; utf-8");

                }
                catch (Exception e){

                }
                LoginActivity();
            }
        });

    }
    public void LoginActivity(){
        Intent intent = new Intent(this, LoginActivity.class);
        startActivity(intent);
    }
}