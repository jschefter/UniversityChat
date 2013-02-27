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
    private final int ROOMLIST_FRAGMENT = 0;
    private final int CHATROOM_FRAGMENT = 1;
    private final int MEMBERLIST_FRAGMENT = 2;

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
        
        /** Keep all pages active when swiping */
        viewPager.setOffscreenPageLimit(2);
 
        /** Getting fragment manager */
        fragmentManager = getSupportFragmentManager();
                
        Vector<Fragment> fragments = new Vector<Fragment>();
        
        if(savedInstanceState == null) //create new fragments to use
        {
	        fragments.add(Fragment.instantiate(this, ChatRoomList.class.getName()));
	        fragments.add(Fragment.instantiate(this, ChatRoom.class.getName()));
	        fragments.add(Fragment.instantiate(this, ChatMemberList.class.getName()));
        }
        else //or retrieve stored fragments
        {
        	fragments.add(getSupportFragmentManager().getFragment(savedInstanceState, ChatRoomList.class.getName()));
        	fragments.add(getSupportFragmentManager().getFragment(savedInstanceState, ChatRoom.class.getName()));
        	fragments.add(getSupportFragmentManager().getFragment(savedInstanceState, ChatMemberList.class.getName()));
        }
         
        /** Instantiating FragmentPagerAdapter */
        pagerAdapter = new MyViewPagerAdapter(fragmentManager, fragments);
 
        
        /** Setting the pagerAdapter to the pager object */
        viewPager.setAdapter(pagerAdapter);
        
        viewPager.setCurrentItem(ROOMLIST_FRAGMENT);
        setUIVariables();
        webView.loadUrl(URL);	// connect to service (start the hub).
    }
    
    private void jumpToChat()
    {
    	viewPager.setCurrentItem(CHATROOM_FRAGMENT);
    }
    
    
    //Storing Fragments to be retrieved after screen orientation changes
    @Override
    protected void onSaveInstanceState(Bundle outState) 
    {
        super.onSaveInstanceState(outState);
        getSupportFragmentManager().putFragment(outState, ChatRoomList.class.getName(), pagerAdapter.getItem(ROOMLIST_FRAGMENT));
        getSupportFragmentManager().putFragment(outState, ChatRoom.class.getName(), pagerAdapter.getItem(CHATROOM_FRAGMENT));
        getSupportFragmentManager().putFragment(outState, ChatMemberList.class.getName(), pagerAdapter.getItem(MEMBERLIST_FRAGMENT));
    }
    
    private void updateChatRoomList(String[] roomList)
    {
    	ChatRoomList chatRoomList = (ChatRoomList)pagerAdapter.getItem(ROOMLIST_FRAGMENT);
    	chatRoomList.updatePublicRoomList(roomList);
    }
    
    private void enableChatButtons()
    {
    	ChatRoom chatRoom = (ChatRoom)pagerAdapter.getItem(CHATROOM_FRAGMENT);
        chatRoom.enableButtons();
    }
    
	private void appendMessageToChat(String username, String msg)
	{
		ChatRoom chatRoom = (ChatRoom)pagerAdapter.getItem(CHATROOM_FRAGMENT);
        chatRoom.updateChatText(username, msg);
	}
	
	private void clearChatHistory()
	{
		ChatRoom chatRoom = (ChatRoom)pagerAdapter.getItem(CHATROOM_FRAGMENT);
		chatRoom.clearChatTextView();
	}
	
	private void updateChatMemberList(String[] list)
	{
		ChatMemberList chatMemberList = (ChatMemberList)pagerAdapter.getItem(MEMBERLIST_FRAGMENT);
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
    	// leave current channel...
		String leaveChannelUrl = String.format("javascript:leaveChannel('%s', '%s')", currentChannel, username);
    	webView.loadUrl(leaveChannelUrl);
    	
    	//destroy this activity
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
    		///---Need to verify user doesnt rejoin current channel, need a method get get current channel
    		
    		
    		// leave current channel...
    		String leaveChannelUrl = String.format("javascript:leaveChannel('%s', '%s')", currentChannel, username);
        	webView.loadUrl(leaveChannelUrl);
        	
        	//clear chat history
        	clearChatHistory();
    		
        	//enable text and send btn in chat
        	enableChatButtons();	
        	
    		// join desired channel...
        	currentChannel = channelName;
        	String joinChannelUrl = String.format("javascript:joinChannel('%s', '%s')", currentChannel, username);
        	webView.loadUrl(joinChannelUrl);
        	
        	//change view to chat room
        	jumpToChat();  	
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
                //appendMessageToChat(username, message);
            }
        }
    }
    
    // the methods of this class with the @JavascriptInterface attributes are called from javascript executed by the WebView
    private class IncommingWebEvents 
    {
        @JavascriptInterface
        public void hubStartDone()
        {
        	System.out.println("incomming from JS: Hub start is done");
        	// signalR hub has been started.
        	// this would be where add/remove channel buttons would be enabled.
        }
        
        @JavascriptInterface
        public void broadcastMessageToChat(final String channelName, final String username, final String message) 
        {
        	System.out.println(String.format("incomming from JS: channel: %s, user: %s, message: %s", channelName, username, message));
        	
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
        	System.out.println("incomming from JS: Got new channel list");
        	
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
        	System.out.println("incomming from JS: Got new chat user list");
        	
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
        	System.out.println("incomming from JS: join chat complete");
        	
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
