package com.example.angel.mycook;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

public class activityRecipe extends AppCompatActivity {

    Double id_recipe;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recipe);


        /*
        *
        *
        *
        *  intent.putExtra("calories_portion", clicked.getCalories_portion());
                intent.putExtra("portions", clicked.getPortions());
                intent.putExtra("fat_portion", clicked.getFat_per_portion());
                intent.putExtra("saturatedfat_portion", clicked.getSaturatedfat_per_portion());
                intent.putExtra("cholesterol_portion", clicked.getCholesterol_per_portion());
                intent.putExtra("carbs_portion", clicked.getCarbs_per_portion());
                intent.putExtra("sugar_portion", clicked.getSugar_per_portion());
                intent.putExtra("fibre_portion", clicked.getProtein_per_portion());
        *
        * */

        String name = getIntent().getStringExtra("name_recipe");
        Double calories = getIntent().getDoubleExtra("calories_portion", 0);
        Double portions = getIntent().getDoubleExtra("portions", 0);
        Double fat = getIntent().getDoubleExtra("fat_portion", 0);
        Double saturated = getIntent().getDoubleExtra("saturatedfat_portion", 0);
        Double cholesterol = getIntent().getDoubleExtra("cholesterol_portion", 0);
        Double carbs = getIntent().getDoubleExtra("carbs_portion", 0);
        Double sugar = getIntent().getDoubleExtra("sugar_portion", 0);
        Double fibre = getIntent().getDoubleExtra("fibre_portion", 0);
        Double protein = getIntent().getDoubleExtra("protein_portion", 0);
        Double sodium = getIntent().getDoubleExtra("sodium_portion",0);
        id_recipe = getIntent().getDoubleExtra("id_recipe",1);

        TextView n = (TextView) findViewById(R.id.textView3);
        n.setText(name);

        TextView c = (TextView) findViewById(R.id.tv_calories);
        c.setText("Calories Portion: " +calories.toString());

        TextView p = (TextView) findViewById(R.id.tv_portions);
        p.setText("Portions: " + portions.toString());

        TextView f = (TextView) findViewById(R.id.tv_fat);
        f.setText("Fat: " +fat.toString());

        TextView s = (TextView) findViewById(R.id.tv_saturatedfat);
        s.setText("Saturated: "+ saturated.toString());

        TextView ch = (TextView) findViewById(R.id.tv_cholesterol);
        ch.setText("Cholesterol: "+cholesterol.toString());


        TextView ca = (TextView) findViewById(R.id.tv_carbs);
        ca.setText("Carbs:" + carbs.toString());


        TextView su = (TextView) findViewById(R.id.tv_sugar);
        su.setText("Sugar: " + sugar.toString());


        TextView fi = (TextView) findViewById(R.id.tv_fibre);
        fi.setText("Fibre: " + fibre.toString());


        TextView pr = (TextView) findViewById(R.id.tv_protein);
        pr.setText("Protein: " + protein.toString());

        TextView so = (TextView) findViewById(R.id.tv_sodium);
        so.setText("Sodium: " +sodium.toString());


    }

    public void seeSteps(View v){
        Intent i = new Intent(activityRecipe.this, StepsRecipe.class);
        i.putExtra("id_recipe", id_recipe);
        startActivity(i);
    }

    public void seeIngredients(View v){
        Intent i = new Intent(activityRecipe.this, IngredientsRecipe.class);
        i.putExtra("id_recipe", id_recipe);
        startActivity(i);
    }
}
