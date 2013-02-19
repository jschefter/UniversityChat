package com.universitychat.androidclient;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.ListFragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.TextView;


public class ChatMemberList extends ListFragment
{
	public ArrayAdapter theListAdapter;
	public ListView memberList;
	public String[] chatMemberArray = {"Member01", "Member02", "Member03", "Member04", "Member05", "Member06", "Member07", "Member08", "Member09"};
	@Override
    public void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        
        theListAdapter =  new ArrayAdapter<String>(getActivity(),android.R.layout.simple_list_item_1, chatMemberArray);
        setListAdapter(theListAdapter); 
    }
	
//	public static ChatMemberList newInstance(String[] args)
//	{
//		System.out.println(args[3]);
//		ChatMemberList newList = new ChatMemberList();
//		Bundle bundle = new Bundle(1);
//		bundle.putStringArray("memberlist", args);
//		newList.setArguments(bundle);
//		
//		return new ChatMemberList();
//	}
	
	public void setChatMemberList(String[] newList)
	{
		
//		if(memberList != null)
//		{
			chatMemberArray = newList;
			theListAdapter =  new ArrayAdapter<String>(getActivity(),android.R.layout.simple_list_item_1, chatMemberArray);
			
//			for(int i = 0; i < chatMemberArray.length; i++)
//				System.out.println(chatMemberArray[i]);


			//this dosent refresh listview, need to fix
			theListAdapter.notifyDataSetChanged();
			memberList.invalidate();
			memberList.postInvalidate();
//		}
	}
	
	@Override
	 public void onListItemClick(ListView l, View v, int position, long id) 
	 {
		
	 }
	
	@Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) 
    {
    	View v = inflater.inflate(R.layout.fragment_chat_member_list, container,false);
    	memberList = (ListView)v.findViewById(android.R.id.list);
        return v;
    }
}

