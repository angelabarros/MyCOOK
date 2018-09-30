
/* table of users */
	create table users(
	id_user numeric(9,0) IDENTITY(1,1),
	username varchar(50),
    password varchar(50),
    subscription varchar(50),
    status varchar(50),
	role varchar(50),
	PRIMARY KEY (id_user)
	);

/* table of preferences */
create table preferences(
id_preference numeric(9,0) IDENTITY(1,1),
description varchar(50),
type varchar(50),
status varchar(50),
PRIMARY KEY (id_preference)
);

/* table of user_preferences */
create table user_preferences(
id_preference numeric(9,0),
id_user numeric(9,0),
FOREIGN KEY (id_preference) REFERENCES preferences (id_preference),
FOREIGN KEY (id_user) REFERENCES users (id_user),
PRIMARY KEY (id_preference, id_user)
);

/*table of recipe */
create table recipe(
id_recipe numeric(9,0) IDENTITY(1,1),
name_recipe varchar(50),
calories_portion numeric(9,0),
portions numeric(9,0),
fat_per_portion numeric (9,0),
saturatedfat_per_portion numeric (9,0),
protein_per_portion numeric (9,0),
sodium_per_portion numeric (9,0),
cholesterol_per_portion numeric (9,0),
carbs_per_portion numeric (9,0),
sugar_per_portion numeric (9,0),
fibre_per_portion numeric (9,0),
PRIMARY KEY (id_recipe)

);

/*table steps of a recipe */
create table steps(
id_step numeric(9,0) IDENTITY(1,1),
id_recipe numeric(9,0),
description varchar(2000),
number_order numeric(9,0),
PRIMARY KEY (id_step),
FOREIGN KEY (id_recipe) REFERENCES recipe (id_recipe)
);

/* table of ingredients */
create table ingredient(
id_ingredient numeric(9,0) IDENTITY(1,1),
name_ingredient varchar(50),
status varchar(50),
PRIMARY KEY (id_ingredient)
);

/* table of ingridients per recipe */
create table ingredients_recipe(
id_recipe numeric (9,0),
id_ingredient numeric (9,0),
quantity_per_portion numeric (9,0),
FOREIGN KEY (id_recipe) REFERENCES recipe (id_recipe),
FOREIGN KEY (id_ingredient) REFERENCES ingredient (id_ingredient),
PRIMARY KEY (id_recipe, id_ingredient)
);

/* table of favorite recipes of a user */
create table favorite_recipes (
id_user numeric(9,0),
id_recipe numeric(9,0),
FOREIGN KEY (id_recipe) REFERENCES recipe (id_recipe),
FOREIGN KEY (id_user) REFERENCES users (id_user),
PRIMARY KEY (id_recipe, id_user)
);

/* table of a meal plan of a user */
create table meal_plan(
id_mealplan numeric(9,0) IDENTITY(1,1),
id_user numeric(9,0),
FOREIGN KEY (id_user) REFERENCES users (id_user),
PRIMARY KEY (id_mealplan)
);

/*table of recipes in a mealplan */
create table recipes_mealplan(
id_recipe numeric(9,0),
id_mealplan numeric(9,0),
FOREIGN KEY (id_recipe) REFERENCES recipe (id_recipe),
FOREIGN KEY (id_mealplan) REFERENCES meal_plan(id_mealplan),
PRIMARY KEY (id_recipe, id_mealplan)
);

/*list of ingredients of a mealplan */
create table shopping_list (
id_mealplan numeric(9,0),
id_user numeric(9,0),
id_shoppinglist numeric(9,0) IDENTITY(1,1),
FOREIGN KEY (id_user) REFERENCES users (id_user),
FOREIGN KEY (id_mealplan) REFERENCES meal_plan(id_mealplan),
PRIMARY KEY (id_shoppinglist)
);

/*ingredients to be bought */
create table ingredients_to_buy(
id_shoppinglist numeric(9,0),
id_ingredient numeric(9,0),
amount numeric (9,0),
FOREIGN KEY (id_shoppinglist) REFERENCES shopping_list(id_shoppinglist),
FOREIGN KEY (id_ingredient) REFERENCES ingredient(id_ingredient),
PRIMARY KEY (id_shoppinglist, id_ingredient)
);

/*images of a recipe */
create table images_recipe(
id_image numeric(9,0) AUTO_INCREMENT,
url varchar(2000),
id_recipe numeric(9,0),
FOREIGN KEY (id_recipe) REFERENCES recipe(id_recipe),
PRIMARY KEY(id_image, id_recipe)
);

/*preferences that a recipe has */
create table recipe_preferences (
id_recipe numeric(9,0),
id_preference numeric(9,0),
FOREIGN KEY (id_recipe) REFERENCES recipe(id_recipe),
FOREIGN KEY (id_preference) REFERENCES preferences(id_preference),
PRIMARY KEY (id_recipe, id_preference)
);

/*plan's preferences... such as: number of fish per week, etc */
create table preference_plan(
id_preference numeric(9,0) AUTO_INCREMENT,
description varchar(200),
value numeric(9,0),
id_user numeric(9,0),
PRIMARY KEY (id_preference),
FOREIGN KEY (id_user) REFERENCES users(id_user)
);