package com.universitychat.androidclient.fragments;

import com.universitychat.androidclient.R;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class ChatMemberList extends Fragment
{
	public ListView memberList;
	public String[] chatMemberArray = {};//{"Loading..."};
	
	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
    }
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {
    	View v = inflater.inflate(R.layout.fragment_chat_member_list, container,false);
    	memberList = (ListView)v.findViewById(R.id.list);
    	memberList.setAdapter(new ArrayAdapter<String>(getActivity(),android.R.layout.simple_list_item_1, chatMemberArray));
        
        return v;
    }
	
	@Override
	public void onPause()
	{
		super.onPause();	
	}
	
	public void setChatMemberList(String[] newMemberList)
	{
		memberList.setAdapter(new ArrayAdapter<String>(getActivity(),android.R.layout.simple_list_item_1, newMemberList));
		System.out.println("chat member list called");
	}
}

