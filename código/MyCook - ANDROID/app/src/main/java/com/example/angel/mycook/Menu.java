package com.example.angel.mycook;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.example.angel.mycook.entities.user;

public class Menu extends AppCompatActivity {


    user logged;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_menu);

     //   String aux = getIntent().getStringExtra("username");

        logged = new user();

        logged.setUsername(getIntent().getStringExtra("username"));
        logged.setSubscription(getIntent().getStringExtra("subscription"));
        logged.setRole(getIntent().getStringExtra("role"));
        logged.setStatus(getIntent().getStringExtra("status"));

        TextView t = (TextView) findViewById(R.id.TVMenu_logado);
        t.setText("Hello " + logged.getUsername());

    }


    @Override
    public boolean onCreateOptionsMenu(android.view.Menu menu) {
        getMenuInflater().inflate(R.menu.menu_options, menu);//Menu Resource, Menu
        return true;
    }

    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {

            case R.id.item_logout:

                Toast.makeText(Menu.this, "faz o logout", Toast.LENGTH_SHORT).show();
                //faz logout

                return true;

            default:
                return super.onOptionsItemSelected(item);
        }
    }

    public void myprofile(View v){
        Intent i = new Intent(Menu.this, MyProfile.class);
        i.putExtra("username", logged.getUsername().toString());
        i.putExtra("subscription", logged.getSubscription().toString());
        i.putExtra("role", logged.getRole().toString());
        i.putExtra("status", logged.getStatus().toString());
        startActivity(i);
    }

    public void mealPlan(View v){
        Intent i = new Intent(Menu.this, MealPlan.class);
        i.putExtra("username", logged.getUsername().toString());
        i.putExtra("subscription", logged.getSubscription().toString());
        i.putExtra("role", logged.getRole().toString());
        i.putExtra("status", logged.getStatus().toString());
        startActivity(i);
    }

}
