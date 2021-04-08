# Team2CA2ShoppingCart
.NET CORE CA Project

Project Specifications

Create a Shopping Cart web application using .NET Core for this CA project.

You can assume that the users who are purchasing have already registered with your system. You just need to provision a login screen for the user to map their activities to entries in your database. 

The Login screen should look like this. Remember to perform proper validation on users’ inputs and display appropriate error messages.

![image](https://user-images.githubusercontent.com/78467063/113659888-97911400-96d5-11eb-974b-97068ad4b1e5.png)

Figure 1:Login

Once logged in, the user will be brought to the Gallery page (Figure 2) to purchase.

![image](https://user-images.githubusercontent.com/78467063/113659961-ba232d00-96d5-11eb-81a8-a1d7ddf9da97.png)

Figure 2: Gallery page

Display the user’s name on the left and a link for him to logout of your system. There should be a Search bar for users to search the products he is looking for. Once he hit Enter, display the list of products that satisfy his Search string (Figure 3). Once he has cleared the Search bar, list all the products again.

![image](https://user-images.githubusercontent.com/78467063/113659999-cd35fd00-96d5-11eb-9400-1bbdef35af21.png)

Figure 3:Search

When the user clicked on the “Add To Cart” button, increment the number beside the Cart icon. If the Cart is currently empty and he clicked on the same item twice, the number besides the Cart icon should be 2.

When he clicks on the Cart icon, display the products as shown in Figure 4.
Allow him to change the quantity of each of his selected product and re-compute the Total amount on the top right. If he clicks Continue Shopping, brings him to the Gallery page (Figure 2). 

![image](https://user-images.githubusercontent.com/78467063/113660036-dd4ddc80-96d5-11eb-9f06-0933eb9f2e98.png)

Figure 4: View Cart

If he clicks on Checkout, we assume that the purchase has been successful and brings him to My Purchases pages (Figure 5). Each purchased product will have a unique activation code. If the user has purchased multiple copies of a product, he would have multiple activation codes. Display multiple activation codes with a combo box.

![image](https://user-images.githubusercontent.com/78467063/113660053-e5a61780-96d5-11eb-9a87-ca86bf651c87.png)

Figure 5: My Purchases

There might be occasions where users are directed directly to the Gallery page (e.g. via a bookmark). We want to allow these users to place items on the cart but before they can check-out, we need to get them to login (again, we assume that the user is already one of our customers). Once login is successful, we would complete the purchase.
Given the above requirements, adopt a program flow that makes sense to you.

The basic functionalities are:
1.	Login / Logout
2.	List Products
3.	Search
4.	Add to Cart
5.	View Cart
6.	Price Calculation
7.	Manage Activation Codes
8.	View Purchase History
_________________________________________________________________________________________________________________________________________________________________________________

This CA project has 25 marks; basic functionalities contribute 20 marks and extra credits an addition of 5 marks. Please feel free to implement any ideas that your team thinks are interesting and/or useful for the extra credits. 
Here are some ideas for extra credits:
1.	Reviews (e.g. allow users to give 1-5 stars to purchased products, add/display users’ comments from past purchases)
2.	Recommendations (e.g. recommend related products)

Other Requirements
1.	Please work in the final-project team that you have been assigned to. 
2.	The use of Entity Framework, for database operations, is highly encouraged.
3.	You will be assessed based on the following criteria:
    1.	Number of features completed
    2.	Clean code and sensible design
    3.	Functional user-interface
    4.	Video Demo of your app
4.	The deliverable for this CA is a single zipped file that consists of:
    1. Project source-code 
    2. Video presentation of your work (try to keep it within 20 mins)
5.	The submission deadline is Monday (19 Apr 2021), 9pm. 
6.	A LumiNUS submission folder will be created with an appropriate name like CA Submissions. There should only be 1 submission per team. Please name your files using your team’s name. For example, if you are Team 1, then your zipped file should be Team1.zip.
