package com.example.angel.mycook;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.angel.mycook.Adapters.CustomIngAdapter;
import com.example.angel.mycook.Adapters.CustomStepAdapter;
import com.example.angel.mycook.entities.ingredient;
import com.example.angel.mycook.entities.step;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class IngredientsRecipe extends AppCompatActivity {

    private Context mContext;
    private String mJSONURLString = "http://mycook.sihm.pt/api/ingredients_recipeAPI";
    private String mJSONURLString2 = "http://mycook.sihm.pt/api/ingredients1";
    private ArrayList<ingredient> all_ingredients = null;
    private ArrayList<ingredient> ingredients = null;
    private Double id_recipeUSED;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mContext = getApplicationContext();
        setContentView(R.layout.activity_ingredients_recipe);

        id_recipeUSED = getIntent().getDoubleExtra("id_recipe", 1);
        all_ingredients = new ArrayList<>();
        ingredients = new ArrayList<>();



        RequestQueue requestQueue = Volley.newRequestQueue(mContext);

        JsonArrayRequest jsonArrayRequest = new JsonArrayRequest(
                Request.Method.GET,
                mJSONURLString2,
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
                                ingredient ing = new ingredient();

                                // Get the current student (json object) data

                                Double id = u.getDouble("id_ingredient");
                                String name_ingredient = u.getString("name_ingredient");

                                ing.setName_ingredient(name_ingredient);
                                ing.setId_ingredient(id);
                                ingredients.add(ing);

                            }

                            //fillList();

                            ingredients.size();


                            for (ingredient ingre: ingredients
                                    ) {

                                for (ingredient ingredi: all_ingredients
                                        ) {

                                    if(ingre.getId_ingredient().equals(ingredi.getId_ingredient())){

                                        ingredi.setName_ingredient(ingre.getName_ingredient());

                                    }

                                }
                            }


                        }catch (JSONException e){
                            e.printStackTrace();
                        }
                    }
                },
                new Response.ErrorListener(){
                    @Override
                    public void onErrorResponse(VolleyError error){
                        // Do something when error occurred
                        Toast.makeText(IngredientsRecipe.this, "erro", Toast.LENGTH_SHORT).show();
                    }
                }
        );

        // Add JsonArrayRequest to the RequestQueue
        requestQueue.add(jsonArrayRequest);












        RequestQueue requestQueue2 = Volley.newRequestQueue(mContext);

        JsonArrayRequest jsonArrayRequest2 = new JsonArrayRequest(
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
                                ingredient ing = new ingredient();

                                // Get the current student (json object) data

                                Double id = u.getDouble("id_ingredient");
                                Double id_recipe = u.getDouble("id_recipe");
                                Double quantity = u.getDouble("quantity_per_portion");

                                id.toString();
                                id_recipe.toString();

                                ing.setId_recipe(id_recipe);
                                ing.setId_ingredient(id);
                                ing.setQuantity(quantity);
                                if(id_recipe.equals(id_recipeUSED))
                                all_ingredients.add(ing);

                            }

                            //fillList();


                            all_ingredients.size();


                            for (ingredient ingre: ingredients
                                 ) {

                                for (ingredient ingredi: all_ingredients
                                     ) {

                                    if(ingre.getId_ingredient().equals(ingredi.getId_ingredient())){

                                        ingredi.setName_ingredient(ingre.getName_ingredient());

                                    }

                                }
                            }

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
                        Toast.makeText(IngredientsRecipe.this, "erro", Toast.LENGTH_SHORT).show();
                    }
                }
        );

        // Add JsonArrayRequest to the RequestQueue
        requestQueue2.add(jsonArrayRequest2);
    }

    private void fillList(){

        CustomIngAdapter itemsAdapter = new CustomIngAdapter(this, all_ingredients );

        ((ListView)findViewById(R.id.listviewING)).setAdapter(itemsAdapter);
    }

}
