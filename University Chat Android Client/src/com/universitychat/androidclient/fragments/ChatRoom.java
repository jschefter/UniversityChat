package com.universitychat.androidclient.fragments;

import com.universitychat.androidclient.R;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.method.ScrollingMovementMethod;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class ChatRoom extends Fragment
{
	private EditText editMessage;
    private Button buttonSendMessage;
    private TextView textViewChat;

	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
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
		}
		else
			System.out.println("textViewChat variable is null");		
	}
		
	public void enableButtons()
	{
		if(editMessage != null && buttonSendMessage != null)
		{
			editMessage.setEnabled(true);
	        buttonSendMessage.setEnabled(true);
		}
//		else
//		{
//			Activity v = getActivity();
//	        editMessage = (EditText)v.findViewById(R.id.editChatMessage);
//	        buttonSendMessage = (Button)v.findViewById(R.id.buttonSendMessage);
//			System.out.println("null");			
//		}
	}
	
	public void onAttach(Activity hostActivity)
	{
		super.onAttach(hostActivity);
		
		// This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
//        try 
//        {
//            mCallback = (OnHeadlineSelectedListener) hostActivity;
//        } 
//        catch (ClassCastException e) 
//        {
//            throw new ClassCastException(hostActivity.toString()
//                    + " must implement ChatRoomInterface");
//        }
		
	}
	
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {      
    	View v = inflater.inflate(R.layout.fragment_chat_window, container,false);
        editMessage = (EditText)v.findViewById(R.id.editChatMessage);
        buttonSendMessage = (Button)v.findViewById(R.id.buttonSendMessage);
        textViewChat = (TextView)v.findViewById(R.id.textViewChat);
        textViewChat.setMovementMethod(new ScrollingMovementMethod());

        return v;
    }
}