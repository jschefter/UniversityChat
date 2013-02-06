package com.example.Java_Javascript_Test;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.webkit.ConsoleMessage;
import android.webkit.JavascriptInterface;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.os.Handler;


public class MainActivity extends Activity {
    private WebView webView;
    private final Handler activityHandler = new Handler();

    private EditText editUsername;
    private Button buttonJoinChat;
    private TextView textViewChat;
    private EditText editMessage;
    private Button buttonSendMessage;

    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);

        setUIVariables();
        setWebView();
        setClickListeners();
    }

    private void setUIVariables() {
        editUsername = (EditText)findViewById(R.id.editUsername);
        buttonJoinChat = (Button)findViewById(R.id.buttonJoinChat);
        textViewChat = (TextView)findViewById(R.id.textViewChat);
        editMessage = (EditText)findViewById(R.id.editMessage);
        buttonSendMessage = (Button)findViewById(R.id.buttonSendMessage);
    }

    private void setWebView() {
        webView = (WebView)findViewById(R.id.webView1);
        webView.setWebChromeClient(new WebChromeClient(){
            public boolean onConsoleMessage(ConsoleMessage cm) {
                Log.d("MyApplication", cm.message() + " -- from line " + cm.lineNumber() + " of " + cm.sourceId());
                return true;
            }
        });
        webView.getSettings().setJavaScriptEnabled(true);
        final WebAppInterface webAppInterface = new WebAppInterface(this);
        webView.addJavascriptInterface(webAppInterface, "Android");
        webView.loadUrl("http://universitychat.azurewebsites.net/Android.html");
    }

    private void setClickListeners() {
        buttonJoinChat.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                // disable UserName input & button
                buttonJoinChat.setEnabled(false);
                editUsername.setEnabled(false);

                // send to server that this user wants to join the chat...
                String username = editUsername.getText().toString();
                webView.loadUrl("javascript:joinChat('" + username + "')");
            }
        });

        buttonSendMessage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String username = editUsername.getText().toString();
                String message = editMessage.getText().toString();

                if(!message.isEmpty()) {
                    // send message to server.
                    String url = String.format("javascript:sendMessage('%s', '%s')", username, message);
                    webView.loadUrl(url);
                }
            }
        });
    }

    // the methods of this class with the @JavascriptInterface attributes are called from javascript executed by the WebView
    private class WebAppInterface {
        Context mContext;

        WebAppInterface(Context c) {
            mContext = c;
        }

        @JavascriptInterface
        public void appendMessage(String message) {
            final String formattedMessage = String.format("\n%s", message);

            activityHandler.post(new Runnable() {
                @Override
                public void run() {
                    textViewChat.append(formattedMessage);
                }
            });
        }

        @JavascriptInterface
        public void pageLoadComplete() {
            activityHandler.post(new Runnable() {
                @Override
                public void run() {
                    // enable UserName input & button
                    buttonJoinChat.setEnabled(true);
                    editUsername.setEnabled(true);
                }
            });
        }

        @JavascriptInterface
        public void joinChatComplete() {
            activityHandler.post(new Runnable() {
                @Override
                public void run() {
                    // enable Message input & button
                    editMessage.setEnabled(true);
                    buttonSendMessage.setEnabled(true);
                }
            });
        }
    }
}
