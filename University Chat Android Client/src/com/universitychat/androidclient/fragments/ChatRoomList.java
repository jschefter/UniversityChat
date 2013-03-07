package com.universitychat.androidclient.fragments;

import com.universitychat.androidclient.ChatActivity;
import com.universitychat.androidclient.R;
import com.universitychat.androidclient.ChatActivity.OutgoingWebEvents;

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
	
	private OutgoingWebEvents outgoingWebEvents;
	
	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
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
    	
    	publicList.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position,
					long id) {
				// user clicked on a channel.
				String roomName = (String)publicList.getAdapter().getItem(position);
				outgoingWebEvents.joinChannel(roomName);
			}
		});
    	
        return v;
    }
	
	@Override
	public void onPause()
	{
		super.onPause();
	}
	
	public void updatePublicRoomList(String[] roomList)
	{
		publicRooms = roomList;
		publicList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, publicRooms));
		
		System.out.println("update public room list called");
	}
}