package com.universitychat.androidclient;


import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
 
public class MyViewPagerAdapter extends FragmentPagerAdapter{
 
    final int PAGE_COUNT = 3;
    Fragment fragment1 = new ChatRoomList();
    ChatRoom fragment2 = new ChatRoom();
    ChatMemberList fragment3 = new ChatMemberList();
 
    /** Constructor of the class */
    public MyViewPagerAdapter(FragmentManager fm) 
    {
        super(fm);
    }
 
    /** This method will be invoked when a page is requested to create */
    @Override
    public Fragment getItem(int arg0) 
    {
        
        switch(arg0)
        {
	        case 0:
	        	return fragment1;
	        case 1:
	        	return fragment2;
	        default:
	        	return fragment3;
        }
    }
 
    /** Returns the number of pages */
    @Override
    public int getCount() 
    {
        return PAGE_COUNT;
    }
}