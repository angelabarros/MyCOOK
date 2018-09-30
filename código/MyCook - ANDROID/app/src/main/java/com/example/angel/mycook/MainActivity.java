package com.example.angel.mycook;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.angel.mycook.entities.user;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {


    private Context mContext;
    private String mJSONURLString = "http://mycook.sihm.pt/api/userAPI";
    EditText username, password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        mContext = getApplicationContext();
        setContentView(R.layout.activity_main);
    }


    public void go_to_registry(View v){

    }

    public void login(View v){

        username = (EditText) findViewById(R.id.ETusername);
        password = (EditText) findViewById(R.id.ETpassword);

        if(username.getText().toString().isEmpty() || password.getText().toString().isEmpty()){
            Toast.makeText(MainActivity.this, R.string.both_fields, Toast.LENGTH_SHORT).show();

        }else{
           // Intent i = new Intent(MainActivity.this, Menu.class);
           // startActivity(i);


            RequestQueue requestQueue = Volley.newRequestQueue(mContext);

            final ArrayList<user> usersapp = new ArrayList<>();

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
                                    user user = new user();
                                    // Get the current student (json object) data
                                    String username = u.getString("username");
                                    String password = u.getString("password");
                                    String subscription = u.getString("subscription");
                                    String status = u.getString("status");
                                    String role = u.getString("role");
                                    //Toast.makeText(MainActivity.this, username, Toast.LENGTH_SHORT).show();

                                    //fill
                                    user.setUsername(username);
                                    user.setPassword(password);
                                    user.setSubscription(subscription);
                                    user.setRole(role);
                                    user.setStatus(status);
                                    usersapp.add(user);


                                }

                                for (user u: usersapp
                                     ) {
                                    if( u.getPassword().equals(password.getText().toString()) && u.getUsername().equals(username.getText().toString())){

                                        Intent i = new Intent(MainActivity.this, Menu.class);
                                        i.putExtra("username", u.getUsername().toString());
                                        i.putExtra("subscription", u.getSubscription().toString());
                                        i.putExtra("role", u.getRole().toString());
                                        i.putExtra("status", u.getStatus().toString());
                                        Toast.makeText(MainActivity.this, "Hello " +  u.getUsername().toString(), Toast.LENGTH_SHORT).show();
                                        startActivity(i);
                                        break;
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
                            Toast.makeText(MainActivity.this, "erro", Toast.LENGTH_SHORT).show();
                        }
                    }
            );

            // Add JsonArrayRequest to the RequestQueue
            requestQueue.add(jsonArrayRequest);
        }



    }
}
