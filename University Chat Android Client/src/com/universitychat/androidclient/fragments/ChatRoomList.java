package com.universitychat.androidclient.fragments;

import com.universitychat.androidclient.ChatActivity;
import com.universitychat.androidclient.R;
import com.universitychat.androidclient.ChatActivity.OutgoingWebEvents;

import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.TextView;

public class ChatRoomList extends Fragment
{
	private String[] publicRooms = {};//{"Loading..."};
	private String[] privateRooms = {};//{"Loading..."};
	private ListView publicList;
	private ListView privateList;
	private int oldListPosition;
	
	private OutgoingWebEvents outgoingWebEvents;
	
	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        
        if (savedInstanceState == null)
        	oldListPosition = -1;
        else
        {
        	publicRooms = savedInstanceState.getStringArray("publicRooms");
            privateRooms = savedInstanceState.getStringArray("privateRooms");
            oldListPosition = savedInstanceState.getInt("oldListPosition");
        }
    }

	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {
		ChatActivity chatActivity = (ChatActivity)getActivity();
		outgoingWebEvents = chatActivity.getOutgoingWebEvents();
		
    	View v = inflater.inflate(R.layout.fragment_chat_list, container,false);
    	publicList = (ListView) v.findViewById(R.id.list_chat_public_room);
    	privateList = (ListView) v.findViewById(R.id.list_chat_private_room);
    	
    	
    	//single chat room support, temporary until implement multiple room support
    	publicList.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
    	privateList.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
    	
//    	//soon to be implemeneted
//    	publicList.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
//    	privateList.setChoiceMode(ListView.CHOICE_MODE_MULTIPLE);
    	
    	
    	publicList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, publicRooms));
    	privateList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, privateRooms));
    	
//    	if(oldListPosition > -1)
//    		publicList.getChildAt(0).setBackgroundColor(Color.BLUE);
    	
    	
    	publicList.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position,
					long id) {
				// user clicked on a channel, join room and set room name
				String roomName = (String)publicList.getAdapter().getItem(position);
				outgoingWebEvents.joinChannel(roomName);
				ChatRoom.setChatRoomName(roomName);
				
				//Support for selected item background highlighting
				if(oldListPosition > -1)
					parent.getChildAt(oldListPosition).setBackgroundColor(Color.BLACK);
				
				oldListPosition = position;
				parent.getChildAt(position).setBackgroundColor(Color.GRAY);
			}
		});
    	if(oldListPosition > -1)
    	{
    		publicList.setItemChecked(oldListPosition, true);
    		//publicList.getItemAtPosition(oldListPosition).setBackgroundColor(Color.GRAY);
    	}
    	
        return v;
    }
	
	@Override
    public void onSaveInstanceState(Bundle outState) 
	{
        super.onSaveInstanceState(outState);
        outState.putStringArray("publicRooms", publicRooms);
        outState.putStringArray("privateRooms", privateRooms);
        outState.putInt("oldListPosition;", oldListPosition);
    }
	
	@Override
	public void onPause()
	{
		System.out.println("on pause chatroom called");
		super.onPause();
	}
	
	public void updatePublicRoomList(String[] roomList)
	{
		System.out.println("Room List Size: " + roomList.length);
		for(int i = 0; i < roomList.length; i++)
			System.out.println(roomList[i]);
		publicRooms = roomList;
		publicList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, publicRooms));
		
		System.out.println("update public room list called");
	}
	
	public void updatePrivateRoomList(String[] roomList)
	{
		privateRooms = roomList;
		privateList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, publicRooms));
	}
}