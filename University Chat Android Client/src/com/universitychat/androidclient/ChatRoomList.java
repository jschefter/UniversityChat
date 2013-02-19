package com.universitychat.androidclient;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.ListFragment;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.webkit.WebView.FindListener;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.AdapterView.OnItemClickListener;

public class ChatRoomList extends Fragment
{
	private ArrayAdapter theListAdapter1;
	private ArrayAdapter theListAdapter2;
	private String[] publicRooms = {"CSS490", "CSS342", "CSS343", "CSS430"};
	private String[] privateRooms = {"Private Room 1", "Private Room 2", "Private Room 3", "Private Room 4"};
	private ListView publicList;
	private ListView privateList;
	
	
	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);

    }

	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {
    	View v = inflater.inflate(R.layout.fragment_chat_list, container,false);
    	publicList = (ListView) v.findViewById(R.id.list_chat_public_room);
    	privateList = (ListView) v.findViewById(R.id.list_chat_private_room);
    	
    	publicList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, publicRooms));
    	privateList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, privateRooms));
    	
        return v;
    }
}