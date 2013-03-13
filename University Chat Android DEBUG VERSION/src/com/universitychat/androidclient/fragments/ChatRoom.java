package com.universitychat.androidclient.fragments;

import com.universitychat.androidclient.ChatActivity;
import com.universitychat.androidclient.R;
import com.universitychat.androidclient.ChatActivity.OutgoingWebEvents;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.method.ScrollingMovementMethod;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextClock;
import android.widget.TextView;

public class ChatRoom extends Fragment
{
	private EditText editMessage;
    private Button buttonSendMessage;
    private TextView textViewChat;
    private static TextView textChatRoomName;
    private OutgoingWebEvents outgoingWebEvents;
    private String chatText;
    private static String currentChatRoomName;

	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        
        if(savedInstanceState == null)
        	currentChatRoomName = null;
        else
        {
        	enableButtons();
        	chatText = savedInstanceState.getString("chatText");
        	currentChatRoomName = savedInstanceState.getString("currentChatRoomName");
        	setChatRoomName(currentChatRoomName);
        }
        
    }
	
	@Override
    public void onSaveInstanceState(Bundle outState) 
	{
        super.onSaveInstanceState(outState);
        chatText = textViewChat.getText().toString();
        outState.putString("chatText", chatText);
        outState.putString("currentChatRoomName", currentChatRoomName);
    }
	
	public String getUserMsg()
	{
		if(textViewChat != null)
		{
			String temp = editMessage.getText().toString();
			editMessage.setText("");
			return temp;
		}
		else
			return "null TextViewChat";
	}
	
	public void updateChatText(String username, String msg)
	{
        final String formattedMessage = String.format("\n%s - %s", username, msg);
        
		if(textViewChat != null)
		{
			textViewChat.append(formattedMessage);
			
			final int offset = 
            		textViewChat.getLayout().getLineTop(textViewChat.getLineCount())  - textViewChat.getHeight();

            if(offset > 0) textViewChat.scrollTo(0, offset);
            else textViewChat.scrollTo(0, 0);
		}
		else
			System.out.println("textViewChat variable is null");		
	}
	
	protected static void setChatRoomName(String chatRoomName)
	{
		currentChatRoomName = chatRoomName;
		textChatRoomName.setText(chatRoomName);
		
	}
	
	public void clearChatTextView()
	{
		if(textViewChat != null)
			textViewChat.setText("");
	}
		
	public void enableButtons()
	{
		if(editMessage != null && buttonSendMessage != null)
		{
			editMessage.setEnabled(true);
	        buttonSendMessage.setEnabled(true);
		}
	}
		
	@Override
	public void onPause()
	{
		super.onPause();
	}
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {      
		ChatActivity chatActivity = (ChatActivity)getActivity();
		outgoingWebEvents = chatActivity.getOutgoingWebEvents();
		
    	View v = inflater.inflate(R.layout.fragment_chat_window, container,false);
        editMessage = (EditText)v.findViewById(R.id.editChatMessage);
        buttonSendMessage = (Button)v.findViewById(R.id.buttonSendMessage);
        textViewChat = (TextView)v.findViewById(R.id.textViewChat);
        textChatRoomName = (TextView)v.findViewById(R.id.textView_chat_room_header);
        textViewChat.setMovementMethod(new ScrollingMovementMethod());
        
        if(currentChatRoomName != null) //if there exists a current chat room session
        {
        	enableButtons();
        	textViewChat.append(chatText);
        }
        
        buttonSendMessage.setOnClickListener(new View.OnClickListener() 
        {
            @Override
            public void onClick(View v) 
            {
            	outgoingWebEvents.sendMessage(getUserMsg());
            }
        });

        return v;
    }
}