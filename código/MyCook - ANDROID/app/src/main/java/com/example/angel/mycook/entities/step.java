package com.example.angel.mycook.entities;

/**
 * Created by angel on 18/07/2017.
 */

public class step {

    private String description;
    private Double number_order;
    private Double id_recipe;

    public Double getId_recipe() {
        return id_recipe;
    }

    public void setId_recipe(Double id_recipe) {
        this.id_recipe = id_recipe;
    }

    public step() {
    }

    public Double getNumber_order() {
        return number_order;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public void setNumber_order(Double number_order) {
        this.number_order = number_order;
    }
}
