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
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Typeface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;


public class LoginWindow extends Activity 
{
	private String[] userCredentials;
	private EditText userNameText;
	private EditText passwordText;
	private Button loginButton;
	private Button loginAnonymousButton;
	private TextView signUpLink;
	private AlertDialog.Builder builder;
	private AlertDialog dialog;
	private String newHostURL;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		setUIVariables();
		Typeface orbitron = Typeface.createFromAsset(getAssets(), "fonts/orbitron-black.otf");
		TextView tv = (TextView) findViewById(R.id.uchatheader);
		tv.setTypeface(orbitron);
		newHostURL = null;	
	}
	
	
	private void setUIVariables() 
    {
        userNameText = (EditText)findViewById(R.id.txt_userName);
        passwordText = (EditText)findViewById(R.id.txt_pw);
        loginButton = (Button)findViewById(R.id.btn_login);
        loginAnonymousButton = (Button)findViewById(R.id.btn_loginAnonymous);
        signUpLink = (TextView)findViewById(R.id.textView_signuplink);
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
                
                builder.setPositiveButton("Apply", new DialogInterface.OnClickListener() {
     	           public void onClick(DialogInterface dialog, int id) {
     	               newHostURL = editText.getText().toString();
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
		
		userCredentials = new String[]{ userName, password };
		new AuthenticationTask().execute(userName, password);
	}
	
	private void startChat()
	{
		Intent chatWindowIntent = new Intent(this, ChatActivity.class);
		chatWindowIntent.putExtra("user_credentials",userCredentials);
		chatWindowIntent.putExtra("newHostURL", newHostURL);
		startActivity(chatWindowIntent);
		this.finish();
	}

	// attempts to authenticate user credentials with the university chat backend.
	private class AuthenticationTask extends AsyncTask<String, Void, Boolean> {

		protected Boolean doInBackground(String... credentials) {
			
			try {
				String query = "username=" + URLEncoder.encode(credentials[0], "UTF-8") + "&password=" + URLEncoder.encode(credentials[1], "UTF-8");
				URL url = new URL(Constants.DEFAULT_LOGIN);
				HttpURLConnection connection = null;
				InputStream inputStream = null;
				
				connection = (HttpURLConnection) url.openConnection();
				connection.setDoOutput(true);
				connection.setDoInput(true);
				connection.setInstanceFollowRedirects(false);
				connection.setRequestMethod("POST");
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
			if(result) {
				// user is authenticated.
				startChat();
			}
			else {
				// user authentication failed.
			}
		}
	}
}
