package com.example.angel.mycook.Adapters;

import android.content.Context;
import android.support.annotation.NonNull;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.angel.mycook.R;
import com.example.angel.mycook.entities.ingredient;
import com.example.angel.mycook.entities.step;

import java.util.ArrayList;

/**
 * Created by angel on 17/07/2017.
 */

public class CustomIngAdapter extends ArrayAdapter<ingredient> {

    public CustomIngAdapter(Context context, ArrayList<ingredient> steps) {
        super(context, 0, steps);


    }

    @NonNull
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        ingredient a = getItem(position);

        if(convertView == null){
            convertView = LayoutInflater.from(getContext())
                    .inflate(R.layout.activity_line, parent, false);
        }

        ((TextView) convertView.findViewById(R.id.name_recipe)).setText("Ingredient: " +a.getName_ingredient().toString());

        ((TextView) convertView.findViewById(R.id.total_calories)).setText("Quantity: " + a.getQuantity().toString());

        return convertView;
    }
}