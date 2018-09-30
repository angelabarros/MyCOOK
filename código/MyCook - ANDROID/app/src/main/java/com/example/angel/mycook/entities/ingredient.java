package com.example.angel.mycook.entities;

/**
 * Created by angel on 18/07/2017.
 */

public class ingredient {

    private Double id_recipe;
    private Double id_ingredient;
    private Double quantity;
    private String name_ingredient;

    public ingredient() {
    }

    public Double getId_recipe() {
        return id_recipe;
    }

    public void setId_recipe(Double id_recipe) {
        this.id_recipe = id_recipe;
    }

    public Double getId_ingredient() {
        return id_ingredient;
    }

    public void setId_ingredient(Double id_ingredient) {
        this.id_ingredient = id_ingredient;
    }

    public Double getQuantity() {
        return quantity;
    }

    public void setQuantity(Double quantity) {
        this.quantity = quantity;
    }

    public String getName_ingredient() {
        return name_ingredient;
    }

    public void setName_ingredient(String name_ingredient) {
        this.name_ingredient = name_ingredient;
    }
}
