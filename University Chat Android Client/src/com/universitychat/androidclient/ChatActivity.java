package com.universitychat.androidclient;

import java.util.Vector;

//import android.R;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.view.ViewPager;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.webkit.JavascriptInterface;
import android.webkit.WebView;
import android.widget.TextView;
import android.widget.Toast;

import com.universitychat.androidclient.fragments.ChatMemberList;
import com.universitychat.androidclient.fragments.ChatRoom;
import com.universitychat.androidclient.fragments.ChatRoomList;


public class ChatActivity extends FragmentActivity
{
    private final Handler chatWindowActivityHandler = new Handler();
    private final String DEFAULT_HOST = "http://universitychat.azurewebsites.net/Android.html";
    private final String FEEDBACK_URL = "https://docs.google.com/forms/d/1pav-_eoF8V522xKQuj0tjt-dTFSGxPsFZ9ct_ZE9ylg/viewform";
    private final int ROOMLIST_FRAGMENT = 0;
    private final int CHATROOM_FRAGMENT = 1;
    private final int MEMBERLIST_FRAGMENT = 2;
    private WebView webView;
    private String username;
    private String password;
    private String currentChannel;
    private ViewPager viewPager;
    private FragmentManager fragmentManager;
    private MyViewPagerAdapter pagerAdapter;
    private OutgoingWebEvents outgoingWebEvents;
    private String URL;
    private AlertDialog.Builder builder;
	private AlertDialog dialog;

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
        URL = extras.getString("newHostURL");
        username = userCredentials[0];
        password = userCredentials[1];
        currentChannel = "";
        
