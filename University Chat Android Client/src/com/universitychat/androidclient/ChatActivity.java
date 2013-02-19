package com.universitychat.androidclient;


import com.universitychat.androidclient.ChatRoom.ChatRoomInterface;

import android.content.Context;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
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


public class ChatActivity extends FragmentActivity implements ChatRoomInterface
{
    private WebView webView;
    private final Handler chatWindowActivityHandler = new Handler();


    private TextView textViewChat;
    private EditText editMessage;
    private Button buttonSendMessage;
    private Button buttonExit;
    private Button tempWebView;
    private String userName;
    private String password;
    private String userInfo[];
    private ChatRoom chatRoomFragment;
    private ChatMemberList chatMemberListFragment;
    private ChatRoomList chatRoomListFragment;
    private ViewPager pager;
    private FragmentManager fm;
    private MyViewPagerAdapter pagerAdapter;
    private String[] chatUserList;

    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chat);
        
        Bundle extras = getIntent().getExtras();
  
        
        userInfo = extras.getStringArray("user_info");
        
        /** Getting a reference to the ViewPager defined the layout file */
        pager = (ViewPager) findViewById(R.id.pager);
 
        /** Getting fragment manager */
        fm = getSupportFragmentManager();
 
        /** Instantiating FragmentPagerAdapter */
        pagerAdapter = new MyViewPagerAdapter(fm);
 
        /** Setting the pagerAdapter to the pager object */
        pager.setAdapter(pagerAdapter);
        
        
        setUIVariables();
        pager.setCurrentItem(1);

        setWebView();
    }
    
    
    
    public void enableChatButtons()
    {
    	ChatRoom frag2 = (ChatRoom)pagerAdapter.getItem(1);

        frag2.enableButtons();
    }
	public void apendMessageToChat(String msg)
	{
		ChatRoom frag2 = (ChatRoom)pagerAdapter.getItem(1);

        frag2.updateChatText(msg);
		
	}
	
	public String getMsgText()
	{
		ChatRoom frag2 = (ChatRoom)pagerAdapter.getItem(1);

        return frag2.getUserMsg();
	}
	
	public void updateChatMemberList(String[] list)
	{

		ChatMemberList frag2 = (ChatMemberList)pagerAdapter.getItem(2);
		frag2.setChatMemberList(list);
	}
	

    private void setUIVariables() 
    {
        editMessage = (EditText)findViewById(R.id.editChatMessage);
        buttonExit = (Button)findViewById(R.id.buttonExit);
    }

    public void setWebView() 
    {
    	
        webView = (WebView)findViewById(R.id.androidWebViewTwo);
        

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
        webView.loadUrl("http://universitychat.azurewebsites.net/Android.html");
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
    	String message = getMsgText();
    	
        if(!message.isEmpty()) 
        {
            // send message to server.
//        	String message2 = message.replace("'", "\\'");
            String url = String.format("javascript:sendMessage('%s', '%s')", userName, message);
            webView.loadUrl(url);
        }
    }
    
    //method only for debugging, will be removed in beta release
    public void killApp(View view)
    {
    	this.finish();
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
                	apendMessageToChat(formattedMessage);
                }
            });
        }
        
        @JavascriptInterface
        public void setChannelUsers(String[] users)
        {
//        	String userlist = "Connected Users:";

        	chatUserList = users;
        	updateChatMemberList(chatUserList);

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
                	enableChatButtons();
                }
            });
        }
    }
}
