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
import com.example.angel.mycook.Adapters.CustomArrayAdapter;
import com.example.angel.mycook.Adapters.CustomStepAdapter;
import com.example.angel.mycook.entities.recipe;
import com.example.angel.mycook.entities.step;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class StepsRecipe extends AppCompatActivity {

    private Context mContext;
    private String mJSONURLString = "http://mycook.sihm.pt/api/stepsAPI";
    private ArrayList<step> all_steps = null;
    private ArrayList<step> steps = null;
    private Double id_recipe;
    ListView listView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mContext = getApplicationContext();
        setContentView(R.layout.activity_steps_recipe);

        id_recipe = getIntent().getDoubleExtra("id_recipe", 1);
        all_steps = new ArrayList<>();
        steps = new ArrayList<>();
        RequestQueue requestQueue = Volley.newRequestQueue(mContext);

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
                                step s = new step();
                                // Get the current student (json object) data

                                String desc = u.getString("description");
                                Double id_recipe = u.getDouble("id_recipe");
                                Double number = u.getDouble("number_order");
                                desc.toString();
                                id_recipe.toString();
                                s.setDescription(desc);
                                s.setNumber_order(number);
                                s.setId_recipe(id_recipe);
                                all_steps.add(s);


                            }


                            for (step st: all_steps
                                 ) {

                                if(st.getId_recipe().equals( id_recipe)){
                                    steps.add(st);
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
                        Toast.makeText(StepsRecipe.this, "erro", Toast.LENGTH_SHORT).show();
                    }
                }
        );

        // Add JsonArrayRequest to the RequestQueue
        requestQueue.add(jsonArrayRequest);
    }

    private void fillList(){

        CustomStepAdapter itemsAdapter = new CustomStepAdapter(this, steps );

        ((ListView)findViewById(R.id.listViewSTEPS)).setAdapter(itemsAdapter);
    }
}

