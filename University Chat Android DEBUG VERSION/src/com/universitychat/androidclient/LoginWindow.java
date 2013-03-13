package com.universitychat.androidclient;

//import android.R;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.Random;

//import android.R;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Typeface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;


public class LoginWindow extends Activity 
{
	private String[] userCredentials;
	private EditText userNameText;
	private EditText passwordText;
	private Button loginButton;
	private Button loginAnonymousButton;
	private CheckBox saveUserChkbx;
	private TextView signUpLink;
	private AlertDialog.Builder builder;
	private AlertDialog dialog;
	private String newHostURL; //user supplied host
	private int loginFlag; //determines whether login or loginanonymous button pressed
	
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		setUIVariables();
		Typeface orbitron = Typeface.createFromAsset(getAssets(), "fonts/orbitron-black.otf");
		TextView tv = (TextView) findViewById(R.id.uchatheader);
		tv.setTypeface(orbitron);
		loginFlag = -1;
		newHostURL = null;
		
		//clear stored pref data
//		SharedPreferences sharedPref = getSharedPreferences(Constants.LOG_IN_PREF,Context.MODE_PRIVATE);
//		SharedPreferences.Editor editor = sharedPref.edit();
//		editor.putString("username", ""); //clear stored info
//		editor.putString("password", ""); //clear stored info
//		editor.putString("savedlogin", "no");
//		editor.commit();
		
		//check if user set host
		SharedPreferences sharedPref1 = getSharedPreferences(Constants.HOST_PREF,Context.MODE_PRIVATE);
		newHostURL = sharedPref1.getString("hostURL","");
		System.out.println("hostURL after shared pref get: " + newHostURL);
		