        if(URL == null) //new host not provided by user, connect to default host
        	URL = DEFAULT_HOST;
        
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
        	//initializeWebView();
	        fragments.add(Fragment.instantiate(this, ChatRoomList.class.getName()));
	        fragments.add(Fragment.instantiate(this, ChatRoom.class.getName()));
	        fragments.add(Fragment.instantiate(this, ChatMemberList.class.getName()));
	        //webView.loadUrl(URL);	// connect to service (start the hub).
	        webView.loadUrl(URL);
        }
        else //or retrieve stored fragments
        {
        	
        	fragments.add(getSupportFragmentManager().getFragment(savedInstanceState, ChatRoomList.class.getName()));
        	fragments.add(getSupportFragmentManager().getFragment(savedInstanceState, ChatRoom.class.getName()));
        	fragments.add(getSupportFragmentManager().getFragment(savedInstanceState, ChatMemberList.class.getName()));
        	//webView.loadUrl(URL);
        	//webView.restoreState(savedInstanceState);
        }
         
        /** Instantiating FragmentPagerAdapter */
        pagerAdapter = new MyViewPagerAdapter(fragmentManager, fragments);
 
        
        /** Setting the pagerAdapter to the pager object */
        viewPager.setAdapter(pagerAdapter);
        
        viewPager.setCurrentItem(ROOMLIST_FRAGMENT);
        
        //webView.loadUrl(URL);	// connect to service (start the hub).
    }
    
    @Override
	public boolean onCreateOptionsMenu(Menu menu) 
	{
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.menu_chat, menu);
		return true;
	}
    
  //Handle event handling for individual menu items
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
 
        switch (item.getItemId())
        {
        	case R.id.menu_chat_about:
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
        		
        	case R.id.menu_create_channel:
        		builder = new AlertDialog.Builder(this);
        		LayoutInflater layInf =LayoutInflater.from(this);
                View view = layInf.inflate(R.layout.edit_text, null);
                final TextView editText = (TextView) view.findViewById(R.id.editText_change_host);
                
                builder.setPositiveButton("Create", new DialogInterface.OnClickListener() {
     	           public void onClick(DialogInterface dialog, int id) {
     	               outgoingWebEvents.createChannel(editText.getText().toString());
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
 
        	//Temporary for debug
        	case R.id.menu_remove_channel:
        		builder = new AlertDialog.Builder(this);
        		LayoutInflater layInf2 =LayoutInflater.from(this);
                View view2 = layInf2.inflate(R.layout.edit_text, null);
                final TextView editText2 = (TextView) view2.findViewById(R.id.editText_change_host);
                
                builder.setPositiveButton("Remove", new DialogInterface.OnClickListener() {
     	           public void onClick(DialogInterface dialog, int id) {
     	               outgoingWebEvents.deleteChannel(editText2.getText().toString());
     	           }});
     	       
                builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
      	           public void onClick(DialogInterface dialog, int id) {
      	               dialog.cancel();
      	           }});
      	       
                builder.setView(view2);
        		builder.setTitle(R.string.menu_change_host);
        		builder.setCancelable(true);
        		dialog = builder.create();
        		dialog.setCancelable(true); //cancelable by back button
        		dialog.setCanceledOnTouchOutside(false); //non-cancelable by click outside
        		dialog.show();
        		return true;
        		
        	case R.id.menu_feedback:
        		builder = new AlertDialog.Builder(this);
        		builder.setTitle(R.string.menu_feedback);
        		builder.setMessage(R.string.prompt_feedback_redirection);
        		builder.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
        	           public void onClick(DialogInterface dialog, int id) {
        	        	   Intent browserIntent = new Intent(Intent.ACTION_VIEW, Uri.parse(FEEDBACK_URL));
        	        		startActivity(browserIntent);
        	           }});
        		
        		builder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
     	           public void onClick(DialogInterface dialog, int id) {
     	               dialog.cancel();
     	           }});
        	       
        		dialog = builder.create();
        		dialog.setCancelable(true); //cancelable by back button
        		dialog.setCanceledOnTouchOutside(false); //non-cancelable by click outside
        		dialog.show();
        		
        		return true;
        	
        	case R.id.menu_sign_out:
        		Toast.makeText(getApplicationContext(), "To be implemented when user feature is ready.", Toast.LENGTH_SHORT).show();
        		return true;
        		
        	//Temporary for debug	
        	case R.id.menu_kill_app:
        		// leave current channel...
        		String leaveChannelUrl = String.format("javascript:leaveChannel('%s', '%s')", currentChannel, username);
            	webView.loadUrl(leaveChannelUrl);
            	
            	//destroy this activity
            	this.finish();
        		return true;
        		
        	default:
        		return super.onOptionsItemSelected(item);
        }
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
        //webView.saveState(outState);
        
    }
    
    @Override   
    protected void onRestoreInstanceState(Bundle savedInstanceState) 
    {
      super.onRestoreInstanceState(savedInstanceState);
      //webView.restoreState(savedInstanceState);
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

    private void initializeWebView() 
    {
        webView = (WebView)findViewById(R.id.androidWebViewTwo);
        final IncommingWebEvents incommingWebEvents = new IncommingWebEvents(this);
        webView.getSettings().setJavaScriptEnabled(true);
        webView.addJavascriptInterface(incommingWebEvents, "Android");
    }        

    public OutgoingWebEvents getOutgoingWebEvents()
    {
    	return outgoingWebEvents;
    }
    
    // this is an interface for all events that fragments can call to back-end.
    public class OutgoingWebEvents
    {
    	public void createChannel(String channelName) 
    	{
    		String createChannelUrl = String.format("javascript:createChannel('%s')", channelName);
        	webView.loadUrl(createChannelUrl);
    	}
    	
    	public void deleteChannel(String channelName) 
    	{
    		String deleteChannelUrl = String.format("javascript:deleteChannel('%s')", channelName);
        	webView.loadUrl(deleteChannelUrl);
    	}
    	
    	// called from UI when user clicks on a room.
    	public void joinChannel(String channelName)
        {
    		///---Need to verify user doesnt rejoin current channel, need a method get get current channel
    		if(currentChannel.equals(channelName)) {
    			return;
    		}
    		    		
    		// leave current channel if in one.
    		if(!currentChannel.isEmpty()) {
	    		String leaveChannelUrl = String.format("javascript:leaveChannel('%s', '%s')", currentChannel, username);
	        	webView.loadUrl(leaveChannelUrl);
    		}
        	
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
            	//System.out.println(String.format("Sending message to channel: %s, %s", message, currentChannel));
//            	String message2 = message.replace("'", "\\'");
                String sendMessageUrl = String.format("javascript:sendMessage('%s', '%s', '%s')", currentChannel, username, message);
                webView.loadUrl(sendMessageUrl);
                //appendMessageToChat(username, message);
            }
        }
        
        public void startHub() {
        	String startHubUrl = String.format("javascript:startHub('%s')", username);
            webView.loadUrl(startHubUrl);
        }
    }
    
    // the methods of this class with the @JavascriptInterface attributes are called from javascript executed by the WebView
    private class IncommingWebEvents 
    {
    	Context chatActivityContext;
    	
    	public IncommingWebEvents(Context c) {
    		chatActivityContext = c;
    	}
    	
    	@JavascriptInterface
        public void pageLoadComplete()
        {
    		outgoingWebEvents.startHub();
        }
    	
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
