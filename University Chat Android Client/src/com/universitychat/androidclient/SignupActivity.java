package com.universitychat.androidclient;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class SignupActivity extends Activity
{
	private EditText editName;
	private EditText editUserName;
	private EditText editEmail;
	private EditText editPassword;
	private EditText editVerifyPassword;
	private Button buttonConfirm;
	private final String INCOMPLETE_DATA = "Make sure all data entered";
	private final String PW_NO_MATCH = "Password do not match";

	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        setTitle("Sign-Up for University Chat");
        setContentView(R.layout.activity_signup);
        setUIVariables();
    }
	
	public void setUIVariables()
	{
		editName = (EditText)findViewById(R.id.editTextName);
		editUserName = (EditText)findViewById(R.id.editTextUserName);
		editEmail = (EditText)findViewById(R.id.editTextEmail);
		editVerifyPassword = (EditText)findViewById(R.id.editTextVPassword);
		editPassword = (EditText)findViewById(R.id.editTextPassword);
		buttonConfirm = (Button)findViewById(R.id.btn_signup_confirm);	
	}
	
	public void confirmCredidentials(View v)
	{
		if(editName.getText().toString().equals("") || editUserName.getText().toString().equals("") || 
				editEmail.getText().toString().equals("") || editPassword.getText().toString().equals("") 
				|| editVerifyPassword.getText().toString().equals(""))
		{
			Toast.makeText(getApplicationContext(), INCOMPLETE_DATA, Toast.LENGTH_LONG).show();
		}
		
		//currently both toasts can display one after another, fix or keep?
		if(!editPassword.getText().toString().equals(editVerifyPassword.getText().toString()))
		{
			Toast.makeText(getApplicationContext(), PW_NO_MATCH, Toast.LENGTH_LONG).show();
		}

	}
}
