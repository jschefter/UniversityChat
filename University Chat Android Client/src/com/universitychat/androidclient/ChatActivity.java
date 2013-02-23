package com.universitychat.androidclient;

import java.util.Vector;

import com.universitychat.androidclient.fragments.ChatMemberList;
import com.universitychat.androidclient.fragments.ChatRoom;
import com.universitychat.androidclient.fragments.ChatRoomList;

import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
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


public class ChatActivity extends FragmentActivity
{
	private final String URL = "http://uc-channels.azurewebsites.net/Android.html";//"http://universitychat.azurewebsites.net/Android.html";
    private WebView webView;
    private final Handler chatWindowActivityHandler = new Handler();


    private TextView textViewChat;
    private EditText editMessage;
    private Button buttonSendMessage;
    private Button buttonExit;
    private Button tempWebView;
    private String username;
    private String password;
    private String currentChannel;
    private ViewPager viewPager;
    private FragmentManager fragmentManager;
    private MyViewPagerAdapter pagerAdapter;
    private OutgoingWebEvents outgoingWebEvents;

    /**
     * Called when the activity is first created.
     */
    @Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        outgoingWebEvents = new OutgoingWebEvents();
        
        setContentView(R.layout.activity_chat);
        
        Bundle extras = getIntent().getExtras();
        String[] userCredentials = extras.getStringArray("user_credentials");
        username = userCredentials[0];
        password = userCredentials[1];
        currentChannel = "";
        
        initializeWebView();
        
        
        /** Getting a reference to the ViewPager defined the layout file */
        viewPager = (ViewPager) findViewById(R.id.pager);
 
        /** Getting fragment manager */
        fragmentManager = getSupportFragmentManager();
                
        Vector<Fragment> fragments = new Vector<Fragment>();
        fragments.add(Fragment.instantiate(this, ChatRoomList.class.getName()));
        fragments.add(Fragment.instantiate(this, ChatRoom.class.getName()));
        fragments.add(Fragment.instantiate(this, ChatMemberList.class.getName()));
         
        /** Instantiating FragmentPagerAdapter */
        pagerAdapter = new MyViewPagerAdapter(fragmentManager, fragments);
 
        /** Setting the pagerAdapter to the pager object */
        viewPager.setAdapter(pagerAdapter);
        
        
        setUIVariables();
        viewPager.setCurrentItem(0);

        
        webView.loadUrl(URL);	// connect to service (start the hub).
    }
    
    private void updateChatRoomList(String[] roomList)
    {
    	ChatRoomList chatRoomList = (ChatRoomList)pagerAdapter.getItem(0);
    	chatRoomList.updatePublicRoomList(roomList);
    }
    
    private void enableChatButtons()
    {
    	ChatRoom chatRoom = (ChatRoom)pagerAdapter.getItem(1);
        chatRoom.enableButtons();
    }
    
	private void appendMessageToChat(String username, String msg)
	{
		ChatRoom chatRoom = (ChatRoom)pagerAdapter.getItem(1);
        chatRoom.updateChatText(username, msg);
	}
	
	private void updateChatMemberList(String[] list)
	{
		ChatMemberList chatMemberList = (ChatMemberList)pagerAdapter.getItem(2);
		chatMemberList.setChatMemberList(list);
	}
	
    private void setUIVariables() 
    {
        editMessage = (EditText)findViewById(R.id.editChatMessage);
        buttonExit = (Button)findViewById(R.id.buttonExit);
    }

    private void initializeWebView() 
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
        IncommingWebEvents incommingWebEvents = new IncommingWebEvents();
        webView.addJavascriptInterface(incommingWebEvents, "Android");
    }        
    
    //method only for debugging, will be removed in beta release
    public void killApp(View view)
    {
    	this.finish();
    }

    public OutgoingWebEvents getOutgoingWebEvents()
    {
    	return outgoingWebEvents;
    }
    
    // this is an interface for all events that fragments can call to back-end.
    public class OutgoingWebEvents
    {
    	// called from UI when user clicks on a room.
    	public void joinChannel(String channelName)
        {
    		// leave current channel...
    		String leaveChannelUrl = String.format("javascript:leaveChannel('%s', '%s')", currentChannel, username);
        	webView.loadUrl(leaveChannelUrl);
    		
    		// join desired channel...
        	currentChannel = channelName;
        	String joinChannelUrl = String.format("javascript:joinChannel('%s', '%s')", currentChannel, username);
        	webView.loadUrl(joinChannelUrl);
        }
    	
    	// called from UI when user clicks on "send message" button.
        public void sendMessage(String message)
        {
            if(!message.isEmpty()) 
            {
                // send message to server.
//            	String message2 = message.replace("'", "\\'");
                String sendMessageUrl = String.format("javascript:sendMessage('%s', '%s', '%s')", currentChannel, username, message);
                webView.loadUrl(sendMessageUrl);
            }
        }
    }
    
    // the methods of this class with the @JavascriptInterface attributes are called from javascript executed by the WebView
    private class IncommingWebEvents 
    {
        @JavascriptInterface
        public void hubStartDone()
        {
        	// signalR hub has been started.
        	// this would be where add/remove channel buttons would be enabled.
        }
        
        @JavascriptInterface
        public void broadcastMessageToChat(final String channelName, final String username, final String message) 
        {
        	// make sure the message is for the current channel.
        	if(channelName.equals(currentChannel)) {
	            chatWindowActivityHandler.post(new Runnable() 
	            {
	                @Override
	                public void run() 
	                {
	                	appendMessageToChat(username, message);
	                }
	            });
        	}
        }
        
        @JavascriptInterface
        public void setChannelList(final String[] channelList)
        {
        	chatWindowActivityHandler.post(new Runnable() 
            {
                @Override
                public void run() 
                {
                	updateChatRoomList(channelList);
                }
            });
        }
        
        @JavascriptInterface
        public void setUserList(final String[] chatUserList)
        {
        	chatWindowActivityHandler.post(new Runnable() 
            {
                @Override
                public void run() 
                {
                	updateChatMemberList(chatUserList);
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
