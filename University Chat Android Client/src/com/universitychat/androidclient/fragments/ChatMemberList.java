package com.universitychat.androidclient.fragments;

import com.universitychat.androidclient.R;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

public class ChatMemberList extends Fragment
{
	public ListView memberList;
	private String[] chatMemberArray = {};//{"Loading..."};
	private TextView numUsers;
	private String storedNumUsers;
	
	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        System.out.println("member list oncreate called");
        
        if (savedInstanceState != null)
        	chatMemberArray = savedInstanceState.getStringArray("chatMemberArray");
    }
	
	@Override
    public void onSaveInstanceState(Bundle outState) 
	{
        super.onSaveInstanceState(outState);
        outState.putStringArray("chatMemberArray", chatMemberArray);
//        outState.putString("storedNumUsers", numUsers.getText().toString());
    }
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {
		System.out.println("member list oncreateview called");
    	View v = inflater.inflate(R.layout.fragment_chat_member_list, container,false);
    	numUsers = (TextView) v.findViewById(R.id.textView_num_users);
    	memberList = (ListView) v.findViewById(R.id.member_list);
    	
    	if(chatMemberArray.length > 0)
    		numUsers.setText(Integer.toString(chatMemberArray.length));
    	
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
		System.out.println("setChatMemberList called");
		for(int i = 0; i < newMemberList.length; i++)
			System.out.println(newMemberList[i]);
		
		chatMemberArray = newMemberList;
		memberList.setAdapter(new ArrayAdapter<String> (getActivity(), android.R.layout.simple_list_item_1, chatMemberArray));
		numUsers.setText(Integer.toString(newMemberList.length));
	}
}

