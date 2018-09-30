package com.example.angel.mycook.Adapters;

import android.content.Context;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.angel.mycook.R;
import com.example.angel.mycook.entities.recipe;

import java.util.ArrayList;

/**
 * Created by angel on 18/07/2017.
 */

public class CustomArrayAdapter extends ArrayAdapter<recipe> {

    public CustomArrayAdapter(Context context, ArrayList<recipe> recipes) {
        super(context, 0, recipes);


    }

    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        recipe a = getItem(position);

        if(convertView == null){
            convertView = LayoutInflater.from(getContext())
                    .inflate(R.layout.activity_line, parent, false);
        }

        ((TextView) convertView.findViewById(R.id.name_recipe)).setText(a.getName_recipe());

        Double total = a.getCalories_portion() * a.getPortions();

        ((TextView) convertView.findViewById(R.id.total_calories)).setText("Calories: " +  total.toString() + " kcal " +  "    " + "Portions: " + a.getPortions().toString());

        return convertView;
    }
}
