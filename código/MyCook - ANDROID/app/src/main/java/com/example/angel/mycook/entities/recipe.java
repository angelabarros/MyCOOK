package com.example.angel.mycook.entities;

/**
 * Created by angel on 17/07/2017.
 */

public class recipe {

    private String name_recipe;
    private Double calories_portion;
    private Double portions;
    private Double sodium_per_portion;
    private Double fat_per_portion;
    private Double saturatedfat_per_portion;
    private Double protein_per_portion;
    private Double cholesterol_per_portion;
    private Double carbs_per_portion;
    private Double sugar_per_portion;
    private Double fibre_per_portion;
    private Double id_recipe;

    public Double getId_recipe() {
        return id_recipe;
    }

    public Double getSodium_per_portion() {
        return sodium_per_portion;
    }

    public void setSodium_per_portion(Double sodium_per_portion) {
        this.sodium_per_portion = sodium_per_portion;
    }

    public void setId_recipe(Double id_recipe) {
        this.id_recipe = id_recipe;
    }

    public recipe() {
    }

    public String getName_recipe() {
        return name_recipe;
    }

    public void setName_recipe(String name_recipe) {
        this.name_recipe = name_recipe;
    }

    public Double getCalories_portion() {
        return calories_portion;
    }

    public void setCalories_portion(Double calories_portion) {
        this.calories_portion = calories_portion;
    }

    public Double getPortions() {
        return portions;
    }

    public void setPortions(Double portions) {
        this.portions = portions;
    }

    public Double getFat_per_portion() {
        return fat_per_portion;
    }

    public void setFat_per_portion(Double fat_per_portion) {
        this.fat_per_portion = fat_per_portion;
    }

    public Double getSaturatedfat_per_portion() {
        return saturatedfat_per_portion;
    }

    public void setSaturatedfat_per_portion(Double saturatedfat_per_portion) {
        this.saturatedfat_per_portion = saturatedfat_per_portion;
    }

    public Double getProtein_per_portion() {
        return protein_per_portion;
    }

    public void setProtein_per_portion(Double protein_per_portion) {
        this.protein_per_portion = protein_per_portion;
    }

    public Double getCholesterol_per_portion() {
        return cholesterol_per_portion;
    }

    public void setCholesterol_per_portion(Double cholesterol_per_portion) {
        this.cholesterol_per_portion = cholesterol_per_portion;
    }

    public Double getCarbs_per_portion() {
        return carbs_per_portion;
    }

    public void setCarbs_per_portion(Double carbs_per_portion) {
        this.carbs_per_portion = carbs_per_portion;
    }

    public Double getSugar_per_portion() {
        return sugar_per_portion;
    }

    public void setSugar_per_portion(Double sugar_per_portion) {
        this.sugar_per_portion = sugar_per_portion;
    }

    public Double getFibre_per_portion() {
        return fibre_per_portion;
    }

    public void setFibre_per_portion(Double fibre_per_portion) {
        this.fibre_per_portion = fibre_per_portion;
    }
}
