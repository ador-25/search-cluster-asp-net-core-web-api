package com.example.chatapp.a327android;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class LoginActivity extends AppCompatActivity {
    Button btn_Signup,btn_Login;
    EditText etUserNameLogin,etPasswordLogin;
    TextView temp;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        etUserNameLogin = (EditText) findViewById(R.id.etUserNameLogin);
        etPasswordLogin=(EditText) findViewById(R.id.etPasswordLogin);

        String username= etUserNameLogin.getText().toString();
        String password= etPasswordLogin.getText().toString();
        btn_Signup = (Button) findViewById(R.id.btn_Signup);
        btn_Login = (Button) findViewById(R.id.btn_Login);
        temp=(TextView) findViewById(R.id.temp);
        btn_Signup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SignUpActivity();
            }
        });
        btn_Login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                temp.setText(etUserNameLogin.getText().toString() +" "+etPasswordLogin.getText().toString() );
                
            }
        });



    }
    // Change activity
    public void SignUpActivity(){
        Intent intent = new Intent(this, SignUpActivity.class);
        startActivity(intent);
    }
}