package com.universitychat.androidclient;

import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.FragmentActivity;
import android.support.v4.view.ViewPager;
import android.util.Log;
import android.view.View;
import android.webkit.ConsoleMessage;
import android.webkit.JavascriptInterface;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;


public class ChatWindow extends FragmentActivity {
    private WebView webView;
    private final Handler chatWindowActivityHandler = new Handler();

    private static String HOST = "http://universitychat.azurewebsites.net/Android.html";
    private TextView textViewChat;
    private EditText editMessage;
    private Button buttonSendMessage;
    private String userName;
    private String password;
    private String userInfo[];
    //private ViewPager mViewPager;

    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chat_window);
        Bundle extras = getIntent().getExtras();
        
        setUIVariables();
        setWebView();
        userInfo = extras.getStringArray("user_info");

        // Set up the ViewPager, attaching the adapter and setting up a listener for when the
        // user swipes between sections.
//        mViewPager = (ViewPager) findViewById(R.id.pager);
//        mViewPager.setAdapter(mAppSectionsPagerAdapter);
    }


    private void setUIVariables() 
    {
        textViewChat = (TextView)findViewById(R.id.textViewChat);
        editMessage = (EditText)findViewById(R.id.editMessage);
        buttonSendMessage = (Button)findViewById(R.id.buttonSendMessage);
    }

    private void setWebView() 
    {
        webView = (WebView)findViewById(R.id.webView1);
        
        // set up logging of javascript console messages (for debugging purposes)
        webView.setWebChromeClient(new WebChromeClient(){
            public boolean onConsoleMessage(ConsoleMessage cm) {
                Log.d("MyApplication", cm.message() + " -- from line " + cm.lineNumber() + " of " + cm.sourceId());
                return true;
            }
        });
        
        webView.getSettings().setJavaScriptEnabled(true);
        final WebAppInterface webAppInterface = new WebAppInterface(this);
        webView.addJavascriptInterface(webAppInterface, "Android");
        webView.loadUrl(HOST);
    }

    
    private void setUpChatConnection(String[] userInfo)
    {
    	userName = userInfo[0];
    	//password = userInfo[1];
    	password = "pw";
    	webView.loadUrl("javascript:joinChat('" + userName + "')");
    }
    
    public void sendMessage(View view)
    {
    	String message = editMessage.getText().toString();
    	
        if(!message.isEmpty()) 
        {
            // send message to server.
            String url = String.format("javascript:sendMessage('%s', '%s')", userName, message);
            webView.loadUrl(url);
            editMessage.setText("");
        }
    }

    // the methods of this class with the @JavascriptInterface attributes are called from javascript executed by the WebView
    private class WebAppInterface 
    {
        Context mContext;

        WebAppInterface(Context c) 
        {
            mContext = c;
        }

        @JavascriptInterface
        public void appendMessage(String message) 
        {
            final String formattedMessage = String.format("\n%s", message);

            chatWindowActivityHandler.post(new Runnable() 
            {
                @Override
                public void run() 
                {
                    textViewChat.append(formattedMessage);
                    
                    final int offset = 
                    		textViewChat.getLayout().getLineTop(textViewChat.getLineCount())  - textViewChat.getHeight();

                    if(offset > 0) textViewChat.scrollTo(0, offset);
                    else textViewChat.scrollTo(0, 0);
                }
            });
        }

        @JavascriptInterface
        public void pageLoadComplete() 
        {
            chatWindowActivityHandler.post(new Runnable() 
            {
                @Override
                public void run() 
                {
                	setUpChatConnection(userInfo);
                }
            });
        }

        @JavascriptInterface
        public void joinChatComplete() 
        {
            chatWindowActivityHandler.post(new Runnable() 
            {
                @Override
                public void run() 
                {
                    // enable Message input & button
                    editMessage.setEnabled(true);
                    buttonSendMessage.setEnabled(true);
                }
            });
        }
    }
}
