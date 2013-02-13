package com.universitychat.androidclient;

import android.app.Activity;
//import android.client.R;
import android.content.Intent;
import android.graphics.Typeface;
import android.os.Bundle;
import android.view.Menu;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import java.util.Random;


public class LoginWindow extends Activity 
{
	
	private String[] loginInfo;
	private EditText userNameText;
	private EditText passwordText;
	private Button loginButton;
	private Button loginAnonymousButton;

	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.login);
		setUIVariables();
		Typeface orbitron = Typeface.createFromAsset(getAssets(), "fonts/orbitron-black.otf");
		TextView tv = (TextView) findViewById(R.id.uchatheader);
		tv.setTypeface(orbitron);
	}
	
	private void setUIVariables() 
    {
        userNameText = (EditText)findViewById(R.id.txt_userName);
        passwordText = (EditText)findViewById(R.id.txt_pw);
        loginButton = (Button)findViewById(R.id.btn_login);
        loginAnonymousButton = (Button)findViewById(R.id.btn_loginAnonymous);
    }

	@Override
	public boolean onCreateOptionsMenu(Menu menu) 
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.login, menu);
		return true;
	}
	
	public void loginAttempt(View view)
	{
		String userName = "AndroidAnonymous";
		String password = "pw";

		//get login data
		switch(view.getId())
        {
            case R.id.btn_login: 
            	userName = userNameText.getText().toString();
        		password = passwordText.getText().toString();
            	break;
            case R.id.btn_loginAnonymous:
            	Random randomGen = new Random();
            	int r = randomGen.nextInt(100);
            	userName = userName + r;
            	break;

        }
		loginInfo = new String[]{userName, password};
		Intent chatWindowIntent = new Intent(this, ChatWindow.class);
		chatWindowIntent.putExtra("user_info",loginInfo);
		startActivity(chatWindowIntent);
		this.finish();
	}
}