		//check if user opted to save log in info and use it if valid
		SharedPreferences sharedPref2 = getSharedPreferences(Constants.LOG_IN_PREF,Context.MODE_PRIVATE);
		String savedLogin = sharedPref2.getString("savedlogin","");
		if(savedLogin.equals("yes"))
		{
			System.out.println("attempt to re-login user");
			userCredentials = new String[2];
			userCredentials[0] = sharedPref2.getString("username","");
			userCredentials[1] = sharedPref2.getString("password","");
			System.out.println("Saved UN: " + userCredentials[0]);
			System.out.println("Saved PW: " + userCredentials[1]);
			loginFlag = 0;
			new AuthenticationTask().execute(userCredentials[0], userCredentials[1]);
		}
	}	
	
	private void setUIVariables() 
    {
        userNameText = (EditText)findViewById(R.id.txt_userName);
        passwordText = (EditText)findViewById(R.id.txt_pw);
        loginButton = (Button)findViewById(R.id.btn_login);
        loginAnonymousButton = (Button)findViewById(R.id.btn_loginAnonymous);
        signUpLink = (TextView)findViewById(R.id.textView_signuplink);
        saveUserChkbx = (CheckBox)findViewById(R.id.chkbx_user_save_login);
    }

	@Override
	public boolean onCreateOptionsMenu(Menu menu) 
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.menu_login, menu);
		return true;
	}
	
	//Handle event handling for individual menu items
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
 
        switch (item.getItemId())
        {
        	case R.id.menu_about:
        		builder = new AlertDialog.Builder(this);
        		builder.setTitle(R.string.about_title);
        		builder.setMessage(R.string.about_content);
        		builder.setPositiveButton(R.string.prompt_close, new DialogInterface.OnClickListener() {
        	           public void onClick(DialogInterface dialog, int id) {
        	               dialog.cancel();
        	           }});
        	       
        		dialog = builder.create();
        		
        		dialog.show();
        		return true;
        		
        	case R.id.menu_change_connection_host:
        		builder = new AlertDialog.Builder(this);
        		LayoutInflater layInf =LayoutInflater.from(this);
                View view = layInf.inflate(R.layout.edit_text, null);
                final TextView editText = (TextView) view.findViewById(R.id.editText_change_host);
                
                //set the textbox to be that of the current host whether default or set by user
                if(newHostURL.equals(""))
                	editText.setText(Constants.DEFAULT_HOST);
                else
                	editText.setText(newHostURL);        
                
                builder.setPositiveButton("Apply", new DialogInterface.OnClickListener() {
     	           public void onClick(DialogInterface dialog, int id) {
     	               newHostURL = editText.getText().toString();
     	               SharedPreferences sharedPref = getSharedPreferences(Constants.HOST_PREF,Context.MODE_PRIVATE);
     	               SharedPreferences.Editor editor = sharedPref.edit();
     	               editor.putString("hostURL", newHostURL); //clear stored info
     	               editor.commit();
     	           }});
     	       
                builder.setNeutralButton("Reset Host", new DialogInterface.OnClickListener() {
       	           public void onClick(DialogInterface dialog, int id) {
						SharedPreferences sharedPref = getSharedPreferences(Constants.HOST_PREF,Context.MODE_PRIVATE);
						SharedPreferences.Editor editor = sharedPref.edit();
						editor.putString("hostURL", Constants.DEFAULT_HOST); //clear stored info
						newHostURL = "";
						editor.commit();
						dialog.cancel();
						Toast.makeText(getApplicationContext(), "Host set to default", Toast.LENGTH_LONG).show();
						
       	           }});
                
                builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
      	           public void onClick(DialogInterface dialog, int id) {
      	               dialog.cancel();
      	           }});
      	       
                builder.setView(view);
        		builder.setTitle(R.string.menu_change_host);
        		builder.setCancelable(true);
        		dialog = builder.create();
        		dialog.setCancelable(true); //cancelable by back button
        		dialog.setCanceledOnTouchOutside(false); //non-cancelable by click outside
        		dialog.show();
        		return true;
 
        	default:
        		return super.onOptionsItemSelected(item);
        }
    } 
	
	public void startSignUpActivity(View view)
	{
		Intent signUpIntent = new Intent(this, SignupActivity.class);
		startActivity(signUpIntent);
	}
	
	public void loginAttempt(View view)
	{
		String userName = "AndroidUser";
		String password = "";

		//get login data
		switch(view.getId())
        {
            case R.id.btn_login: 
            	userName = userNameText.getText().toString();
        		password = passwordText.getText().toString();
        		loginFlag = 0; //login button pressed
            	break;
            case R.id.btn_loginAnonymous:
            	Random randomGen = new Random();
            	int r = randomGen.nextInt(100);
            	userName = userName + r;
            	loginFlag = 1; //login anonymous button pressed
            	break;
        }
		
		userCredentials = new String[]{ userName, password };
		new AuthenticationTask().execute(userName, password);
	}
	
	private void startChat()
	{
		Intent chatWindowIntent = new Intent(this, ChatActivity.class);
		chatWindowIntent.putExtra("user_credentials",userCredentials);
		chatWindowIntent.putExtra("newHost", newHostURL);
		startActivity(chatWindowIntent);
		this.finish();
	}

	// attempts to authenticate user credentials with the university chat backend.
	private class AuthenticationTask extends AsyncTask<String, Void, Boolean> {

		protected Boolean doInBackground(String... credentials) {
			
			try {
				String query = "username=" + URLEncoder.encode(credentials[0], "UTF-8") + "&password=" + URLEncoder.encode(credentials[1], "UTF-8");
				 URL url;// = new URL(Constants.DEFAULT_LOGIN);
				
				if(newHostURL.equals("")) //use default URL
					url = new URL(Constants.DEFAULT_HOST +  Constants.DEFAULT_LOGIN_EXT); 
				else //user has supplied host URL
					url = new URL(newHostURL + Constants.DEFAULT_LOGIN_EXT);
			
				System.out.println("Login Host: " + url.toString());
				
				HttpURLConnection connection = null;
				InputStream inputStream = null;
				
				
				connection = (HttpURLConnection) url.openConnection();
				
				connection.setDoOutput(true);
				connection.setDoInput(true);
				connection.setInstanceFollowRedirects(false);
				connection.setRequestMethod("POST");
//				connection.setConnectTimeout(20000); //added 20 sec timeout
//				connection.setReadTimeout(20000); //added 20 sec timeout
				connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded"); 
				connection.setRequestProperty("charset", "utf-8");
				connection.setRequestProperty("Content-Length", "" + Integer.toString(query.getBytes().length));
				connection.setUseCaches (false);
				
				DataOutputStream writer = new DataOutputStream(connection.getOutputStream());
				writer.writeBytes(query);
				writer.flush();
				writer.close();
				
				inputStream = connection.getInputStream();
				BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream));
				String line;
				StringBuilder sb = new StringBuilder();
				while((line = reader.readLine()) != null) {
					sb.append(line);
				}
				reader.close();
				connection.disconnect();
				
				String result = sb.toString();
				System.out.println(result);
				return result.equals("Authenticated")? true : false;
			}
			catch (Exception e)
			{
				e.printStackTrace();
			}
			
			return false;
		}
		
		protected void onPostExecute(Boolean result)
		{
			if(result) // user is authenticated.
			{
				SharedPreferences sharedPref = getSharedPreferences(Constants.LOG_IN_PREF,Context.MODE_PRIVATE);
				SharedPreferences.Editor editor = sharedPref.edit();
				//remember login credidentials
				if(saveUserChkbx.isChecked() && loginFlag == 0) //checkbox checked and login button pressed
				{
					editor.putString("username", userCredentials[0]);
					editor.putString("password", userCredentials[1]); //pw in plain text need to fix
					editor.putString("savedlogin", "yes");
//					System.out.println("Before Commit UN: " + userCredentials[0]);
//					System.out.println("Before Commit PW: " + userCredentials[1]);
					editor.commit();
				}
				else //log in not saved
				{
					editor.putString("username", ""); //clear stored info
					editor.putString("password", ""); //clear stored info
					editor.putString("savedlogin", "no");
					editor.commit();
				}
				startChat();
			}
			else // user authentication failed.
			{
				Toast.makeText(getApplicationContext(), "Please Enter Valid Log in Credidentials", Toast.LENGTH_LONG).show();
			}
		}
	}
}
