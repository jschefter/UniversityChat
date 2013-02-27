package com.universitychat.androidclient;


import java.util.Vector;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
 
public class MyViewPagerAdapter extends FragmentPagerAdapter{
 
    private Vector<Fragment> pageFragments;
 
    /** Constructor of the class */
    public MyViewPagerAdapter(FragmentManager fm, Vector<Fragment> newPageFragments) 
    {
        super(fm);
        pageFragments = newPageFragments;
    }
 
    /** This method will be invoked when a page is requested to create */
    @Override
    public Fragment getItem(int arg0) 
    {
        switch(arg0)
        {
	        case 0:
	        	return pageFragments.get(0);
	        case 1:
	        	return pageFragments.get(1);
	        default:
	        	return pageFragments.get(2);
        }
    }
 
    /** Returns the number of page fragments */
    @Override
    public int getCount() 
    {
        return pageFragments.size();
    }
}