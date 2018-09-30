package com.example.angel.mycook;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.angel.mycook.Adapters.CustomArrayAdapter;
import com.example.angel.mycook.entities.recipe;
import com.example.angel.mycook.entities.user;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class MealPlan extends AppCompatActivity {

    private Context mContext;
    private String mJSONURLString = "http://mycook.sihm.pt/api/recipesAPI";
    private ArrayList<recipe> all_recipes = null;
    ListView listView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mContext = getApplicationContext();
        setContentView(R.layout.activity_meal_plan);

        listView = (ListView) findViewById(R.id.listViewRecipes);

        RequestQueue requestQueue = Volley.newRequestQueue(mContext);

        all_recipes = new ArrayList<>();

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent intent = new Intent(MealPlan.this, activityRecipe.class);
                intent.putExtra("coisas", position);
                recipe clicked = all_recipes.get(position);
                intent.putExtra("id_recipe", clicked.getId_recipe());

                intent.putExtra("name_recipe", clicked.getName_recipe());
                intent.putExtra("calories_portion", clicked.getCalories_portion());
                intent.putExtra("portions", clicked.getPortions());
                intent.putExtra("fat_portion", clicked.getFat_per_portion());
                intent.putExtra("saturatedfat_portion", clicked.getSaturatedfat_per_portion());
                intent.putExtra("cholesterol_portion", clicked.getCholesterol_per_portion());
                intent.putExtra("carbs_portion", clicked.getCarbs_per_portion());
                intent.putExtra("sugar_portion", clicked.getSugar_per_portion());
                intent.putExtra("sodium_portion", clicked.getSodium_per_portion());
                intent.putExtra("fibre_portion", clicked.getFibre_per_portion());
                intent.putExtra("protein_portion", clicked.getProtein_per_portion());
                startActivity(intent);
            }
        });

        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(
                Request.Method.GET,
                mJSONURLString,
                null,
                new Response.Listener<JSONArray>() {
                    @Override
                    public void onResponse(JSONArray response) {
                        // Do something with response
                        //mTextView.setText(response.toString());

                        // Process the JSON
                        try{
                            // Loop through the array elements
                            for(int i=0;i<response.length();i++){
                                // Get current json object
                                JSONObject u = response.getJSONObject(i);
                                recipe r = new recipe();
                                // Get the current student (json object) data

                                String name_recipe = u.getString("name_recipe");
                                Double id_recipe = u.getDouble("id_recipe");
                                Double calories_portion = u.getDouble("calories_portion");
                                Double portions = u.getDouble("portions");
                                Double fat_per_portion = u.getDouble("fat_per_portion");
                                Double saturatedfat_per_portion = u.getDouble("saturatedfat_per_portion");
                                Double cholesterol_per_portion = u.getDouble("cholesterol_per_portion");
                                Double carbs_per_portion = u.getDouble("carbs_per_portion");
                                Double protein_per_portion = u.getDouble("protein_per_portion");
                                Double sugar_per_portion = u.getDouble("sugar_per_portion");
                                Double fibre_per_portion = u.getDouble("fibre_per_portion");
                                Double sodium_per_portion = u.getDouble("sodium_per_portion");

                                //fill
                                r.setName_recipe(name_recipe);
                                r.setId_recipe(id_recipe);
                                r.setCalories_portion(calories_portion);
                                r.setPortions(portions);
                                r.setProtein_per_portion(protein_per_portion);
                                r.setFat_per_portion(fat_per_portion);
                                r.setSodium_per_portion(sodium_per_portion);
                                r.setSaturatedfat_per_portion(saturatedfat_per_portion);
                                r.setCholesterol_per_portion(cholesterol_per_portion);
                                r.setCarbs_per_portion(carbs_per_portion);
                                r.setSugar_per_portion(sugar_per_portion);
                                r.setFibre_per_portion(fibre_per_portion);
                                all_recipes.add(r);

                            }

                            all_recipes.size();

                            fillList();

                        }catch (JSONException e){
                            e.printStackTrace();
                        }
                    }
                },
                new Response.ErrorListener(){
                    @Override
                    public void onErrorResponse(VolleyError error){
                        // Do something when error occurred
                        Toast.makeText(MealPlan.this, "erro", Toast.LENGTH_SHORT).show();
                    }
                }
        );

        // Add JsonArrayRequest to the RequestQueue
        requestQueue.add(jsonArrayRequest);
    }

    private void fillList(){

        CustomArrayAdapter itemsAdapter = new CustomArrayAdapter(this, all_recipes );

        ((ListView)findViewById(R.id.listViewRecipes)).setAdapter(itemsAdapter);
    }

    }



