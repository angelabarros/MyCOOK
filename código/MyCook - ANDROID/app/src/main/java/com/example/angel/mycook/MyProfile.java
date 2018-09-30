package com.example.angel.mycook;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.widget.TextView;
import android.widget.Toast;

import com.example.angel.mycook.entities.user;

public class MyProfile extends AppCompatActivity {

    user logged;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my_profile);
        
        logged = new user();

        logged.setUsername(getIntent().getStringExtra("username"));
        logged.setSubscription(getIntent().getStringExtra("subscription"));
        logged.setRole(getIntent().getStringExtra("role"));
        logged.setStatus(getIntent().getStringExtra("status"));

        TextView t = (TextView) findViewById(R.id.tvUSERLOGADO);
        t.setText(logged.getUsername());
    }


    @Override
    public boolean onCreateOptionsMenu(android.view.Menu menu) {
        getMenuInflater().inflate(R.menu.menu_options, menu);//Menu Resource, Menu
        return true;
    }

    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {

            case R.id.item_logout:

                Toast.makeText(MyProfile.this, "faz o logout", Toast.LENGTH_SHORT).show();
                //faz logout

                return true;

            default:
                return super.onOptionsItemSelected(item);
        }
    }

}
